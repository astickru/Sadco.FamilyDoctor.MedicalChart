using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart
{
	public partial class F_Main : Form
	{
		public F_Main() {
			InitializeComponent();
			AddUnit(0);
		}

		private void ctrl_MenuShowTemplates_Click(object sender, EventArgs e) {
			AddUnit(0);
		}

		private void ctrl_MenuShowElements_Click(object sender, EventArgs e) {
			AddUnit(1);
		}

		private void ctrlMIExit_Click(object sender, EventArgs e) {
			this.Close();
		}

		#region Unit change handler
		public UserControl currentUnit = null;

		public void ChangeUnit(object sender, EventArgs e) { AddUnit(0); }

		public void AddUnit(int newUnit) {
			if (currentUnit != null)
				DeleteUnit();

			switch (newUnit) {
				case 0x00: { this.currentUnit = new UC_EditorElements(); break; }
				case 0x01: { this.currentUnit = new UC_EditorTemplates(); break; }
				default: { AddUnit(0); return; }
			}
			
			this.ctrl_CustomControls.Controls.Add(this.currentUnit);
		}

		public void DeleteUnit() {
			this.currentUnit.Dispose();
			this.ctrl_CustomControls.Controls.Remove(currentUnit);
		}
		#endregion
	}
}
