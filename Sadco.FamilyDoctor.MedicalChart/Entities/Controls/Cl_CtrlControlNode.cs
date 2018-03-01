using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.Core.Entities.Controls;
using System.Drawing;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart.Entities.Controls
{
	public class Cl_CtrlControlNode : TreeNode, I_TreeControl
	{
		public I_BaseControl p_Control { get; private set; }

		public string p_TreeName { get { return p_Control.p_BaseControl.p_Name; } }

		public TreeNode p_getTreeNode { get { return this; } }

		public void f_AddToTreeNode(TreeNodeCollection nodes) {
			base.Tag = this;
			base.ForeColor = Color.Blue;
			f_UpdateTreeNodeIcon();

			nodes.Add(this);
		}

		public void f_UpdateTreeNodeIcon() {
			UI_Helper.SetMenuImage(p_Control.p_Icon, p_Control.p_IconName);

			base.ImageKey = p_Control.p_BaseControl.p_IconName;
			base.SelectedImageKey = p_Control.p_BaseControl.p_IconName;
		}

		public void f_SetObjectControl(I_Control control) {
			if (!(control is I_BaseControl))
				return;

			I_BaseControl objControl = control as I_BaseControl;
			p_Control = objControl;
			if (p_Control != null) {
				base.Name = p_Control.p_BaseControl.p_ID.ToString();
				base.Text = p_TreeName;
			}
		}
	}
}
