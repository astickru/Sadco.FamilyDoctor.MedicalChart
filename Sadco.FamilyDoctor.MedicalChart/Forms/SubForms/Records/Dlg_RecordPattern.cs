using FD.dat.mon.stb.lib;
using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Controls.DesignerPanel;
using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.Core.EntityLogs;
using Sadco.FamilyDoctor.Core.Facades;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data.Entity;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
    public partial class Dlg_RecordPattern : Form
    {
        private Cl_EntityLog m_Log = new Cl_EntityLog();

        public Dlg_RecordPattern()
        {
            this.Font = new System.Drawing.Font(ConfigurationManager.AppSettings["FontFamily"],
                    float.Parse(ConfigurationManager.AppSettings["FontSize"]),
                    (System.Drawing.FontStyle)int.Parse(ConfigurationManager.AppSettings["FontStyle"]),
                    System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            InitializeComponent();
        }

        private Cl_Record m_Record = null;
        private Cl_Record m_SourceRecord = null;

        private Cl_RecordPattern m_RecordPattern = null;
        public Cl_RecordPattern p_RecordPattern {
            get {
                return m_RecordPattern;
            }
            set {
                f_SetRecordPattern(value);
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
            if (m_RecordPattern != null && m_RecordPattern.p_Template != null)
            {
                if (m_RecordPattern.p_Template.p_TemplateElements == null)
                {
                    var cTe = Cl_App.m_DataContext.Entry(m_RecordPattern.p_Template).Collection(g => g.p_TemplateElements).Query().Include(te => te.p_ChildElement).Include(te => te.p_ChildElement.p_Default).Include(te => te.p_ChildTemplate);
                    cTe.Load();
                }
                m_ControlTemplate = new Ctrl_Template();
                m_ControlTemplate.Dock = DockStyle.Fill;
                m_ControlTemplate.p_Template = m_RecordPattern.p_Template;
                m_ControlTemplate.p_PaddingX = p_PaddingX;
                m_ControlTemplate.p_PaddingY = p_PaddingY;
                m_ControlTemplate.f_SetRecord(m_Record);
                ctrlPContent.Controls.Add(m_ControlTemplate);
            }
        }

        public void f_SetRecordPattern(Cl_RecordPattern a_RecordPattern)
        {
            m_RecordPattern = a_RecordPattern;
            if (m_RecordPattern != null && m_RecordPattern.p_Template != null)
            {
                try
                {
                    Cl_TemplatesFacade.f_GetInstance().f_LoadTemplatesElements(m_RecordPattern.p_Template);
                    ctrlDoctorFIO.Text = m_RecordPattern.p_DoctorFIO;
                    ctrlTitle.Text = m_RecordPattern.p_Title;
                    ctrlName.Text = m_RecordPattern.p_Name;
                    Text = string.Format("Паттерн записей по шаблону \"{0}\" v{1}", m_RecordPattern.p_Template.p_Name, ConfigurationManager.AppSettings["Version"]);
                    m_Record = Cl_RecordsFacade.f_GetInstance().f_GetNewRecord(m_RecordPattern);
                    f_UpdateControls();
                    m_Log.f_SetEntity(m_Record);
                }
                catch (Exception er)
                {
                    MonitoringStub.Error("Error_Editor", "Не удалось установить паттерн записей по шаблону", er, null, null);
                }
            }
        }

        internal void FormatPaternFromRecord(Cl_Record a_Record)
        {
            if (a_Record == null) return;
            try
            {
                m_SourceRecord = a_Record;
                Cl_TemplatesFacade.f_GetInstance().f_LoadTemplatesElements(a_Record.p_Template);
                Cl_RecordPattern pattern = Cl_RecordsFacade.f_GetInstance().f_GetNewRecordPattern(a_Record);
                pattern.p_ClinicName = Cl_SessionFacade.f_GetInstance().p_Doctor.p_ClinicName;
                pattern.f_SetDoctor(Cl_SessionFacade.f_GetInstance().p_Doctor);
                this.p_RecordPattern = pattern;
            }
            catch (Exception er)
            {
                MonitoringStub.Error("Error_Editor", "Не удалось сформировать паттерн записей по записи", er, null, null);
            }
        }

        private void ctrlBSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ctrlTitle.Text))
            {
                MonitoringStub.Message("Заполните поле \"Заголовок\"!");
                return;
            }
            if (string.IsNullOrWhiteSpace(ctrlName.Text))
            {
                MonitoringStub.Message("Заполните поле \"Название\"!");
                return;
            }
            if (m_ControlTemplate != null)
            {
                using (var transaction = Cl_App.m_DataContext.Database.BeginTransaction())
                {
                    try
                    {
                        var record = m_ControlTemplate.f_GetNewRecord(false);
                        if (record != null)
                        {
                            if (m_SourceRecord == null && m_Log.f_IsChanged(record) == false && record.p_Title == ctrlTitle.Text)
                            {
                                MonitoringStub.Message("Паттерн не изменялся!");
                                transaction.Rollback();
                                return;
                            }
                            record.p_Title = ctrlTitle.Text;
                            var recordPattern = Cl_RecordsFacade.f_GetInstance().f_GetNewRecordPattern(ctrlName.Text, record);
                            if (recordPattern != null)
                            {
                                Cl_App.m_DataContext.p_RecordsPatterns.Add(recordPattern);
                                Cl_App.m_DataContext.SaveChanges();

                                if (m_SourceRecord == null)
                                    Cl_EntityLog.f_CustomMessageLog(E_EntityTypes.RecordsPatterns, string.Format("Создан новый патерн \"{0}\" по шаблону \"{1}\"", recordPattern.p_Name, recordPattern.p_Template.p_Name));
                                else
                                    Cl_EntityLog.f_CustomMessageLog(E_EntityTypes.RecordsPatterns, string.Format("Сформирован патерн по записи \"{0}\"", m_SourceRecord.p_Title));

                                transaction.Commit();
                                f_SetRecordPattern(recordPattern);
                                e_Save?.Invoke(this, new EventArgs());
                            }
                            else
                            {
                                transaction.Rollback();
                                return;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MonitoringStub.Error("Error_Editor", "При сохранении изменений паттерна записей произошла ошибка", ex, null, null);
                    }
                }
            }
        }
    }
}
