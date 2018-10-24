using FD.dat.mon.stb.lib;
using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Controls;
using Sadco.FamilyDoctor.Core.Controls.DesignerPanel;
using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.Core.EntityLogs;
using Sadco.FamilyDoctor.Core.Facades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            ctrlBFormatByPattern.Visible = Cl_SessionFacade.f_GetInstance().p_Doctor.p_Permission.p_IsEditAllRecords || Cl_SessionFacade.f_GetInstance().p_Doctor.p_Permission.p_IsEditSelfRecords;

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

        public event EventHandler<Cl_Record.Cl_EventArgs> e_Save;

        private Ctrl_Template m_ControlTemplate = null;
        private UС_RecordByFile m_ControlRecordByFile = null;

        private void f_UpdateControls()
        {
            try
            {
                m_ControlTemplate = null;
                m_ControlRecordByFile = null;
                ctrlPContent.Controls.Clear();
                if (m_Record != null)
                {
                    if (m_Record.p_Template != null)
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
                    else if (m_Record.p_Type == E_RecordType.FinishedFile)
                    {
                        m_ControlRecordByFile = new UС_RecordByFile();
                        m_ControlRecordByFile.f_SetRecord(m_Record);
                        ctrlPContent.Controls.Add(m_ControlRecordByFile);
                    }
                }
            }
            catch (Exception er)
            {
                MonitoringStub.Error("Error_Editor", "Не удалось обновить контролы в записи", er, null, null);
            }
        }

        public void f_SetRecord(Cl_Record a_Record)
        {
            m_Record = a_Record;
            if (m_Record != null)
            {
                if (m_Record.p_MedicalCard != null)
                {
                    ctrlPatientFIO.Text = string.Format("{0}, {1}, {2} ({3})", m_Record.p_MedicalCard.p_PatientFIO,
                        m_Record.p_MedicalCard.p_PatientSex == Core.Permision.Cl_User.E_Sex.Man ? "М" : m_Record.p_MedicalCard.p_PatientSex == Core.Permision.Cl_User.E_Sex.Female ? "Ж" : "Нет данных",
                        m_Record.p_MedicalCard.p_PatientDateBirth.ToShortDateString(), m_Record.p_MedicalCard.f_GetPatientAgeByMonthText(m_Record.p_DateCreate));
                    ctrlTitle.Text = m_Record.p_Title;
                    if (m_Record.p_DateReception.Year >= 1980)
                    {
                        ctrlDTPDateReception.Value = m_Record.p_DateReception;
                        ctrlDTPTimeReception.Value = m_Record.p_DateReception;
                    }
                    else
                    {
                        ctrlDTPDateReception.Value = DateTime.Now;
                        ctrlDTPTimeReception.Value = DateTime.Now;
                    }
                    if (m_Record.p_Version == 0)
                        ctrl_Version.Text = "Черновик";
                    else
                        ctrl_Version.Text = m_Record.p_Version.ToString();
                }
                if (m_Record.p_Template != null)
                {
                    try
                    {
                        Cl_TemplatesFacade.f_GetInstance().f_LoadTemplatesElements(m_Record.p_Template);
                        Text = string.Format("Запись \"{0}\" v{1}", m_Record.p_Template.p_Name, ConfigurationManager.AppSettings["Version"]);
                        f_UpdateControls();
                    }
                    catch (Exception er)
                    {
                        MonitoringStub.Error("Error_Editor", "Не удалось установить запись", er, null, null);
                    }
                }
                else if (m_Record.p_Type == E_RecordType.FinishedFile)
                {
                    try
                    {
                        Text = string.Format("Запись c готовым файлом v{0}", ConfigurationManager.AppSettings["Version"]);
                        f_UpdateControls();
                    }
                    catch (Exception er)
                    {
                        MonitoringStub.Error("Error_Editor", "Не удалось установить запись", er, null, null);
                    }
                }

                m_Log.f_SetEntity(m_Record);
            }
        }

        public void f_FormatByPattern(Cl_RecordPattern a_Pattern)
        {
            if (a_Pattern != null)
            {
                ctrlTitle.Text = a_Pattern.p_Title;
                Cl_RecordsFacade.f_GetInstance().f_EditRecordFromPattern(m_Record, a_Pattern);
                f_UpdateControls();
                m_Log.f_SetEntity(m_Record);
            }
        }

        private void ctrlBSave_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ctrlTitle.Text))
            {
                MonitoringStub.Message("Заполните поле \"Заголовок\"!");
                return;
            }
            if (ctrlDTPDateReception.Value == null)
            {
                MonitoringStub.Message("Заполните поле \"Дата приема\"!");
                return;
            }
            if (ctrlDTPTimeReception.Value == null)
            {
                MonitoringStub.Message("Заполните поле \"Время приема\"!");
                return;
            }
            if (m_Record != null)
            {
                Cl_Record record = null;
                if (m_Record.p_Type == E_RecordType.ByTemplate && m_ControlTemplate != null)
                {
                    record = m_ControlTemplate.f_GetNewRecord();
                }
                else if (m_Record.p_Type == E_RecordType.FinishedFile && m_ControlRecordByFile != null)
                {
                    record = m_ControlRecordByFile.f_GetNewRecord();
                    if (record?.p_FileBytes == null)
                    {
                        MonitoringStub.Message("Заполните поле \"Файл записи\"!");
                        return;
                    }
                }
                if (record != null)
                {
                    using (var transaction = Cl_App.m_DataContext.Database.BeginTransaction())
                    {
                        try
                        {
                            if (m_Log.f_IsChanged(record) == false && record.p_Title == ctrlTitle.Text)
                            {
                                MonitoringStub.Message("Элемент не изменялся!");
                                transaction.Rollback();
                                return;
                            }

                            record.p_Title = ctrlTitle.Text;
                            record.p_DateReception = new DateTime(ctrlDTPDateReception.Value.Year,
                                                            ctrlDTPDateReception.Value.Month,
                                                            ctrlDTPDateReception.Value.Day,
                                                            ctrlDTPTimeReception.Value.Hour,
                                                            ctrlDTPTimeReception.Value.Minute,
                                                            0);

                            if (Cl_SessionFacade.f_GetInstance().p_Doctor.p_Permission.p_Role == Core.Permision.E_Roles.Assistant)
                            {
                                record.f_SetDoctor(Cl_SessionFacade.f_GetInstance().p_Doctor.p_ParentUser);
                            }

                            Cl_App.m_DataContext.p_Records.Add(record);
                            Cl_App.m_DataContext.SaveChanges();
                            
                            if (m_Record.p_Type == E_RecordType.FinishedFile)
                            {
                                record.p_FilePath = Cl_RecordsFacade.f_GetInstance().f_GetLocalResourcesRelativeFilePath(record);
                                DirectoryInfo dirInfo = new DirectoryInfo(Cl_RecordsFacade.f_GetInstance().f_GetLocalResourcesPath());
                                dirInfo.CreateSubdirectory(Cl_RecordsFacade.f_GetInstance().f_GetLocalResourcesRelativePath(record));
                                File.WriteAllBytes(Cl_RecordsFacade.f_GetInstance().f_GetLocalResourcesPath() + "/" + record.p_FilePath, record.p_FileBytes);
                                //if (m_Record.p_FileType == E_RecordFileType.HTML)
                                //{
                                //    Regex regex = new Regex(@"src=(?<source>.*?\.gif)|src=(?<source>.*?\.jpeg)|src=(?<source>.*?\.jpeg)|src=(?<source>.*?\.png)");
                                //    var html = Encoding.UTF8.GetString(record.p_FileBytes);
                                //    var matches = regex.Matches(html);
                                //    foreach (Match match in matches)
                                //    {
                                //        var filePath =  match.Groups["source"].Value
                                //        File.WriteAllBytes(Cl_RecordsFacade.f_GetInstance().f_GetLocalResourcesPath() + "/" + record.p_FilePath, record.p_FileBytes);
                                //    }
                                //}
                            } else
                            {
                                record.p_HTMLDoctor = record.f_GetHTMLDoctor();
                                record.p_HTMLPatient = record.f_GetHTMLPatient();
                            }

                            //record.p_FileType = E_RecordFileType.HTML;
                            if (record.p_Version == 1)
                            {
                                record.p_RecordID = record.p_ID;
                            }
                            Cl_App.m_DataContext.SaveChanges();
                            Cl_EntityLog.f_CustomMessageLog(E_EntityTypes.UIEvents, string.Format("Сохранение записи: {0}, дата записи: {1}, клиника: {2}", record.p_Title, record.p_DateCreate, record.p_ClinicName), record.p_RecordID);
                            m_Log.f_SaveEntity(record);

                            transaction.Commit();
                            f_SetRecord(record);
                            e_Save?.Invoke(this, new Cl_Record.Cl_EventArgs() { p_Record = record });
                            this.Close();

                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            try
                            {
                                File.Delete(record.p_FilePath);
                            }
                            catch { };
                            MonitoringStub.Error("Error_Editor", "При сохранении изменений записи произошла ошибка", ex, null, null);
                        }
                    }
                }
            }
        }

        private void ctrlBFormatByPattern_Click(object sender, EventArgs e)
        {
            var dlg = new Dlg_RecordSelectPattern(p_Record.p_Template);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                if (dlg.p_SelectedRecordPattern != null)
                {
                    f_FormatByPattern(dlg.p_SelectedRecordPattern);
                }
            }
        }

        private void ctrlBHistory_Click(object sender, EventArgs e)
        {
            try
            {
                Dlg_HistoryViewer viewer = new Dlg_HistoryViewer();
                viewer.LoadHistory(false, E_EntityTypes.Records, p_Record.p_RecordID);
                viewer.ShowDialog(this);
            }
            catch (Exception er)
            {
                MonitoringStub.Error("Error_Editor", "Не удалось открыть истории", er, null, null);
            }
        }

        private void ctrlBMKB_Click(object sender, EventArgs e)
        {
            var dlgMKB = new Dlg_MKB();
            dlgMKB.ctrlTBMKB1.Text = m_Record.p_MKB1;
            dlgMKB.ctrlTBMKB2.Text = m_Record.p_MKB2;
            dlgMKB.ctrlTBMKB3.Text = m_Record.p_MKB3;
            dlgMKB.ctrlTBMKB4.Text = m_Record.p_MKB4;
            if (dlgMKB.ShowDialog() == DialogResult.OK)
            {
                m_Record.p_MKB1 = dlgMKB.ctrlTBMKB1.Text;
                m_Record.p_MKB2 = dlgMKB.ctrlTBMKB2.Text;
                m_Record.p_MKB3 = dlgMKB.ctrlTBMKB3.Text;
                m_Record.p_MKB4 = dlgMKB.ctrlTBMKB4.Text;
                if (m_Record.p_Type == E_RecordType.ByTemplate && m_ControlTemplate != null)
                {
                    m_ControlTemplate.f_UpdateMKB();
                }
                else if (m_Record.p_Type == E_RecordType.FinishedFile && m_ControlRecordByFile != null)
                {
                    m_ControlRecordByFile.f_UpdateMKB();
                }
            }
        }

        #region Рейтинг
        private void ctrlBRating_Click(object sender, EventArgs e)
        {
            try
            {
                Dlg_RatingViewer viewer = new Dlg_RatingViewer();
                viewer.f_LoadRating(p_Record);
                viewer.ShowDialog(this);
            }
            catch (Exception er)
            {
                MonitoringStub.Error("Error_Editor", "Не удалось открыть оценку", er, null, null);
            }
        }

        private void InitRating()
        {
            double rate = 0;
            int total = 0;
            if (p_Record == null) return;

            ctrlBRating.Visible = Cl_SessionFacade.f_GetInstance().p_Doctor.p_Permission.p_IsEditAllRatings && p_Record != null && p_Record.p_Version > 0;

            try
            {
                IQueryable<Cl_Rating> ratings = Cl_App.m_DataContext.p_Ratings.Where(l => l.p_RecordID == p_Record.p_RecordID);
                Dictionary<int, Cl_Rating> result = new Dictionary<int, Cl_Rating>();

                foreach (Cl_Rating item in ratings)
                {
                    if (!result.ContainsKey(item.p_UserID))
                    {
                        result.Add(item.p_UserID, item);
                        continue;
                    }

                    if (result[item.p_UserID].p_Time < item.p_Time)
                        result[item.p_UserID] = item;
                }

                foreach (KeyValuePair<int, Cl_Rating> item in result)
                {
                    rate = rate + item.Value.p_Value;
                    total++;
                }

                if (rate > 0)
                {
                    this.Text += "   Оценка: " + Math.Round(rate / total, 1).ToString();
                }
            }
            catch (Exception er)
            {
                MonitoringStub.Error("Error_Editor", "Не удалось инициализировать оценку", er, null, null);
            }
        }
        #endregion
    }
}
