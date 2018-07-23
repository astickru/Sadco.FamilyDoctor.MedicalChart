﻿using FD.dat.mon.stb.lib;
using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Controls;
using Sadco.FamilyDoctor.Core.Controls.DesignerPanel;
using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.Core.EntityLogs;
using Sadco.FamilyDoctor.Core.Facades;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
    public partial class Dlg_Record : Form
    {
        private Cl_EntityLog m_Log = new Cl_EntityLog();

        public Dlg_Record()
        {
            this.Font = new System.Drawing.Font(ConfigurationManager.AppSettings["FontFamily"],
                    float.Parse(ConfigurationManager.AppSettings["FontSize"]),
                    (System.Drawing.FontStyle)int.Parse(ConfigurationManager.AppSettings["FontStyle"]),
                    System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            InitializeComponent();
            
            this.Load += Dlg_Record_Load;
        }

        private void Dlg_Record_Load(object sender, EventArgs e)
        {
            InitRating();
        }

        private Cl_Record m_Record = null;
        public Cl_Record p_Record {
            get {
                return m_Record;
            }
            set {
                f_SetRecord(value);
            }
        }

        private int m_PaddingX = 5;
        [Category("p_Padding")]
        [DefaultValue(false)]
        public int p_PaddingX {
            get { return m_PaddingX; }
            set {
                if (this.m_PaddingX != value)
                {
                    m_PaddingX = value;
                }
            }
        }

        private int m_PaddingY = 5;
        [Category("p_Padding")]
        [DefaultValue(false)]
        public int p_PaddingY {
            get { return m_PaddingY; }
            set {
                if (this.m_PaddingY != value)
                {
                    m_PaddingY = value;
                    f_UpdateControls();
                }
            }
        }

        public event EventHandler e_Save;

        private Ctrl_Template m_ControlTemplate = null;

        private void f_UpdateControls()
        {
            m_ControlTemplate = null;
            ctrlPContent.Controls.Clear();
            if (m_Record != null && m_Record.p_Template != null)
            {
                if (m_Record.p_Template.p_TemplateElements == null)
                {
                    var cTe = Cl_App.m_DataContext.Entry(m_Record.p_Template).Collection(g => g.p_TemplateElements).Query().Include(te => te.p_ChildElement).Include(te => te.p_ChildElement.p_Default).Include(te => te.p_ChildTemplate);
                    cTe.Load();
                }
                m_ControlTemplate = new Ctrl_Template();
                m_ControlTemplate.Dock = DockStyle.Fill;
                m_ControlTemplate.p_Template = m_Record.p_Template;
                m_ControlTemplate.p_PaddingX = p_PaddingX;
                m_ControlTemplate.p_PaddingY = p_PaddingY;
                m_ControlTemplate.f_SetRecord(m_Record);
                ctrlPContent.Controls.Add(m_ControlTemplate);
            }
        }

        public void f_SetRecord(Cl_Record a_Record)
        {
            m_Record = a_Record;
            if (m_Record != null && m_Record.p_Template != null)
            {
                Cl_TemplatesFacade.f_GetInstance().f_LoadTemplatesElements(m_Record.p_Template);
                ctrlDoctorFIO.Text = m_Record.p_DoctorFIO;
                ctrlPatientFIO.Text = string.Format("{0} ({1}, {2})", m_Record.p_PatientFIO,
                    m_Record.p_PatientSex == Core.Permision.Cl_User.E_Sex.Man ? "Мужчина" : m_Record.p_PatientSex == Core.Permision.Cl_User.E_Sex.Female ? "Женьщина" : "Нет данных",
                    m_Record.p_PatientDateBirth.ToShortDateString());
                ctrlTitle.Text = m_Record.p_Title;
                Text = string.Format("Запись \"{0}\" v{1}", m_Record.p_Template.p_Name, ConfigurationManager.AppSettings["Version"]);
                if (m_Record.p_Version == 0)
                    ctrl_Version.Text = "Черновик";
                else
                    ctrl_Version.Text = m_Record.p_Version.ToString();
                f_UpdateControls();
                m_Log.f_SetEntity(m_Record);
            }
        }

        private void ctrlBSave_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ctrlTitle.Text))
            {
                MonitoringStub.Message("Заголовок пустой!");
                return;
            }
            if (m_ControlTemplate != null)
            {
                using (var transaction = Cl_App.m_DataContext.Database.BeginTransaction())
                {
                    try
                    {
                        var record = m_ControlTemplate.f_GetNewRecord();
                        if (record != null)
                        {
                            if (m_Log.f_IsChanged(record) == false && record.p_Title == ctrlTitle.Text)
                            {
                                MonitoringStub.Message("Элемент не изменялся!");
                                transaction.Rollback();
                                return;
                            }

                            record.p_Title = ctrlTitle.Text;
                            Cl_App.m_DataContext.p_Records.Add(record);
                            Cl_App.m_DataContext.SaveChanges();
                            record.p_FileType = Cl_Record.E_RecordFileType.HTML;
                            record.p_HTMLDoctor = record.f_GetHTMLDoctor();
                            record.p_HTMLPatient = record.f_GetHTMLPatient();
                            if (record.p_Version == 1)
                            {
                                record.p_RecordID = record.p_ID;
                            }
                            Cl_App.m_DataContext.SaveChanges();
                            m_Log.f_SaveEntity(record);
                            transaction.Commit();
                            //m_ControlTemplate.f_SetRecord(record);
                            f_SetRecord(record);
                            e_Save?.Invoke(this, new EventArgs());
                            //ctrl_Version.Text = record.p_Version.ToString();
                        }
                        else
                        {
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MonitoringStub.Error("Error_Editor", "При сохранении изменений записи произошла ошибка", ex, null, null);
                    }
                }
            }
        }

        private void ctrlBHistory_Click(object sender, EventArgs e)
        {
            Dlg_HistoryViewer viewer = new Dlg_HistoryViewer();
            viewer.LoadHistory(p_Record.p_RecordID, E_EntityTypes.Records);
            viewer.ShowDialog(this);
        }

        #region Рейтинг
        private void ctrlBRating_Click(object sender, EventArgs e)
        {
            Dlg_RatingViewer viewer = new Dlg_RatingViewer();
            viewer.LoadRating(p_Record.p_RecordID);
            viewer.ShowDialog(this);
        }

        private void InitRating()
        {
            double rate = 0;
            int total = 0;
            if (p_Record == null) return;

            ctrlBRating.Visible = Cl_SessionFacade.f_GetInstance().p_Doctor.p_Permission.p_IsEditAllRatings;

            IQueryable<Cl_Rating> ratings = Cl_App.m_DataContext.p_Ratings.Where(l => l.p_RecordID == p_Record.p_RecordID);
            foreach (Cl_Rating item in ratings)
            {
                rate = rate + item.p_Value;
                total++;
            }

            if (rate > 0)
            {
                this.Text += "   Оценка: " + Math.Round(rate / total, 1).ToString();
            }
        }
        #endregion
    }
}
