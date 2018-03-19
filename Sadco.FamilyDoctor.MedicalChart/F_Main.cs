using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.MedicalChart.Forms.SubForms;
using System;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart
{
	public partial class F_Main : Form
	{
		private UI_PanelManager p_PanelManager = null;

		public F_Main() {
			Cl_App.Initialize();
			InitializeComponent();
			p_PanelManager = new UI_PanelManager(ctrl_CustomControls);
			setControl<UC_EditorTemplates>();
		}

		private void ctrl_MenuShowTemplates_Click(object sender, EventArgs e) {
			setControl<UC_EditorTemplates>();
		}

		private void ctrl_MenuShowElements_Click(object sender, EventArgs e) {
			setControl<UC_EditorElements>();
		}

		private void setControl<T>() where T : UserControl {
			this.Text = p_PanelManager.f_SetElement<T>().Tag.ToString();
		}

		private void ctrlMIExit_Click(object sender, EventArgs e) {
			this.Close();
		}
	}
}
