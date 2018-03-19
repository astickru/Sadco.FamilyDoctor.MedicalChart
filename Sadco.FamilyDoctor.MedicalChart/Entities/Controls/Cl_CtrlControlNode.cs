using Sadco.FamilyDoctor.Core.Controls;
using Sadco.FamilyDoctor.Core.Entities;
using System.Drawing;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart.Entities.Controls
{
    public class Cl_CtrlControlNode : TreeNode, I_TreeControl
    {
        public Cl_Element p_Element { get; private set; }
        public string p_TreeName { get { return p_Element.p_Name; } }
        public TreeNode p_getTreeNode { get { return this; } }

        public void f_AddToTreeNode(TreeNodeCollection nodes)
        {
            base.Tag = this;
            base.ForeColor = Color.Blue;
            f_UpdateTreeNodeIcon();

            nodes.Add(this);
        }

        public void f_UpdateTreeNodeIcon()
        {
            UI_Helper.SetMenuImage(p_Element.p_Icon, p_Element.p_IconName);

            base.ImageKey = p_Element.p_IconName;
            base.SelectedImageKey = p_Element.p_IconName;
        }

        public void f_SetObjectControl(I_Control control)
        {
            if (!(control is Cl_Element))
                return;

            Cl_Element objControl = control as Cl_Element;
            p_Element = objControl;
            if (p_Element != null)
            {
                base.Name = p_Element.p_ID.ToString();
                base.Text = p_TreeName;
            }
        }
    }
}
