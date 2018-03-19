using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Controls;
using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.MedicalChart.Forms.SubForms.Elements.Editors;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
    public partial class UC_EditorElements : UserControl
    {
        public const string WinTitle = "Редактор элементов v0.5";

        private UI_PanelManager m_PanelManager = null;


        private enum Em_NodeTypes
        {
            Nothing,
            Group,
            Control
        }

        public UC_EditorElements()
        {
            this.Tag = WinTitle;
            InitializeComponent();
            f_InitTreeView();

            m_PanelManager = new UI_PanelManager(ctrl_P_ElementProperty);
        }

        private void f_InitTreeView()
        {
            ctrl_TreeElements.AfterSelect += Ctrl_TreeElements_AfterSelect;
            Cl_Group[] groups = Cl_App.m_DataContext.p_GroupsElements.Include(g => g.p_SubGroups).Where(e => e.p_ParentID == null).ToArray();
            foreach (Cl_GroupElements group in groups)
            {
                f_PopulateTreeGroup(group, ctrl_TreeElements.Nodes);
            }
        }

        private void f_PopulateTreeGroup(Cl_GroupElements a_Group, TreeNodeCollection a_TreeNodes)
        {
            TreeNode node = new Ctrl_TreeNodeGroup(a_Group);
            a_TreeNodes.Add(node);
            var els = Cl_App.m_DataContext.p_Elements
                .Include(e => e.p_PartLocations).Include(e => e.p_NormValues).Include(e => e.p_PatValues)
                .Where(e => e.p_ParentGroupID == a_Group.p_ID && !e.p_IsArhive).GroupBy(e => e.p_ElementID)
                    .Select(grp => grp
                        .OrderByDescending(v => v.p_Version)
                        .FirstOrDefault());
            foreach (Cl_Element el in els)
            {
                node.Nodes.Add(new Ctrl_TreeNodeElement(a_Group, el));
            }
            var dcGroups = Cl_App.m_DataContext.Entry(a_Group).Collection(g => g.p_SubGroups);
            if (!dcGroups.IsLoaded) dcGroups.Load();
            foreach (Cl_GroupElements group in a_Group.p_SubGroups)
            {
                f_PopulateTreeGroup(group, node.Nodes);
            }
        }

        private void Ctrl_TreeElements_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (ctrl_TreeElements.p_SelectedElement != null && ctrl_TreeElements.p_SelectedElement.p_Element != null)
            {
                UC_ElementsPropertyPanel panel = m_PanelManager.f_SetElement<UC_ElementsPropertyPanel>();
                panel.p_EditableElement = ctrl_TreeElements.p_SelectedElement;
            }
            else
            {
                m_PanelManager.f_DeleteElement();
            }
        }
    }
}
