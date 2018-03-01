using Sadco.FamilyDoctor.Core.Entities;
using System.Drawing;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart.Entities.Controls
{
	public class Cl_CtrlTemplateNode : TreeNode, I_TreeControl
	{
		public Cl_Template p_Template { get; private set; }

		public string p_TreeName { get { return p_Template.p_Name; } }

		public TreeNode p_getTreeNode { get { return this; } }

		public void f_AddToTreeNode(TreeNodeCollection nodes) {
			base.Tag = this;
			base.ForeColor = Color.Blue;
			base.ImageKey = p_Template.p_IconName;

			nodes.Add(this);
		}

		public void f_SetObjectControl(I_Control control) {
			if (!(control is Cl_Template))
				return;

			Cl_Template objControl = (Cl_Template)control;
			p_Template = objControl;
			if (p_Template != null) {
				base.Name = p_Template.p_ID.ToString();
				base.Text = p_TreeName;
			}
		}
	}
}
