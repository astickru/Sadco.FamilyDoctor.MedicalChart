using Sadco.FamilyDoctor.MedicalChart.Forms.MegaTemplate;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart.Entities.Controls
{
	public class Cl_CtrlTreeNode : TreeNode
	{
		private Core.Entities.Controls.I_Control m_Control = null;

		public Core.Entities.Controls.I_Control p_Control {
			get {
				return m_Control;
			}
			set {
				m_Control = value;
				if (m_Control != null) {
					base.Name = m_Control.p_BaseControl.p_ID.ToString();
					base.Text = p_ControlName;
				}
			}
		}

		public string p_ControlName { get { return p_Control.p_BaseControl.p_Name; } }

		public void f_AddToTreeNode(TreeNodeCollection nodes) {
			base.Tag = this;
			base.ForeColor = Color.Blue;
			base.ImageKey = p_Control.p_BaseControl.p_Image;

			nodes.Add(this);
		}
	}
}
