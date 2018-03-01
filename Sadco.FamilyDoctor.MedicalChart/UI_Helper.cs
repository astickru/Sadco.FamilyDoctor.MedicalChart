using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.MedicalChart.Entities.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart
{
	public static class UI_Helper
	{
		public static ImageList p_ImageList { get; private set; }

		static UI_Helper() {
			p_ImageList = new ImageList();
		}

		public static void InitTreeView(TreeView treeObj) {
			if (treeObj == null)
				throw new ArgumentNullException("treeObj");

			treeObj.ImageList = p_ImageList;

			UI_Helper.SetMenuImage(Properties.Resources.folder, "folder");
			treeObj.ImageKey = "folder";
			treeObj.SelectedImageKey = "folder";

			treeObj.Nodes.Clear();
		}

		public static void AddTreeNodeControl(I_TreeControl treeControl, TreeNodeCollection nodes, I_Control control) {
			treeControl.f_SetObjectControl(control);
			treeControl.f_AddToTreeNode(nodes);
			UI_Helper.SetMenuImage(control.p_Icon, control.p_IconName);
			treeControl.p_getTreeNode.ForeColor = Color.Blue;
			treeControl.p_getTreeNode.ImageKey = control.p_IconName;
			treeControl.p_getTreeNode.SelectedImageKey = control.p_IconName;
		}

		/// <summary>
		/// Добавление нового элемента группы в передаваемую колекцию элементов
		/// </summary>
		/// <param name="group">Объект создаваемой группы</param>
		/// <param name="nodes">Колекция, в которую помещается новый элемент</param>
		/// <returns></returns>
		public static TreeNode CreateNodeGroup(I_Group group, TreeNodeCollection nodes) {
			return nodes.Add(group.p_ID.ToString(), group.p_Name);
		}

		public static void SetMenuImage(Image img, string nameKey) {
			if (p_ImageList.Images.ContainsKey(nameKey)) return;
			p_ImageList.Images.Add(nameKey, img);
		}
	}
}
