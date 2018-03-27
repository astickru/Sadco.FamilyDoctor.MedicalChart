using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Controls;
using Sadco.FamilyDoctor.Core.Entities;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
    public partial class UC_EditorTemplates : UserControl
	{
		public UC_EditorTemplates() {
            Tag = string.Format("Редактор шаблонов v{0}", ConfigurationManager.AppSettings["Version"]);
            InitializeComponent();
			f_InitTreeView();
		}

		private void f_InitTreeView() {
            ctrl_TreeTemplates.AfterSelect += Ctrl_TreeTemplates_AfterSelect;
            ctrl_TreeTemplates.e_EditElement += Ctrl_TreeTemplates_e_EditElement;
            Cl_Group[] groups = Cl_App.m_DataContext.p_Groups.Include(g => g.p_SubGroups).Where(g => g.p_Type == Cl_Group.E_Type.Templates && g.p_ParentID == null && !g.p_IsArhive).ToArray();
            foreach (Cl_Group group in groups)
            {
                f_PopulateTreeGroup(group, ctrl_TreeTemplates.Nodes);
            }
		}

        private void f_PopulateTreeGroup(Cl_Group a_Group, TreeNodeCollection a_TreeNodes) {
            TreeNode node = new Ctrl_TreeNodeGroup(a_Group);
            a_TreeNodes.Add(node);
            var tpls = Cl_App.m_DataContext.p_Templates
                .Where(t => t.p_ParentGroupID == a_Group.p_ID && !t.p_IsArhive).GroupBy(t => t.p_TemplateID)
                    .Select(grp => grp
                        .OrderByDescending(v => v.p_Version)
                        .FirstOrDefault());
            foreach (Cl_Template tpl in tpls)
            {
                node.Nodes.Add(new Ctrl_TreeNodeTemplate(a_Group, tpl));
            }
            var dcGroups = Cl_App.m_DataContext.Entry(a_Group).Collection(g => g.p_SubGroups);
            if (!dcGroups.IsLoaded) dcGroups.Load();
            foreach (Cl_Group group in a_Group.p_SubGroups)
            {
                if (!group.p_IsArhive)
                    f_PopulateTreeGroup(group, node.Nodes);
            }
		}

        private void Ctrl_TreeTemplates_AfterSelect(object sender, TreeViewEventArgs e)
        {
            f_LoadTemplates();
        }

        private void Ctrl_TreeTemplates_e_EditElement(object sender, TreeViewEventArgs e)
        {
            Ctrl_TreeNodeTemplate treeNode = (Ctrl_TreeNodeTemplate)e.Node;
            F_DesignerTemplate editor = new F_DesignerTemplate();
            editor.p_ActiveTemplate = treeNode.p_Template;
            editor.Show(ParentForm);
        }

        /// <summary>
        /// Обновление списка темплейтов в области свойств группы
        /// </summary>
        private void f_LoadTemplates()
        {
            ctrl_LVTemplates.Items.Clear();
            if (ctrl_TreeTemplates.p_SelectedTemplate != null && ctrl_TreeTemplates.p_SelectedTemplate.p_Template != null)
            {
                Cl_TemplatesElements[] controls = Cl_App.m_DataContext.p_TemplatesElements.Where(t => t.p_TemplateID == ctrl_TreeTemplates.p_SelectedTemplate.p_Template.p_ID).ToArray();
                foreach (Cl_TemplatesElements control in controls)
                {
                    if (control.p_Element == null) continue;

                    ListViewItem listitem = new ListViewItem(new string[] { control.p_Element.p_Name, control.p_ControlType });
                    ctrl_LVTemplates.Items.Add(listitem);
                }
            }
            else if (ctrl_TreeTemplates.p_SelectedGroup != null)
            {
                Cl_Template[] templates = Cl_App.m_DataContext.p_Templates.
                    Where(t => t.p_ParentGroupID == ctrl_TreeTemplates.p_SelectedGroup.p_Group.p_ID && !t.p_IsArhive).ToArray();
                foreach (Cl_Template template in templates)
                {
                    ListViewItem listitem = new ListViewItem(new string[] { template.p_Name, template.p_Description });
                    listitem.Tag = template.p_ID;
                    ctrl_LVTemplates.Items.Add(listitem);
                }
            }
        }
    }
}
