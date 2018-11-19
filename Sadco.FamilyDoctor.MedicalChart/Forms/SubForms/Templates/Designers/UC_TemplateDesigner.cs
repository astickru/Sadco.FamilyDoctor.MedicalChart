using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Controls;
using Sadco.FamilyDoctor.Core.Controls.DesignerPanel;
using Sadco.FamilyDoctor.Core.Entities;
using System;
using System.Linq;
using System.Data.Entity;
using System.Windows.Forms;
using FD.dat.mon.stb.lib;
using Sadco.FamilyDoctor.Core.EntityLogs;
using Sadco.FamilyDoctor.Core.Facades;
using Sadco.FamilyDoctor.Core.Controls.ResizableListBox;

namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
    public partial class UC_TemplateDesigner : UserControl
    {
        private Cl_EntityLog m_Log = new Cl_EntityLog();

        public UC_TemplateDesigner()
        {
            InitializeComponent();
            ctrl_EditorPanel.p_AllowItemDrag = true;
        }

        public Cl_Template p_EditingTemplate { get; private set; }

        public void f_SetToolboxService(Ctrl_TreeElements a_TreeElements)
        {
            ctrl_EditorPanel.p_ToolboxService = a_TreeElements;
            ctrl_EditorPanel.p_ToolboxService.p_ReadOnly = true;
        }

        public void f_SetTemplate(Cl_Template a_Template)
        {
            if (a_Template == null) return;

            p_EditingTemplate = a_Template;
            if (p_EditingTemplate.p_Version == 0)
                ctrl_Version.Text = "Черновик";
            else
                ctrl_Version.Text = p_EditingTemplate.p_Version.ToString();
            Cl_TemplatesFacade.f_GetInstance().f_LoadTemplatesElements(a_Template);
            m_Log.f_SetEntity(a_Template);
            if (a_Template.p_TemplateElements != null)
                ctrl_EditorPanel.f_SetTemplatesElements(a_Template.p_TemplateElements.ToArray());
        }

        private void ctrl_B_Save_Click(object sender, EventArgs e)
        {
            if (p_EditingTemplate == null) return;
            I_Element[] elements = new I_Element[ctrl_EditorPanel.Items.Count];
            ctrl_EditorPanel.Items.CopyTo(elements, 0);
            Cl_Template tpl = Cl_TemplatesFacade.f_GetInstance().f_SaveTemplate(p_EditingTemplate, elements, false, m_Log);
            f_SetTemplate(tpl);
        }

        private void ctrl_B_UpSave_Click(object sender, EventArgs e)
        {
            if (p_EditingTemplate == null) return;
            I_Element[] templates = new I_Element[ctrl_EditorPanel.Items.Count];
            ctrl_EditorPanel.Items.CopyTo(templates, 0);

            Cl_Template tpl = Cl_TemplatesFacade.f_GetInstance().f_SaveTemplate(p_EditingTemplate, templates, true, m_Log);
            f_SetTemplate(tpl);
        }

        private void ctrl_B_History_Click(object sender, EventArgs e)
        {
            Dlg_HistoryViewer viewer = new Dlg_HistoryViewer();
            viewer.LoadHistory(false, E_EntityTypes.Templates, p_EditingTemplate.p_TemplateID);
            viewer.ShowDialog(this);
        }
    }
}
