using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Controls;
using Sadco.FamilyDoctor.Core.Controls.DesignerPanel;
using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.MedicalChart.Forms.SubForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
    public partial class F_DesignerTemplate : Form
	{
		private UC_TemplateDesigner templateDesigner = null;

		public Cl_Template p_ActiveTemplate { get; set; }
		private List<Cl_TemplatesElements> m_TemplateControls = new List<Cl_TemplatesElements>();
		private List<Cl_TemplatesElements> m_newTemplateControls = new List<Cl_TemplatesElements>();

		public F_DesignerTemplate() {
            InitializeComponent();
            Text = string.Format("Дизайнер шаблона v{0}", ConfigurationManager.AppSettings["Version"]);
            f_InitTreeView();

			this.Load += F_LocationEditor_Load;
		}

		private void F_LocationEditor_Load(object sender, EventArgs e) {
			templateDesigner = new UC_TemplateDesigner();
			templateDesigner.f_SetToolboxService(ctrl_TreeElements);
			templateDesigner.p_ActiveTemplate = p_ActiveTemplate;
			ctrl_P_DesignConteiner.Controls.Add(templateDesigner);
			templateDesigner.Dock = DockStyle.Fill;
        }

        private void f_InitTreeView()
        {
            Cl_Group[] groups = Cl_App.m_DataContext.p_Groups.Include(g => g.p_SubGroups).Where(g => g.p_Type == Cl_Group.E_Type.Elements && g.p_ParentID == null && !g.p_IsArhive).ToArray();
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
                .Where(e => e.p_ParentGroupID == a_Group.p_ID && !e.p_IsArhive).GroupBy(e => e.p_ElementID)
                    .Select(grp => grp
                        .OrderByDescending(v => v.p_Version).FirstOrDefault())
                        .Include(e => e.p_ParamsValues);
            foreach (Cl_Element el in els)
            {
                //node.Nodes.Add(new Ctrl_TreeNodeElement(a_Group, el));

                Cl_ToolboxItemElement item = new Cl_ToolboxItemElement(typeof(Ctrl_Element));
                item.p_TreeNodeElement = new Ctrl_TreeNodeElement(a_Group, el);
                ctrl_TreeElements.AddToolboxItem(item);


                //ToolboxItem item = new ToolboxItem(typeof(Ctrl_Element));
                //item.Bitmap = (Bitmap)Properties.Resources.ResourceManager.GetObject(el.p_IconName);
                //item.Description = item.DisplayName = el.p_Name;
                //ctrl_TreeElements.AddToolboxItem(item);
            }
            var dcGroups = Cl_App.m_DataContext.Entry(a_Group).Collection(g => g.p_SubGroups);
            if (!dcGroups.IsLoaded) dcGroups.Load();
            foreach (Cl_Group group in a_Group.p_SubGroups)
            {
                if (!group.p_IsArhive)
                    f_PopulateTreeGroup(group, node.Nodes);
            }
        }

  //      #region Initialize tree menu
  //      private void f_InitTVControls() {
		//	UI_Helper.InitTreeView(ctrl_TVControls);
		//	UI_Helper.InitTreeView(ctrl_TreeElements);

  //          Cl_Group[] groups = Cl_App.m_DataContext.p_Groups.Include(g => g.p_SubGroups).Where(e => e.p_Type == Cl_Group.E_Type.Elements && e.p_ParentID == null).ToArray();
		//	foreach (Cl_Group group in groups) {
		//		f_PopulateTVControls(group, ctrl_TVControls.Nodes);
		//		f_PopulateTVControls(group, ctrl_TreeElements.Nodes);
		//	}
		//}

		//private void f_PopulateTVControls(Cl_Group a_Group, TreeNodeCollection a_TreeNodes) {
		//	TreeNode node = UI_Helper.CreateNodeGroup(a_Group, a_TreeNodes);
		//	// Загрузка контролов
		//	//foreach (Cl_CtrlImage control in Cl_App.m_DataContext.p_Elms_Image.Include(el => el.p_BaseControl).Where(t => t.p_BaseControl.p_ParentGroupID == a_Group.p_ID)) {
		//	//	UI_Helper.AddTreeNodeControl(new Cl_CtrlControlNode(), node.Nodes, control);
		//	//}
		//	//foreach (Cl_CtrlTextual control in Cl_App.m_DataContext.p_Elms_Textual.Include(el => el.p_BaseControl).Where(t => t.p_BaseControl.p_ParentGroupID == a_Group.p_ID)) {
		//	//	UI_Helper.AddTreeNodeControl(new Cl_CtrlControlNode(), node.Nodes, new Core.Controls.UC_Text() { p_CtrlText = control });
		//	//}
		//	var dcGroups = Cl_App.m_DataContext.Entry(a_Group).Collection(c => c.p_SubGroups);
		//	if (!dcGroups.IsLoaded) dcGroups.Load();
		//	// Загрузка групп
		//	foreach (Cl_Group group in a_Group.p_SubGroups) {
		//		f_PopulateTVControls(group, node.Nodes);
		//	}
		//}
		//#endregion

		//private void ctrl_TVControls_ItemDrag(object sender, ItemDragEventArgs e) {
		//	if (!(e.Item is TreeNode))
		//		return;

		//	//if (!(((TreeNode)e.Item).Tag is Ctrl_TreeNodeElement)) return;

		//	DoDragDrop(e.Item, DragDropEffects.Move);
		//}
	}
}
