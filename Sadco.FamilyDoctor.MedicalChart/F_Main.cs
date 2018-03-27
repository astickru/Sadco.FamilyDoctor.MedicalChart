using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.MedicalChart.Forms.SubForms;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart
{
    public partial class F_Main : Form
    {
        private UI_PanelManager p_PanelManager = null;

        public F_Main()
        {
            Tag = string.Format("Мегашаблон v{0}", ConfigurationManager.AppSettings["Version"]);
            Cl_App.Initialize();
            this.Font = new System.Drawing.Font(ConfigurationManager.AppSettings["FontFamily"],
                float.Parse(ConfigurationManager.AppSettings["FontSize"]),
                (System.Drawing.FontStyle)int.Parse(ConfigurationManager.AppSettings["FontStyle"]),
                System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            InitializeComponent();
            p_PanelManager = new UI_PanelManager(ctrl_CustomControls);
            f_SetControl<UC_EditorTemplates>();
        }

        private void ctrl_MenuShowTemplates_Click(object sender, EventArgs e)
        {
            f_SetControl<UC_EditorTemplates>();
        }

        private void ctrl_MenuShowElements_Click(object sender, EventArgs e)
        {
            f_SetControl<UC_EditorElements>();
        }

        private void f_SetControl<T>() where T : UserControl
        {
            this.Text = p_PanelManager.f_SetElement<T>().Tag.ToString();
        }

        private void ctrlMIExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
