using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Controls;
using Sadco.FamilyDoctor.Core.Entities;
using System;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms.Elements.Editors
{
    public partial class UC_ElementsPropertyPanel : UserControl
    {
        private UI_PanelManager m_PanelManager = null;
        private Ctrl_TreeNodeElement m_EditableElement = null;

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

        public UC_ElementsPropertyPanel()
        {
            InitializeComponent();
            m_PanelManager = new UI_PanelManager(ctrl_P_ControlConteiner);
        }

        private void f_LoadCustomValues()
        {
            if (p_EditableElement.p_Element.f_IsImage())
            {
                m_PanelManager.f_SetElement<UC_EditorImage>();
            }
            else if (p_EditableElement.p_Element.f_IsText())
            {
                m_PanelManager.f_SetElement<UC_EditorTextual>();
            }
            ((I_EditPanel)m_PanelManager.p_ActiveControl).f_SetElement(p_EditableElement.p_Element);
        }

        private void ctrl_B_Save_Click(object sender, EventArgs e)
        {
            //m_EditableElement.Text = m_EditableElement.p_Element.p_Name;
            Cl_Element el = (Cl_Element)((I_EditPanel)m_PanelManager.p_ActiveControl).f_ConfirmChanges();
            m_EditableElement.p_Element = el;

            //Cl_App.m_DataContext.SaveChanges();
            //p_EditableControl.f_UpdateTreeNodeIcon();
        }
    }
}
