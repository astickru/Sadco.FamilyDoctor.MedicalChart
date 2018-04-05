using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Controls;
using Sadco.FamilyDoctor.Core.Entities;
using System;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
    public partial class F_DesignerTemplate : Form
	{
		private UC_TemplateDesigner templateDesigner = null;
		public Cl_Template p_ActiveTemplate { get; set; }

		public F_DesignerTemplate() {
            InitializeComponent();
            f_InitTreeView();

			this.Load += F_LocationEditor_Load;
		}

		private void F_LocationEditor_Load(object sender, EventArgs e) {
			templateDesigner = new UC_TemplateDesigner();
			templateDesigner.f_SetToolboxService(ctrl_TreeElements);
			templateDesigner.f_SetTemplate(p_ActiveTemplate);
			ctrl_P_DesignConteiner.Controls.Add(templateDesigner);
			templateDesigner.Dock = DockStyle.Fill;
            Text = string.Format("Дизайнер шаблона \"{0}\" v{1}", p_ActiveTemplate.p_Name, ConfigurationManager.AppSettings["Version"]);
        }

        private void f_InitTreeView()
        {
            TreeNode node = new Ctrl_TreeNodeGroup(new Cl_Group() { p_Name = "Элементы" });
            ctrl_TreeElements.Nodes.Add(node);
            Cl_Group[] groups = Cl_App.m_DataContext.p_Groups.Include(g => g.p_SubGroups).Where(g => g.p_Type == Cl_Group.E_Type.Elements && g.p_ParentID == null && !g.p_IsArhive).ToArray();
            foreach (Cl_Group group in groups)
            {
                f_PopulateTreeGroup(Cl_Group.E_Type.Elements, group, node.Nodes);
            }
            node = new Ctrl_TreeNodeGroup(new Cl_Group() { p_Name = "Шаблоны" });
            ctrl_TreeElements.Nodes.Add(node);
            groups = Cl_App.m_DataContext.p_Groups.Include(g => g.p_SubGroups).Where(g => g.p_Type == Cl_Group.E_Type.Templates && g.p_ParentID == null && !g.p_IsArhive).ToArray();
            foreach (Cl_Group group in groups)
            {
                f_PopulateTreeGroup(Cl_Group.E_Type.Templates, group, node.Nodes);
            }
        }

        private void f_PopulateTreeGroup(Cl_Group.E_Type a_Type, Cl_Group a_Group, TreeNodeCollection a_TreeNodes)
        {
            TreeNode node = new Ctrl_TreeNodeGroup(a_Group);
            a_TreeNodes.Add(node);
            if (a_Type == Cl_Group.E_Type.Elements)
            {
                var els = Cl_App.m_DataContext.p_Elements
                    .Where(e => e.p_ParentGroupID == a_Group.p_ID && !e.p_IsArhive).GroupBy(e => e.p_ElementID)
                        .Select(grp => grp
                            .OrderByDescending(v => v.p_Version).FirstOrDefault())
                            .Include(e => e.p_ParamsValues);
                foreach (Cl_Element el in els)
                {
                    node.Nodes.Add(new Ctrl_TreeNodeElement(a_Group, el));
                }
            }
            else
            {
                var tpls = Cl_App.m_DataContext.p_Templates.Include(t => t.p_TemplateElements)
                    .Where(e => e.p_ParentGroupID == a_Group.p_ID && !e.p_IsArhive).GroupBy(e => e.p_TemplateID)
                        .Select(grp => grp
                            .OrderByDescending(v => v.p_Version).FirstOrDefault());    
                foreach (Cl_Template tpl in tpls)
                {
                    node.Nodes.Add(new Ctrl_TreeNodeTemplate(a_Group, tpl));
                }
            }
            var dcGroups = Cl_App.m_DataContext.Entry(a_Group).Collection(g => g.p_SubGroups);
            if (!dcGroups.IsLoaded) dcGroups.Load();
            foreach (Cl_Group group in a_Group.p_SubGroups)
            {
                if (!group.p_IsArhive)
                    f_PopulateTreeGroup(a_Type, group, node.Nodes);
            }
        }
	}
}
