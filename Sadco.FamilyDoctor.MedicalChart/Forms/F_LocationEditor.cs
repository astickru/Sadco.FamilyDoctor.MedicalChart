using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.MedicalChart.Entities.Controls;
using Sadco.FamilyDoctor.MedicalChart.Forms.SubForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart.Forms
{
	public partial class F_LocationEditor : Form
	{
		private UC_TemplateDesigner templateDesigner = null;

		public Cl_Template p_ActiveTemplate { get; set; }
		private List<Cl_TemplatesElements> m_TemplateControls = new List<Cl_TemplatesElements>();
		private List<Cl_TemplatesElements> m_newTemplateControls = new List<Cl_TemplatesElements>();

		public F_LocationEditor() {
			InitializeComponent();
			f_InitTVControls();

			this.Load += F_LocationEditor_Load;
		}

		private void F_LocationEditor_Load(object sender, EventArgs e) {
			templateDesigner = new UC_TemplateDesigner();
			templateDesigner.f_SetToolboxService(ctrl_TreeElements);
			templateDesigner.p_ActiveTemplate = p_ActiveTemplate;
			ctrl_P_DesignConteiner.Controls.Add(templateDesigner);
			templateDesigner.Dock = DockStyle.Fill;

			//templateDesigner.ctrl_EditorPanel.f_SetToolbox(ctrl_TreeElements);
		}

		#region Initialize tree menu
		private void f_InitTVControls() {
			UI_Helper.InitTreeView(ctrl_TVControls);
			UI_Helper.InitTreeView(ctrl_TreeElements);

			Cl_GroupElements[] groups = Cl_App.m_DataContext.p_GroupsElements.Include(g => g.p_SubGroups).Where(e => e.p_ParentID == null).ToArray();
			foreach (Cl_GroupElements group in groups) {
				f_PopulateTVControls(group, ctrl_TVControls.Nodes);
				f_PopulateTVControls(group, ctrl_TreeElements.Nodes);
			}
		}

		private void f_PopulateTVControls(Cl_GroupElements a_Group, TreeNodeCollection a_TreeNodes) {
			TreeNode node = UI_Helper.CreateNodeGroup(a_Group, a_TreeNodes);
			// Загрузка контролов
			//foreach (Cl_CtrlImage control in Cl_App.m_DataContext.p_Elms_Image.Include(el => el.p_BaseControl).Where(t => t.p_BaseControl.p_ParentGroupID == a_Group.p_ID)) {
			//	UI_Helper.AddTreeNodeControl(new Cl_CtrlControlNode(), node.Nodes, control);
			//}
			//foreach (Cl_CtrlTextual control in Cl_App.m_DataContext.p_Elms_Textual.Include(el => el.p_BaseControl).Where(t => t.p_BaseControl.p_ParentGroupID == a_Group.p_ID)) {
			//	UI_Helper.AddTreeNodeControl(new Cl_CtrlControlNode(), node.Nodes, new Core.Controls.UC_Text() { p_CtrlText = control });
			//}
			var dcGroups = Cl_App.m_DataContext.Entry(a_Group).Collection(c => c.p_SubGroups);
			if (!dcGroups.IsLoaded) dcGroups.Load();
			// Загрузка групп
			foreach (Cl_GroupElements group in a_Group.p_SubGroups) {
				f_PopulateTVControls(group, node.Nodes);
			}
		}
		#endregion

		private void ctrl_TVControls_ItemDrag(object sender, ItemDragEventArgs e) {
			if (!(e.Item is TreeNode))
				return;

			//if (!(((TreeNode)e.Item).Tag is Ctrl_TreeNodeElement)) return;

			DoDragDrop(e.Item, DragDropEffects.Move);
		}
	}
}
