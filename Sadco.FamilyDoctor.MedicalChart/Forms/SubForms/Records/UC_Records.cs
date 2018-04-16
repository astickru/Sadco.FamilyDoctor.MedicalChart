using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Controls;
using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.Core.Permision;
using Sadco.FamilyDoctor.MedicalChart.Forms.SubForms.Elements.Editors;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
    public partial class UC_Records : UserControl
    {
        private UI_PanelManager m_PanelManager = null;

        public UC_Records()
        {
            Tag = string.Format("Записи клиента v{0}", ConfigurationManager.AppSettings["Version"]);
            InitializeComponent();
            ctrlLPatientName.Text = UserSession.PatientName;
            //f_InitTreeView();
            //m_PanelManager = new UI_PanelManager(ctrl_P_ElementProperty);
        }

        private void ctrlBReportAdd_Click(object sender, System.EventArgs e)
        {
            var dlg = new Dlg_SelectTemplate();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                if (dlg.p_SelectedTemplate != null)
                {
                    Cl_Record record = new Cl_Record();
                    record.p_Template = dlg.p_SelectedTemplate;
                    record.p_UserName = UserSession.Name;
                    record.p_PatientName = UserSession.PatientName;
                    var dlgRecord = new Dlg_Record();
                    dlgRecord.p_Record = record;
                    dlgRecord.ShowDialog(this);
                }
            }
        }
    }
}
