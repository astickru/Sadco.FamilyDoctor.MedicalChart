using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Controls;
using Sadco.FamilyDoctor.Core.Controls.DesignerPanel;
using Sadco.FamilyDoctor.Core.Entities;
using System;
using System.Linq;
using System.Data.Entity;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
    public partial class UC_TemplateDesigner : UserControl
    {
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
            var elements = Cl_App.m_DataContext.p_TemplatesElements.Include(te => te.p_ChildElement).Include(te => te.p_ChildTemplate).Include(te => te.p_ChildTemplate.p_TemplateElements)
                .Where(t => t.p_TemplateID == p_EditingTemplate.p_ID).OrderBy(t => t.p_Index).ToArray();
            ctrl_EditorPanel.f_SetTemplatesElements(elements);
        }

        private void ctrl_B_Save_Click(object sender, EventArgs e)
        {
            if (p_EditingTemplate == null) return;
            using (var transaction = Cl_App.m_DataContext.Database.BeginTransaction())
            {
                try
                {
                    Cl_Template tpl = null;
                    if (p_EditingTemplate.p_Version == 0)
                    {
                        tpl = p_EditingTemplate;
                        tpl.p_Version = 1;
                    }
                    else
                    {
                        tpl = new Cl_Template();
                        tpl.p_TemplateID = p_EditingTemplate.p_TemplateID;
                        tpl.p_Type = p_EditingTemplate.p_Type;
                        tpl.p_Name = p_EditingTemplate.p_Name;
                        tpl.p_Version = p_EditingTemplate.p_Version + 1;
                        tpl.p_ParentGroupID = p_EditingTemplate.p_ParentGroupID;
                        tpl.p_ParentGroup = p_EditingTemplate.p_ParentGroup;
                        tpl.p_Description = p_EditingTemplate.p_Description;
                        Cl_App.m_DataContext.p_Templates.Add(tpl);
                    }
                    Cl_App.m_DataContext.SaveChanges();
                    int index = 0;
                    foreach (I_Element item in ctrl_EditorPanel.Items)
                    {
                        var tplEl = new Cl_TemplateElement();
                        tplEl.p_TemplateID = tpl.p_ID;
                        tplEl.p_Template = tpl;
                        if (item is Ctrl_Element)
                        {
                            var block = (Ctrl_Element)item;
                            tplEl.p_ChildElementID = block.p_ID;
                            tplEl.p_ChildElement = block.p_Element;
                        }
                        else if (item is Ctrl_Template)
                        {
                            var block = (Ctrl_Template)item;
                            tplEl.p_ChildTemplateID = block.p_ID;
                            tplEl.p_ChildTemplate = block.p_Template;
                        }
                        tplEl.p_Index = index++;
                        Cl_App.m_DataContext.p_TemplatesElements.Add(tplEl);
                    }
                    Cl_App.m_DataContext.SaveChanges();
                    transaction.Commit();
                    f_SetTemplate(tpl);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("При сохранении изменений произошла ошибка");
                }
            }
        }
    }
}
