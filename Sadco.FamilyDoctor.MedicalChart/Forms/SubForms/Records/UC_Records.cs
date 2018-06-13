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
            ctrlLPatientName.Text = Cl_SessionFacade.f_GetInstance().p_Patient.p_FIO;
            m_WebBrowserPrint.DocumentCompleted += M_WebBrowserPrint_DocumentCompleted;
            f_UpdateRecords();
        }

        /// <summary>Флаг отображения удаленных записей</summary>
        public bool p_IsShowDeleted { get; set; }

        private Cl_Record[] m_Records = null;
        private Cl_Record m_SelectedRecord = null;
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
            ctrl_TRecords.Columns.AddRange(p_MedicalCardID, p_ClinikName, p_DateForming, p_CategoryTotal, p_Title, p_UserFIO);
            foreach (var record in m_Records)
            {
                OutlookGridRow row = new OutlookGridRow();
                row.CreateCells(ctrl_TRecords,
                    record.p_MedicalCardID,
                    record.p_ClinikName,
                    record.p_DateForming,
                    record.p_CategoryTotal != null ? record.p_CategoryTotal.p_Name : "",
                    record.p_Title,
                    record.p_UserFIO);
                ctrl_TRecords.Rows.Add(row);
            }
            ctrl_TRecords.GroupTemplate.Column = ctrl_TRecords.Columns[0];
            ctrl_TRecords.Sort(ctrl_TRecords.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
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
                    record.f_SetUser(Cl_SessionFacade.f_GetInstance().p_User);
                    record.f_SetPatient(Cl_SessionFacade.f_GetInstance().p_Patient);
                    var dlgRecord = new Dlg_Record();
                    dlgRecord.e_Save += DlgRecord_e_Save;
                    dlgRecord.p_Record = record;
                    dlgRecord.ShowDialog(this);
                }
            }
        }

        private void f_Open(Cl_Record a_Record)
        {
            if (a_Record != null && !a_Record.p_IsAutimatic)
            {
                var dlgRecord = new Dlg_Record();
                dlgRecord.e_Save += DlgRecord_e_Save;
                dlgRecord.p_Record = a_Record;
                dlgRecord.ShowDialog(this);
            }
        }

        private void ctrl_TRecords_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (m_Records.Length > e.RowIndex)
            {
                f_Open(m_Records[e.RowIndex]);
            }
        }

        private void DlgRecord_e_Save(object sender, EventArgs e)
        {
            f_UpdateRecords();
        }


        private void ctrl_TRecords_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && m_Records.Length > e.RowIndex)
            {
                var record = m_SelectedRecord = m_Records[e.RowIndex];
                ctrlCMViewer.Enabled = true;
                ctrlMIArchive.Visible = !record.p_IsArchive && Cl_SessionFacade.f_GetInstance().p_User.p_Permission.p_IsEditArchive;
                if (record.p_HTMLUser != null)
                {
                    ctrlHTMLViewer.DocumentText = record.f_GetDocumentText(Application.StartupPath);
                    ctrlHTMLViewer.Visible = true;
                    ctrlPDFViewer.Visible = false;
                }
                else
                {
                    if (record.p_Type == Cl_Record.E_RecordType.FinishedFile)
                    {
                        if (record.p_FileType == Cl_Record.E_RecordFileType.HTML)
                        {
                            ctrlHTMLViewer.DocumentText = record.f_GetDocumentText(Application.StartupPath);
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
                            ctrlHTMLViewer.DocumentText = record.f_GetDocumentText(Application.StartupPath);
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
                m_SelectedRecord = null;
                ctrlCMViewer.Enabled = false;
                ctrlMIArchive.Visible = false;
            }
        }

        private void ctrlMIOpen_Click(object sender, EventArgs e)
        {
            f_Open(m_SelectedRecord);
        }

        private void ctrlMIArhive_Click(object sender, EventArgs e)
        {
            if (m_SelectedRecord != null)
            {
                if (!m_SelectedRecord.p_IsArchive)
                {
                    m_SelectedRecord.p_IsArchive = true;
                    Cl_App.m_DataContext.SaveChanges();
                }
                ctrlMIArchive.Visible = false;
            }
        }

        private void ctrlMIPrint_Click(object sender, EventArgs e)
        {
            if (m_SelectedRecord != null)
            {
                if (m_SelectedRecord.p_FileType == Cl_Record.E_RecordFileType.HTML)
                {
                    m_WebBrowserPrint.DocumentText = m_SelectedRecord.f_GetDocumentText(Application.StartupPath);
                }
            }
        }

        private void M_WebBrowserPrint_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (m_SelectedRecord != null)
            {
                m_WebBrowserPrint.Print();
                if (!m_SelectedRecord.p_IsPrint)
                {
                    m_SelectedRecord.p_IsPrint = true;
                    Cl_App.m_DataContext.SaveChanges();
                }
            }
        }
    }
}
