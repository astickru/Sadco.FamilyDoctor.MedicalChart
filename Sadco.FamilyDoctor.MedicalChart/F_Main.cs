using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.MedicalChart.Forms.SubForms;
using System;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart
{
	public partial class F_Main : Form
	{
		private UI_PanelManager panelManager = null;

		public F_Main() {
			Cl_App.Initialize();

			InitializeComponent();

			panelManager = new UI_PanelManager(ctrl_CustomControls);
			setControl<UC_EditorTemplates>();
		}

		private void ctrl_MenuShowTemplates_Click(object sender, EventArgs e) {
			setControl<UC_EditorTemplates>();
		}

		private void ctrl_MenuShowElements_Click(object sender, EventArgs e) {
			setControl<UC_EditorElements>();
		}

		private void setControl<T>() where T : UserControl {
			this.Text = panelManager.SetControl<T>().Tag.ToString();
		}

		private void ctrlMIExit_Click(object sender, EventArgs e) {
			this.Close();
		}
	}
}
