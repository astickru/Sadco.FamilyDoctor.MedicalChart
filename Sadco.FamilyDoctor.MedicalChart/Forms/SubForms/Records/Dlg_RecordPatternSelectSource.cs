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
    public partial class Dlg_RecordPatternSelectSource : Form
    {
        public Dlg_RecordPatternSelectSource() {
            Text = string.Format("Выбор шаблона для нового паттерна записей v{0}", ConfigurationManager.AppSettings["Version"]);
            InitializeComponent();
            f_InitTreeView();
        }

        public Cl_Template p_SelectedTemplate {
            get {
                if (ctrl_TreeTemplates.SelectedNode != null)
                {
                    var node = ctrl_TreeTemplates.SelectedNode as Ctrl_TreeNodeTemplate;
                    if (node != null)
                    {
                        return node.p_Template;
                    }
                }
                return null;
            }
        }

		private void f_InitTreeView() {
            Cl_Group[] groups = Cl_App.m_DataContext.p_Groups.Include(g => g.p_SubGroups).Where(g => g.p_Type == Cl_Group.E_Type.Templates && g.p_ParentID == null && !g.p_IsDelete).ToArray();
            foreach (Cl_Group group in groups)
            {
                f_PopulateTreeGroup(group, ctrl_TreeTemplates.Nodes);
            }
		}

        private void f_PopulateTreeGroup(Cl_Group a_Group, TreeNodeCollection a_TreeNodes) {
            TreeNode node = new Ctrl_TreeNodeGroup(a_Group);
            a_TreeNodes.Add(node);
            var tpls = Cl_App.m_DataContext.p_Templates
                .Where(t => t.p_ParentGroupID == a_Group.p_ID && t.p_Type == Cl_Template.E_TemplateType.Template && !t.p_IsDelete).GroupBy(t => t.p_TemplateID)
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
                if (!group.p_IsDelete)
                    f_PopulateTreeGroup(group, node.Nodes);
            }
		}

        private void ctrl_TreeTemplates_DoubleClick(object sender, System.EventArgs e)
        {
            var tree = (Ctrl_TreeTemplates)sender;
            if (tree.p_SelectedTemplate != null)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
