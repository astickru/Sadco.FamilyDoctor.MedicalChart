using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Controls;
using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.Core.EntityLogs;
using System;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms.Elements.Editors
{
	public partial class UC_ElementsPropertyPanel : UserControl
	{
		public UC_ElementsPropertyPanel()
		{
			InitializeComponent();
			m_PanelManager = new UI_PanelManager(ctrl_P_ControlConteiner);
		}

		private UI_PanelManager m_PanelManager = null;
		private Ctrl_TreeNodeElement m_EditableElement = null;

		private bool m_IsReadOnly = false;
		public bool p_IsReadOnly {
			get {
				return m_IsReadOnly;
			}
			set {
				m_IsReadOnly = value;
				if (m_IsReadOnly)
				{
					ctrl_BCancel.Visible = ctrl_BSave.Visible = false;
					ctrl_BEdit.Visible = true;
					p_EditPanel.p_ReadOnly = false;
				}
				else
				{
					ctrl_BCancel.Visible = ctrl_BSave.Visible = true;
					ctrl_BEdit.Visible = false;
					p_EditPanel.p_ReadOnly = true;
				}
			}
		}

		private I_EditPanel p_EditPanel {
			get {
				return ((I_EditPanel)m_PanelManager.p_ActiveControl);
			}
		}

		public Ctrl_TreeNodeElement p_EditableElement {
			get {
				return m_EditableElement;
			}
			set {
				m_EditableElement = value;
				if (m_EditableElement == null) return;
				f_LoadCustomValues();
			}
		}

		private void f_LoadCustomValues()
		{
			Control ctrl = null;
			if (p_EditableElement.p_Element.f_IsImage())
			{
				ctrl = m_PanelManager.f_SetElement<UC_EditorImage>();

			}
			else if (p_EditableElement.p_Element.f_IsText())
			{
				ctrl = m_PanelManager.f_SetElement<UC_EditorTextual>();
			}
			if (ctrl != null)
			{
				ctrl.Dock = DockStyle.Top;
				p_EditPanel.f_SetElement(p_EditableElement.p_Element);
				p_IsReadOnly = true;
			}
		}

		private void ctrl_BCancel_Click(object sender, EventArgs e)
		{
            EntityLog.CustomMessageLog(p_EditableElement.p_Element, "Нажата кнопка \"Отмена\"");
            p_IsReadOnly = true;
			p_EditPanel.f_SetElement(p_EditableElement.p_Element);
		}

		private void ctrl_BEdit_Click(object sender, EventArgs e)
		{
            EntityLog.CustomMessageLog(p_EditableElement.p_Element, "Нажата кнопка \"Редактировать\"");
			p_IsReadOnly = false;
		}

		private void ctrl_BSave_Click(object sender, EventArgs e)
		{
            EntityLog.CustomMessageLog(p_EditableElement.p_Element, "Нажата кнопка \"Сохранить\"");
            Cl_Element el = p_EditPanel.f_ConfirmChanges() as Cl_Element;
			if (el != null)
			{
				m_EditableElement.p_Element = el;
				p_IsReadOnly = true;
			}
		}

		private void ctrl_BHistory_Click(object sender, EventArgs e)
		{
			Dlg_HistoryViewer viewer = new Dlg_HistoryViewer();
			viewer.LoadHistory(p_EditableElement.p_Element.p_ElementID);
			viewer.ShowDialog(this);
		}
	}
}
