using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Controls;
using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.Core.Facades;
using Sadco.FamilyDoctor.MedicalChart.Forms.SubForms.Elements.Editors;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
    public partial class UC_EditorElements : UserControl
    {
        private UI_PanelManager m_PanelManager = null;

        public UC_EditorElements()
        {
            Tag = string.Format("Редактор элементов v{0}", ConfigurationManager.AppSettings["Version"]);
            InitializeComponent();
            f_InitTreeView();
            m_PanelManager = new UI_PanelManager(ctrl_P_ElementProperty);
        }

        /// <summary>Флаг отображения удаленных элементов</summary>
        public bool p_IsShowDeleted { get; set; }

        private void f_InitTreeView()
        {
            ctrl_TreeElements.p_IsShowDeleted = p_IsShowDeleted;
            ctrl_TreeElements.AfterSelect += Ctrl_TreeElements_AfterSelect;
            ctrl_TreeElements.e_AfterCreateElement += Ctrl_TreeElements_e_AfterCreateElement;
            f_PopulateGroup();
        }

        private void f_PopulateGroup()
        {
            Cl_Group[] groups = Cl_App.m_DataContext.p_Groups.Include(g => g.p_SubGroups).Where(g => g.p_Type == Cl_Group.E_Type.Elements && g.p_ParentID == null && (p_IsShowDeleted ? true : !g.p_IsDelete)).ToArray();
            foreach (Cl_Group group in groups)
            {
                f_PopulateTreeGroup(group, ctrl_TreeElements.Nodes);
            }
        }

        private void f_PopulateTreeGroup(Cl_Group a_Group, TreeNodeCollection a_TreeNodes)
        {
            TreeNode node = new Ctrl_TreeNodeGroup(a_Group);
            a_TreeNodes.Add(node);
            var els = Cl_App.m_DataContext.p_Elements
                .Where(e => e.p_ParentGroupID == a_Group.p_ID && (p_IsShowDeleted ? true : !e.p_IsDelete)).GroupBy(e => e.p_ElementID)
                    .Select(grp => grp
                        .OrderByDescending(v => v.p_Version).FirstOrDefault())
                        .Include(e => e.p_ParamsValues);
            foreach (Cl_Element el in els)
            {
                node.Nodes.Add(new Ctrl_TreeNodeElement(a_Group, el));
            }
            var dcGroups = Cl_App.m_DataContext.Entry(a_Group).Collection(g => g.p_SubGroups);
            if (!dcGroups.IsLoaded) dcGroups.Load();
            foreach (Cl_Group group in a_Group.p_SubGroups)
            {
                if (!group.p_IsDelete || p_IsShowDeleted)
                    f_PopulateTreeGroup(group, node.Nodes);
            }
        }

        private void Ctrl_TreeElements_e_AfterCreateElement(object sender, TreeViewEventArgs e)
        {
            if (ctrl_TreeElements.p_SelectedElement != null && ctrl_TreeElements.p_SelectedElement.p_Element != null)
            {
                UC_ElementsPropertyPanel panel = m_PanelManager.f_SetElement<UC_ElementsPropertyPanel>();
                panel.p_EditableElement = ctrl_TreeElements.p_SelectedElement;
                panel.p_IsReadOnly = false;
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

        public void f_ShowDeletedElements(bool isShow)
        {
            ctrl_TreeElements.Nodes.Clear();
            f_PopulateGroup();
        }
    }
}
