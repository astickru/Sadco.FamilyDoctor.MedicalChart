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
            f_UpdateRecords();
        }

        /// <summary>Флаг отображения удаленных записей</summary>
        public bool p_IsShowDeleted { get; set; }

        private Cl_Record[] m_Records = null;

        private void f_UpdateRecords()
        {
            m_Records = Cl_App.m_DataContext.p_Records.Where(r => p_IsShowDeleted ? true : !r.p_IsDelete).GroupBy(e => e.p_RecordID)
                    .Select(grp => grp
                        .OrderByDescending(v => v.p_Version).FirstOrDefault())
                        .Include(r => r.p_CategoryTotal).Include(r => r.p_CategoryClinik).Include(r => r.p_Values).Include(r => r.p_Template).Include(r => r.p_Values.Select(v => v.p_Params)).ToArray();


            DataTable dt = new DataTable();
            foreach (DataGridViewColumn col in ctrl_TPartNormRangeValues.Columns)
            {
                dt.Columns.Add(col.Name);
                col.DataPropertyName = col.Name;
            };
            foreach (var norm in m_Records)
            {
                var row = dt.NewRow();
                row["p_ClinikName"] = norm.p_ClinikName;
                row["p_DateForming"] = norm.p_DateForming;
                row["p_CategoryTotal"] = norm.p_CategoryTotal != null ? norm.p_CategoryTotal.p_Name : "";
                row["p_Title"] = norm.p_Title;
                row["p_UserFIO"] = norm.p_UserFIO;
                dt.Rows.Add(row);
            }
            ctrl_TPartNormRangeValues.DataSource = dt;
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

        private void ctrl_TPartNormRangeValues_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (m_Records.Length > e.RowIndex)
            {
                var record = m_Records[e.RowIndex];
                var dlgRecord = new Dlg_Record();
                dlgRecord.e_Save += DlgRecord_e_Save;
                dlgRecord.p_Record = record;
                dlgRecord.ShowDialog(this);
            }
        }

        private void DlgRecord_e_Save(object sender, EventArgs e)
        {
            f_UpdateRecords();
        }

        void OpenWebPage(ref WebBrowser _webBrowser, string _link)
        {
            if (!_webBrowser.IsDisposed)
                _webBrowser.Dispose();

            _webBrowser = new WebBrowser();
            panel2.Controls.Add(_webBrowser);
            _webBrowser.Dock = DockStyle.Fill;
            _webBrowser.Show();
            _webBrowser.Navigate(_link);
        }

        private void ctrl_TPartNormRangeValues_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (m_Records.Length > e.RowIndex)
            {
                var record = m_Records[e.RowIndex];
                if (record.p_HTMLUser != null)
                    ctrlViewer.DocumentText = record.p_HTMLUser.Replace("src=\"", "src=\"file:///" + Application.StartupPath + "/");
                else
                    ctrlViewer.DocumentText = "";


                if (e.ColumnIndex > 2)
                {
                    ctrlViewer.Visible = false;
                    ctrlPDFViewer.Visible = true;
                    ctrlPDFViewer.src = "http://kaskad-asu.com/images/files/uch/lekcii.pdf#navpanes=0&toolbar=0";
                }
                else
                {
                    ctrlViewer.Visible = true;
                    ctrlPDFViewer.Visible = false;
                }

                //var sds = new AcroPDFLib.AcroPDF();
                //Controls.Add(sds);
                //sds.src = "http://kaskad-asu.com/images/files/uch/lekcii.pdf#navpanes=0&toolbar=0";

                //OpenWebPage(ref ctrlViewer, "http://kaskad-asu.com/images/files/uch/lekcii.pdf#navpanes=0&toolbar=0");
            }
        }
    }
}
