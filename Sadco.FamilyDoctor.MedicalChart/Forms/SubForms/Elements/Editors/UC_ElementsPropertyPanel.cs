using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Entities.Controls;
using Sadco.FamilyDoctor.MedicalChart.Entities.Controls;
using System;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms.Elements.Editors
{
	public partial class UC_ElementsPropertyPanel : UserControl
	{
		private UI_PanelManager panelManager = null;
		private Cl_CtrlControlNode m_EditableControl = null;

		public Cl_CtrlControlNode EditableControl {
			get {
				return m_EditableControl;
			}
			set {
				m_EditableControl = value;
				if (m_EditableControl == null) return;

				f_loadDefaultValues();
				f_loadCustomValues();
			}
		}

		public UC_ElementsPropertyPanel() {
			InitializeComponent();

			panelManager = new UI_PanelManager(ctrl_P_ControlsConteiner);
		}

		private void f_loadDefaultValues() {
			ctrl_TB_Name.Text = m_EditableControl.p_Control.p_BaseControl.p_Name;
			ctrl_TB_Hint.Text = m_EditableControl.p_Control.p_BaseControl.p_Help;
			ctrl_CB_IsRequiredFIeld.Checked = m_EditableControl.p_Control.p_BaseControl.p_Required;
			ctrl_CB_IsEditing.Checked = m_EditableControl.p_Control.p_BaseControl.p_Editing;
			ctrl_CB_IsVisible.Checked = m_EditableControl.p_Control.p_BaseControl.p_Visible;
			ctrl_CB_IsSymmentry.Checked = m_EditableControl.p_Control.p_BaseControl.p_Symmetrical;
			ctrl_TB_Symmetry1.Text = m_EditableControl.p_Control.p_BaseControl.p_SymmetryParamLeft;
			ctrl_TB_Symmetry2.Text = m_EditableControl.p_Control.p_BaseControl.p_SymmetryParamRight;
			ctrl_TB_Note.Text = m_EditableControl.p_Control.p_BaseControl.p_Comment;
		}

		private void f_loadCustomValues() {
			if (EditableControl.p_Control is Cl_CtrlImage) {
				ctrl_CB_IsEditing.Visible = false;
				ctrl_CB_IsRequiredFIeld.Visible = false;
				panel5.Visible = false;

				panelManager.SetControl<UC_EditorImage>();
			} else if (EditableControl.p_Control is Cl_CtrlTextual) {
				ctrl_CB_IsEditing.Visible = true;
				ctrl_CB_IsRequiredFIeld.Visible = true;
				panel5.Visible = true;

				panelManager.SetControl<UC_EditorTextual>();
			}

			((I_EditPanel)panelManager.ActiveControl).SetControl(EditableControl.p_Control);
		}

		private void ctrl_B_Save_Click(object sender, EventArgs e) {
			m_EditableControl.p_Control.p_BaseControl.p_Name = ctrl_TB_Name.Text;
			m_EditableControl.p_Control.p_BaseControl.p_Help = ctrl_TB_Hint.Text;
			m_EditableControl.p_Control.p_BaseControl.p_Required = ctrl_CB_IsRequiredFIeld.Checked;
			m_EditableControl.p_Control.p_BaseControl.p_Editing = ctrl_CB_IsEditing.Checked;
			m_EditableControl.p_Control.p_BaseControl.p_Visible = ctrl_CB_IsVisible.Checked;
			m_EditableControl.p_Control.p_BaseControl.p_Symmetrical = ctrl_CB_IsSymmentry.Checked;
			m_EditableControl.p_Control.p_BaseControl.p_SymmetryParamLeft = ctrl_TB_Symmetry1.Text;
			m_EditableControl.p_Control.p_BaseControl.p_SymmetryParamRight = ctrl_TB_Symmetry2.Text;
			m_EditableControl.p_Control.p_BaseControl.p_Comment = ctrl_TB_Note.Text;

			m_EditableControl.Text = m_EditableControl.p_TreeName;

			((I_EditPanel)panelManager.ActiveControl).ConfirmChanges();

			Cl_App.m_DataContext.SaveChanges();

			EditableControl.f_UpdateTreeNodeIcon();
		}
	}
}
