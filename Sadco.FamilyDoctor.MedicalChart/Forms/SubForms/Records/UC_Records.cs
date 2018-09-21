using FD.dat.mon.stb.lib;
using OutlookStyleControls;
using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.Core.EntityLogs;
using Sadco.FamilyDoctor.Core.Facades;
using Sadco.FamilyDoctor.Core.Permision;
using System;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
    public partial class UC_Records : UserControl
    {
        public UC_Records()
        {
            Tag = string.Format("Записи клиента v{0}", ConfigurationManager.AppSettings["Version"]);
            InitializeComponent();

            p_DateForming.ValueType = typeof(DateTime);
            ctrlLPatientName.Text = Cl_SessionFacade.f_GetInstance().p_Patient.p_FIO;
            m_Permission = Cl_SessionFacade.f_GetInstance().p_Doctor.p_Permission;

            ctrlBReportAddRecord.Visible = ctrlBReportAddPattern.Visible = m_Permission.p_IsEditAllRecords || m_Permission.p_IsEditSelfRecords;
            ctrlBAddRecordFromRecord.Visible = false;
            ctrlBReportFormatPattern.Visible = false;

            m_WebBrowserPrint.Parent = this;
            m_WebBrowserPrint.DocumentCompleted += M_WebBrowserPrint_DocumentCompleted;
            f_UpdateRecords();
        }

        /// <summary>Флаг отображения удаленных записей</summary>
        public bool p_IsShowDeleted { get; set; }

        private Cl_Record[] m_Records = null;
        private Cl_Record m_SelectedRecord = null;
        private Cl_UserPermission m_Permission = null;
        private WebBrowser m_WebBrowserPrint = new WebBrowser();

        private void f_UpdateRecords()
        {
            try
            {
                var patientID = Cl_SessionFacade.f_GetInstance().p_Patient.p_UserID;
                var patientUID = Cl_SessionFacade.f_GetInstance().p_Patient.p_UserUID;

                var records = Cl_App.m_DataContext.p_Records.AsQueryable();
                if (Cl_SessionFacade.f_GetInstance().p_Doctor.p_Permission.p_IsReadSelectedRecords)
                {
                    if (Cl_SessionFacade.f_GetInstance().p_DateStart != null && Cl_SessionFacade.f_GetInstance().p_DateEnd != null)
                    {
                        var dateStart = Cl_SessionFacade.f_GetInstance().p_DateStart;
                        var dateEnd = Cl_SessionFacade.f_GetInstance().p_DateEnd;
                        records = records.Where(r => r.p_DateLastChange >= dateStart && r.p_DateLastChange <= dateEnd);
                    }
                    else
                        MonitoringStub.Error("Error_Editor", "Для проверяющего С/К не указан период", null, null, null);
                }

                m_Records = records.Where(r => p_IsShowDeleted ? true : !r.p_IsDelete && ((r.p_PatientUID != null && r.p_PatientUID == patientUID) || r.p_PatientID == patientID)).GroupBy(e => e.p_RecordID)
                        .Select(grp => grp
                            .OrderByDescending(v => v.p_Version).FirstOrDefault())
                            .Include(r => r.p_CategoryTotal).Include(r => r.p_CategoryClinic).Include(r => r.p_Values).Include(r => r.p_Template).Include(r => r.p_Values.Select(v => v.p_Params)).ToArray();

                ctrl_TRecords.BindData(null, null);
                ctrl_TRecords.Columns.AddRange(p_MedicalCardID, p_ClinikName, p_DateForming, p_CategoryTotal, p_Title, p_DoctorFIO);

                foreach (var record in m_Records)
                {
                    OutlookGridRow row = new OutlookGridRow();
                    row.CreateCells(ctrl_TRecords,
                        record.p_MedicalCardID,
                        record.p_ClinicName,
                        record.p_DateForming.ToString("dd.MM.yyyy hh:mm"),
                        record.p_CategoryTotal != null ? record.p_CategoryTotal.p_Name : "",
                        record.p_Title,
                        record.p_DoctorFIO);
                    row.Tag = record;
                    ctrl_TRecords.Rows.Add(row);
                }
                ctrl_TRecords.Columns[0].Visible = false;
                ctrl_TRecords.GroupTemplate.Column = ctrl_TRecords.Columns[0];
                ctrl_TRecords.Sort(ctrl_TRecords.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
            }
            catch (Exception er)
            {
                MonitoringStub.Error("Error_Editor", "Не удалось обновить записи", er, null, null);
            }
        }

        private void f_FormatPattern(Cl_Record a_Record)
        {
            if (a_Record != null && !a_Record.p_IsAutomatic && a_Record.p_Template != null)
            {
                //Cl_TemplatesFacade.f_GetInstance().f_LoadTemplatesElements(a_Record.p_Template);
                //Cl_RecordPattern pattern = Cl_RecordsFacade.f_GetInstance().f_GetNewRecordPattern(a_Record);
                //pattern.p_ClinicName = Cl_SessionFacade.f_GetInstance().p_Doctor.p_ClinicName;
                //pattern.f_SetDoctor(Cl_SessionFacade.f_GetInstance().p_Doctor);
                var dlgPattern = new Dlg_RecordPattern();
                //dlgPattern.p_RecordPattern = pattern;
                dlgPattern.FormatPaternFromRecord(a_Record);
                dlgPattern.ShowDialog(this);
            }
        }

        private bool f_GetEdited(Cl_Record a_Record)
        {
            if (a_Record != null)
                return !a_Record.p_IsAutomatic && (Cl_SessionFacade.f_GetInstance().p_Doctor.p_Permission.p_IsEditAllRecords
                    || (Cl_SessionFacade.f_GetInstance().p_Doctor.p_Permission.p_IsEditSelfRecords && a_Record.p_DoctorID == Cl_SessionFacade.f_GetInstance().p_Doctor.p_UserID));
            else
                return false;
        }

        private void f_AddRecordFromRecord(Cl_Record a_Record)
        {
            if (a_Record != null)
            {
                Cl_TemplatesFacade.f_GetInstance().f_LoadTemplatesElements(a_Record.p_Template);
                Cl_Record record = Cl_RecordsFacade.f_GetInstance().f_GetNewRecord(a_Record);
                if (record != null)
                {
                    var dlgRecord = new Dlg_Record();
                    dlgRecord.e_Save += DlgRecord_e_Save;
                    dlgRecord.p_Record = record;
                    dlgRecord.ShowDialog(this);
                }
            }
        }

        private void f_Edit(Cl_Record a_Record)
        {
            if (f_GetEdited(a_Record))
            {
                Cl_EntityLog.f_CustomMessageLog(E_EntityTypes.UIEvents, string.Format("Редактирование записи: {0}, дата записи: {1}, клиника: {2}", a_Record.p_Title, a_Record.p_DateCreate, a_Record.p_ClinicName), a_Record.p_RecordID);

                var dlgRecord = new Dlg_Record();
                dlgRecord.e_Save += DlgRecord_e_Save;
                dlgRecord.p_Record = a_Record;
                dlgRecord.ShowDialog(this);
                Cl_EntityLog.f_CustomMessageLog(E_EntityTypes.UIEvents, string.Format("Выход из редактирования записи: {0}, дата записи: {1}, клиника: {2}", a_Record.p_Title, a_Record.p_DateCreate, a_Record.p_ClinicName), a_Record.p_RecordID);
            }
        }

        private void f_Archive()
        {
            if (m_SelectedRecord != null)
            {
                if (!m_SelectedRecord.p_IsArchive)
                {
                    using (var transaction = Cl_App.m_DataContext.Database.BeginTransaction())
                    {
                        try
                        {
                            Cl_EntityLog log = new Cl_EntityLog();
                            log.f_SetEntity(m_SelectedRecord);
                            m_SelectedRecord.p_IsArchive = true;
                            log.f_SaveEntity(m_SelectedRecord);
                            Cl_App.m_DataContext.SaveChanges();
                            transaction.Commit();
                            ctrlBReportArchive.Visible = ctrlMIArchive.Visible = false;
                        }
                        catch
                        {
                            m_SelectedRecord.p_IsArchive = false;
                            transaction.Rollback();
                            MonitoringStub.Error("Error_Tree", "Не удалось перенести запись в архив", null, null, null);
                        }
                    }
                }
            }
        }

        private void f_Rating()
        {
            if (m_SelectedRecord != null)
            {
                Dlg_RatingViewer viewer = new Dlg_RatingViewer();
                viewer.f_LoadRating(m_SelectedRecord.p_RecordID);
                viewer.ShowDialog(this);
            }
        }

        private void f_SyncBMK()
        {
            if (m_SelectedRecord != null)
            {
                if (!m_SelectedRecord.p_IsSyncBMK)
                {
                    using (var transaction = Cl_App.m_DataContext.Database.BeginTransaction())
                    {
                        try
                        {
                            m_SelectedRecord.p_DateSyncBMK = DateTime.Now;
                            Cl_App.m_DataContext.SaveChanges();
                            Cl_EntityLog.f_CustomMessageLog(m_SelectedRecord, "Синхронизация записи с БМК");
                            transaction.Commit();
                            ctrlBReportSyncBMK.Visible = ctrlMISyncBMK.Visible = false;
                        }
                        catch
                        {
                            m_SelectedRecord.p_DateSyncBMK = null;
                            transaction.Rollback();
                            MonitoringStub.Error("Error_Tree", "Не удалось синхронизировать запись с БМК", null, null, null);
                        }
                    }
                }
            }
        }

        private bool p_IsPrintDoctor = false;
        private void f_PrintDoctor()
        {
            if (m_SelectedRecord != null)
            {
                if (m_SelectedRecord.p_FileType == E_RecordFileType.HTML)
                {
                    p_IsPrintDoctor = true;
                    m_WebBrowserPrint.DocumentText = m_SelectedRecord.f_GetDocumentTextDoctor(Application.StartupPath);
                    Cl_EntityLog.f_CustomMessageLog(m_SelectedRecord, "Печать карточки для доктора" + (!m_SelectedRecord.p_IsPrintDoctor ? " (первая печать)" : ""));
                }
            }
        }

        private void f_PrintPatient()
        {
            if (m_SelectedRecord != null)
            {
                if (m_SelectedRecord.p_FileType == E_RecordFileType.HTML)
                {
                    p_IsPrintDoctor = false;
                    m_WebBrowserPrint.DocumentText = m_SelectedRecord.f_GetDocumentTextPatient(Application.StartupPath);
                    Cl_EntityLog.f_CustomMessageLog(m_SelectedRecord, "Печать карточки для пациента" + (!m_SelectedRecord.p_IsPrintPatient ? " (первая печать)" : ""));
                }
            }
        }

        private void ctrlBReportAddRecord_Click(object sender, System.EventArgs e)
        {
            var dlg = new Dlg_RecordSelectSource();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    if (dlg.p_SelectedTemplate != null)
                    {
                        Cl_Record record = new Cl_Record();
                        record.p_DateCreate = DateTime.Now;
                        record.p_DateLastChange = record.p_DateForming = record.p_DateCreate;
                        record.f_SetTemplate(dlg.p_SelectedTemplate);
                        record.p_MedicalCardID = Cl_SessionFacade.f_GetInstance().p_MedCardNumber;
                        record.p_ClinicName = Cl_SessionFacade.f_GetInstance().p_Doctor.p_ClinicName;
                        record.f_SetDoctor(Cl_SessionFacade.f_GetInstance().p_Doctor);
                        record.f_SetPatient(Cl_SessionFacade.f_GetInstance().p_Patient);
                        var dlgRecord = new Dlg_Record();
                        dlgRecord.e_Save += DlgRecord_e_Save;
                        dlgRecord.p_Record = record;
                        dlgRecord.ShowDialog(this);
                    }
                    else if (dlg.p_SelectedRecordPattern != null)
                    {
                        if (dlg.p_SelectedRecordPattern.p_Template != null)
                        {
                            Cl_TemplatesFacade.f_GetInstance().f_LoadTemplatesElements(dlg.p_SelectedRecordPattern.p_Template);
                            Cl_Record record = Cl_RecordsFacade.f_GetInstance().f_GetNewRecord(dlg.p_SelectedRecordPattern);
                            if (record != null)
                            {
                                var dlgRecord = new Dlg_Record();
                                dlgRecord.e_Save += DlgRecord_e_Save;
                                dlgRecord.p_Record = record;
                                dlgRecord.ShowDialog(this);
                            }
                        }
                    }
                }
                catch (Exception er)
                {
                    MonitoringStub.Error("Error_Editor", "Не удалось добавить запись", er, null, null);
                }
            }
        }

        private void ctrlBAddRecordFromRecord_Click(object sender, EventArgs e)
        {
            f_AddRecordFromRecord(m_SelectedRecord);
        }

        private void ctrlBReportAddPattern_Click(object sender, EventArgs e)
        {
            var dlg = new Dlg_RecordPatternSelectSource();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                if (dlg.p_SelectedTemplate != null)
                {
                    try
                    {
                        Cl_RecordPattern pattern = new Cl_RecordPattern();
                        pattern.p_ClinicName = Cl_SessionFacade.f_GetInstance().p_Doctor.p_ClinicName;
                        pattern.f_SetDoctor(Cl_SessionFacade.f_GetInstance().p_Doctor);
                        pattern.f_SetTemplate(dlg.p_SelectedTemplate);
                        var dlgPattern = new Dlg_RecordPattern();
                        dlgPattern.p_RecordPattern = pattern;
                        dlgPattern.ShowDialog(this);
                    }
                    catch (Exception er)
                    {
                        MonitoringStub.Error("Error_Editor", "Не удалось добавить патерн", er, null, null);
                    }
                }
            }
        }

        private void ctrlBReportFormatPattern_Click(object sender, EventArgs e)
        {
            f_FormatPattern(m_SelectedRecord);
        }

        private void DlgRecord_e_Save(object sender, EventArgs e)
        {
            f_UpdateRecords();
        }

        private void ctrlBReportEdit_Click(object sender, EventArgs e)
        {
            f_Edit(m_SelectedRecord);
        }

        private void ctrlBReportArchive_Click(object sender, EventArgs e)
        {
            f_Archive();
        }

        private void ctrlBReportRating_Click(object sender, EventArgs e)
        {
            f_Rating();
        }

        private void ctrlBReportSyncBMK_Click(object sender, EventArgs e)
        {
            f_SyncBMK();
        }

        private void ctrlBReportPrintDoctor_Click(object sender, EventArgs e)
        {
            f_PrintDoctor();
        }

        private void ctrlBReportPrintPatient_Click(object sender, EventArgs e)
        {
            f_PrintPatient();
        }

        private void ctrl_TRecords_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ctrl_TRecords.CurrentRow != null && ctrl_TRecords.CurrentRow is OutlookGridRow && !((OutlookGridRow)ctrl_TRecords.CurrentRow).IsGroupRow && ctrl_TRecords.CurrentRow.Tag != null)
            {
                f_Edit(m_Records.FirstOrDefault(r => r.p_ID == ((Cl_Record)ctrl_TRecords.CurrentRow.Tag).p_ID));
            }
        }

        private void ctrl_TRecords_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            m_SelectedRecord = null;
            if (ctrl_TRecords.CurrentRow != null && ctrl_TRecords.CurrentRow is OutlookGridRow && !((OutlookGridRow)ctrl_TRecords.CurrentRow).IsGroupRow && ctrl_TRecords.CurrentRow.Tag != null)
            {
                try
                {
                    var record = m_SelectedRecord = m_Records.FirstOrDefault(r => r.p_ID == ((Cl_Record)ctrl_TRecords.CurrentRow.Tag).p_ID);
                    if (record != null)
                    {
                        Cl_EntityLog.f_CustomMessageLog(E_EntityTypes.UIEvents, string.Format("Просмотр записи: {0}, дата записи: {1}, клиника: {2}", record.p_Title, record.p_DateCreate, record.p_ClinicName), record.p_RecordID);

                        ctrlPRecordInfo.Visible = true;
                        ctrlRecordInfo.Text = string.Format("{0} {1} [{2}, {3}]", record.p_DateCreate.ToShortDateString(), record.p_Title, record.p_DateLastChange, record.p_DoctorFIO);

                        ctrlCMViewer.Enabled = true;
                        ctrlBAddRecordFromRecord.Visible = ctrlBReportFormatPattern.Visible = !record.p_IsAutomatic && (m_Permission.p_IsEditAllRecords || m_Permission.p_IsEditSelfRecords);
                        ctrlBReportEdit.Visible = ctrlMIEdit.Visible = f_GetEdited(record);
                        ctrlBReportArchive.Visible = ctrlMIArchive.Visible = !record.p_IsArchive && m_Permission.p_IsEditArchive;
                        ctrlBReportRating.Visible = ctrlMIRating.Visible = m_Permission.p_IsEditAllRatings;
                        ctrlBReportSyncBMK.Visible = ctrlMISyncBMK.Visible = !record.p_IsSyncBMK && record.p_IsPrintDoctor && m_Permission.p_IsEditArchive;
                        ctrlBReportPrintDoctor.Visible = ctrlBReportPrintPatient.Visible = ctrlMIPrint.Visible = m_Permission.p_IsPrint;
                        if (record.p_HTMLDoctor != null)
                        {
                            ctrlHTMLViewer.DocumentText = record.f_GetDocumentTextDoctor(Application.StartupPath);
                            ctrlHTMLViewer.Visible = true;
                            ctrlPDFViewer.Visible = false;
                        }
                        else
                        {
                            if (record.p_Type == E_RecordType.FinishedFile)
                            {
                                if (record.p_FileType == E_RecordFileType.HTML)
                                {
                                    ctrlHTMLViewer.DocumentText = record.f_GetDocumentTextDoctor(Application.StartupPath);
                                    ctrlHTMLViewer.Visible = true;
                                    ctrlPDFViewer.Visible = false;
                                }
                                else if (record.p_FileType == E_RecordFileType.PDF)
                                {
                                    var path = string.Format("{0}medicalChartTemp.pdf", Path.GetTempPath());
                                    File.WriteAllBytes(path, record.p_FileBytes);
                                    ctrlPDFViewer.src = path;
                                    ctrlHTMLViewer.Visible = false;
                                    ctrlPDFViewer.Visible = true;
                                }
                                else if (record.p_FileType == E_RecordFileType.JFIF || record.p_FileType == E_RecordFileType.JIF || record.p_FileType == E_RecordFileType.JPE ||
                                    record.p_FileType == E_RecordFileType.JPEG || record.p_FileType == E_RecordFileType.JPG || record.p_FileType == E_RecordFileType.PNG)
                                {
                                    ctrlHTMLViewer.DocumentText = record.f_GetDocumentTextDoctor(Application.StartupPath);
                                    ctrlHTMLViewer.Visible = true;
                                    ctrlPDFViewer.Visible = false;
                                }
                            }
                            else
                            {
                                ctrlHTMLViewer.Visible = false;
                                ctrlPDFViewer.Visible = false;
                            }
                        }
                    }
                    else
                    {
                        ctrlPRecordInfo.Visible = false;
                    }
                }
                catch (Exception er)
                {
                    MonitoringStub.Error("Error_Editor", "Не удалось отобразить запись", er, null, null);
                }
            }
            if (m_SelectedRecord == null)
            {
                ctrlCMViewer.Enabled = false;
                ctrlPRecordInfo.Visible = false;
                ctrlBReportFormatPattern.Visible = false;
                ctrlBReportEdit.Visible = false;
                ctrlBReportArchive.Visible = false;
                ctrlBReportRating.Visible = false;
                ctrlBReportPrintDoctor.Visible = ctrlBReportPrintPatient.Visible = false;
            }
        }

        private void ctrlMIEdit_Click(object sender, EventArgs e)
        {
            f_Edit(m_SelectedRecord);
        }

        private void ctrlMIArhive_Click(object sender, EventArgs e)
        {
            f_Archive();
        }

        private void ctrlMIRating_Click(object sender, EventArgs e)
        {
            f_Rating();
        }

        private void ctrlMISyncBMK_Click(object sender, EventArgs e)
        {
            f_SyncBMK();
        }

        private void ctrlMIPrintDoctor_Click(object sender, EventArgs e)
        {
            f_PrintDoctor();
        }

        private void ctrlMIPrintPatient_Click(object sender, EventArgs e)
        {
            f_PrintPatient();
        }

        private void M_WebBrowserPrint_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (m_SelectedRecord != null)
            {
                m_WebBrowserPrint.ShowPrintPreviewDialog();
                if (p_IsPrintDoctor)
                {
                    if (!m_SelectedRecord.p_IsPrintDoctor)
                    {
                        m_SelectedRecord.p_DatePrintDoctor = DateTime.Now;
                        Cl_App.m_DataContext.SaveChanges();
                    }
                }
                else
                {
                    if (!m_SelectedRecord.p_IsPrintPatient)
                    {
                        m_SelectedRecord.p_DatePrintPatient = DateTime.Now;
                        Cl_App.m_DataContext.SaveChanges();
                    }
                }
            }
        }
    }
}
