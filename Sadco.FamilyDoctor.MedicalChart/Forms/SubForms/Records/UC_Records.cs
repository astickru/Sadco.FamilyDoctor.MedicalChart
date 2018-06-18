using OutlookStyleControls;
using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Controls;
using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.Core.Facades;
using Sadco.FamilyDoctor.Core.Permision;
using Sadco.FamilyDoctor.MedicalChart.Forms.SubForms.Elements.Editors;
using System;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
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
            var patientID = Cl_SessionFacade.f_GetInstance().p_Patient.p_UserID;
            var patientUID = Cl_SessionFacade.f_GetInstance().p_Patient.p_UserUID;
            m_Records = Cl_App.m_DataContext.p_Records.Where(r => p_IsShowDeleted ? true : !r.p_IsDelete && ((r.p_PatientUID != null && r.p_PatientUID == patientUID) || r.p_PatientID == patientID)).GroupBy(e => e.p_RecordID)
                    .Select(grp => grp
                        .OrderByDescending(v => v.p_Version).FirstOrDefault())
                        .Include(r => r.p_CategoryTotal).Include(r => r.p_CategoryClinik).Include(r => r.p_Values).Include(r => r.p_Template).Include(r => r.p_Values.Select(v => v.p_Params)).ToArray();

            ctrl_TRecords.BindData(null, null);
            ctrl_TRecords.Columns.AddRange(p_MedicalCardID, p_ClinikName, p_DateForming, p_CategoryTotal, p_Title, p_DoctorFIO);

            foreach (var record in m_Records)
            {
                OutlookGridRow row = new OutlookGridRow();
                row.CreateCells(ctrl_TRecords,
                    record.p_MedicalCardID,
                    record.p_ClinikName,
                    record.p_DateForming,
                    record.p_CategoryTotal != null ? record.p_CategoryTotal.p_Name : "",
                    record.p_Title,
                    record.p_DoctorFIO);
                row.Tag = record;
                ctrl_TRecords.Rows.Add(row);
            }
            ctrl_TRecords.GroupTemplate.Column = ctrl_TRecords.Columns[0];
            ctrl_TRecords.Sort(ctrl_TRecords.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
        }

        private void f_Edit(Cl_Record a_Record)
        {
            if (a_Record != null && !a_Record.p_IsAutimatic)
            {
                var dlgRecord = new Dlg_Record();
                dlgRecord.e_Save += DlgRecord_e_Save;
                dlgRecord.p_Record = a_Record;
                dlgRecord.ShowDialog(this);
            }
        }

        private void f_Archive()
        {
            if (m_SelectedRecord != null)
            {
                if (!m_SelectedRecord.p_IsArchive)
                {
                    m_SelectedRecord.p_IsArchive = true;
                    Cl_App.m_DataContext.SaveChanges();
                }
                ctrlBReportArchive.Visible = ctrlMIArchive.Visible = false;
            }
        }

        private void f_Rating()
        {
            if (m_SelectedRecord != null)
            {
                Dlg_RatingViewer viewer = new Dlg_RatingViewer();
                viewer.LoadRating(m_SelectedRecord.p_RecordID);
                viewer.ShowDialog(this);
            }
        }

        private void f_SyncBMK()
        {
            if (m_SelectedRecord != null)
            {
                if (!m_SelectedRecord.p_IsSyncBMK)
                {
                    m_SelectedRecord.p_DateSyncBMK = DateTime.Now;
                    Cl_App.m_DataContext.SaveChanges();
                }
                ctrlBReportSyncBMK.Visible = ctrlMISyncBMK.Visible = false;
            }
        }

        private bool p_IsPrintDoctor = false;
        private void f_PrintDoctor()
        {
            if (m_SelectedRecord != null)
            {
                if (m_SelectedRecord.p_FileType == Cl_Record.E_RecordFileType.HTML)
                {
                    p_IsPrintDoctor = true;
                    m_WebBrowserPrint.DocumentText = m_SelectedRecord.f_GetDocumentTextDoctor(Application.StartupPath);
                }
            }
        }

        private void f_PrintPatient()
        {
            if (m_SelectedRecord != null)
            {
                if (m_SelectedRecord.p_FileType == Cl_Record.E_RecordFileType.HTML)
                {
                    p_IsPrintDoctor = false;
                    m_WebBrowserPrint.DocumentText = m_SelectedRecord.f_GetDocumentTextPatient(Application.StartupPath);
                }
            }
        }

        private void ctrlBReportAdd_Click(object sender, System.EventArgs e)
        {
            var dlg = new Dlg_SelectTemplate();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                if (dlg.p_SelectedTemplate != null)
                {
                    Cl_Record record = new Cl_Record();
                    record.p_DateCreate = DateTime.Now;
                    record.p_DateLastChange = record.p_DateForming = record.p_DateCreate;
                    record.f_SetTemplate(dlg.p_SelectedTemplate);
                    record.p_MedicalCardID = Cl_SessionFacade.f_GetInstance().p_MedCardNumber;
                    record.p_ClinikName = Cl_SessionFacade.f_GetInstance().p_Doctor.p_ClinikName;
                    record.f_SetDoctor(Cl_SessionFacade.f_GetInstance().p_Doctor);
                    record.f_SetPatient(Cl_SessionFacade.f_GetInstance().p_Patient);
                    var dlgRecord = new Dlg_Record();
                    dlgRecord.e_Save += DlgRecord_e_Save;
                    dlgRecord.p_Record = record;
                    dlgRecord.ShowDialog(this);
                }
            }
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

        private void DlgRecord_e_Save(object sender, EventArgs e)
        {
            f_UpdateRecords();
        }


        private void ctrl_TRecords_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            m_SelectedRecord = null;
            if (ctrl_TRecords.CurrentRow != null && ctrl_TRecords.CurrentRow is OutlookGridRow && !((OutlookGridRow)ctrl_TRecords.CurrentRow).IsGroupRow && ctrl_TRecords.CurrentRow.Tag != null)
            {
                var record = m_SelectedRecord = m_Records.FirstOrDefault(r => r.p_ID == ((Cl_Record)ctrl_TRecords.CurrentRow.Tag).p_ID);
                if (record != null)
                {
                    ctrlCMViewer.Enabled = true;
                    ctrlBReportEdit.Visible = ctrlMIEdit.Visible = true;
                    ctrlBReportArchive.Visible = ctrlMIArchive.Visible = !record.p_IsArchive && m_Permission.p_IsEditArchive;
                    ctrlBReportRating.Visible = ctrlMIRating.Visible = m_Permission.p_IsEditAllRatings;
                    ctrlBReportSyncBMK.Visible = ctrlMISyncBMK.Visible = !record.p_IsSyncBMK && record.p_IsPrintDoctor && m_Permission.p_IsEditArchive;
                    ctrlBReportPrintDoctor.Visible = ctrlBReportPrintPatient.Visible = ctrlMIPrint.Visible = m_Permission.p_Role == E_Roles.ChiefDoctor || m_Permission.p_Role == E_Roles.ChiefUnitDoctor || m_Permission.p_Role == E_Roles.Doctor
                        || m_Permission.p_Role == E_Roles.Expert || m_Permission.p_Role == E_Roles.Archivarius;
                    if (record.p_HTMLDoctor != null)
                    {
                        ctrlHTMLViewer.DocumentText = record.f_GetDocumentTextDoctor(Application.StartupPath);
                        ctrlHTMLViewer.Visible = true;
                        ctrlPDFViewer.Visible = false;
                    }
                    else
                    {
                        if (record.p_Type == Cl_Record.E_RecordType.FinishedFile)
                        {
                            if (record.p_FileType == Cl_Record.E_RecordFileType.HTML)
                            {
                                ctrlHTMLViewer.DocumentText = record.f_GetDocumentTextDoctor(Application.StartupPath);
                                ctrlHTMLViewer.Visible = true;
                                ctrlPDFViewer.Visible = false;
                            }
                            else if (record.p_FileType == Cl_Record.E_RecordFileType.PDF)
                            {
                                var path = string.Format("{0}medicalChartTemp.pdf", Path.GetTempPath());
                                File.WriteAllBytes(path, record.p_FileBytes);
                                ctrlPDFViewer.src = path;
                                ctrlHTMLViewer.Visible = false;
                                ctrlPDFViewer.Visible = true;
                            }
                            else if (record.p_FileType == Cl_Record.E_RecordFileType.JFIF || record.p_FileType == Cl_Record.E_RecordFileType.JIF || record.p_FileType == Cl_Record.E_RecordFileType.JPE ||
                                record.p_FileType == Cl_Record.E_RecordFileType.JPEG || record.p_FileType == Cl_Record.E_RecordFileType.JPG || record.p_FileType == Cl_Record.E_RecordFileType.PNG)
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
            }
            if (m_SelectedRecord == null)
            {
                ctrlCMViewer.Enabled = false;
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
