using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Sadco.FamilyDoctor.Core.Entities.Controls;
using Sadco.FamilyDoctor.MedicalChart.Forms.MegaTemplate;

namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
	public partial class UC_EditorTextual : UserControl, I_EditPanel
	{
		Cl_CtrlTextual editingControl = null;

		public UC_EditorTextual() {
			InitializeComponent();

			f_SetSelectTypes();
		}

		private void f_SetSelectTypes() {
			ctrl_CB_ControlType.Items.Clear();

			foreach (TextControlTypes suit in Enum.GetValues(typeof(TextControlTypes))) {
				ctrl_CB_ControlType.Items.Add(suit);
			}
		}

		public void ConfirmChanges() {
			editingControl.p_ControlType = (TextControlTypes)ctrl_CB_ControlType.SelectedItem;

			if (editingControl.f_ValueType() == ValueTypes.Single)
				editingControl.p_Text = ctrl_TB_Text.Text;
			else if(editingControl.f_ValueType() == ValueTypes.Multi) {
				editingControl.p_Elements.Clear();
				foreach (ListViewItem item in ctrl_LVTemplates.Items)
					editingControl.p_Elements.Add(item.Text);
			}
		}

		public void SetControl(I_BaseControl p_Control) {
			if (!(p_Control is Cl_CtrlTextual)) return;

			editingControl = (Cl_CtrlTextual)p_Control;

			ctrl_CB_ControlType.SelectedItem = editingControl.p_ControlType;
		}

		private void f_LoadTextValue() {
			ctrl_P_Text.Visible = true;
			ctrl_P_TableText.Visible = false;

			if (editingControl.f_ValueType() == ValueTypes.Single)
				ctrl_TB_Text.Text = editingControl.p_Text;
			else
				ctrl_TB_Text.Text = "";
		}

		private void f_LoadArrayValue() {
			ctrl_P_Text.Visible = false;
			ctrl_P_TableText.Visible = true;

			ctrl_LVTemplates.Items.Clear();

			if (editingControl.f_ValueType() == ValueTypes.Multi) {
				foreach (string item in editingControl.p_Elements)
					ctrl_LVTemplates.Items.Add(item);
			}
		}

		private void ctrl_CB_ControlType_SelectedValueChanged(object sender, EventArgs e) {
			TextControlTypes SelectTypePanel = (TextControlTypes)((ComboBox)sender).SelectedItem;

			if (SelectTypePanel == TextControlTypes.Text || SelectTypePanel == TextControlTypes.CheckBox)
				f_LoadTextValue();
			else
				f_LoadArrayValue();
		}

		private void ctrl_B_Add_Click(object sender, EventArgs e) {
			F_Template fTemplate = new F_Template();
			if (fTemplate.ShowDialog() != DialogResult.OK) return;

			ctrl_LVTemplates.Items.Add(fTemplate.ctrl_TBName.Text);
		}

		private void ctrl_B_Delete_Click(object sender, EventArgs e) {
			List<int> selItems = new List<int>();

			foreach (int item in ctrl_LVTemplates.SelectedIndices) {
				selItems.Add(item);
			}

			foreach (int index in selItems) {
				ctrl_LVTemplates.Items.RemoveAt(index);
			}
		}
	}
}
