using Sadco.FamilyDoctor.Core.Entities;
using System.Drawing;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.Core.Controls
{
    public class Ctrl_TreeNodeElement : TreeNode
    {
        public Ctrl_TreeNodeElement(Cl_GroupElements a_Group, Cl_Element a_Element)
        {
            p_Group = a_Group;
            p_Element = a_Element;
            ForeColor = Color.Blue;
        }

        public Cl_GroupElements p_Group { get; private set; }
        public Cl_Element m_Element = null;
        public Cl_Element p_Element {
            get {
                return m_Element;
            }
            set {
                m_Element = value;
                if (m_Element != null)
                {
                    Text = string.Format("{0} v.{1}", m_Element.p_Name, m_Element.p_Version);
                    Name = p_Element.p_ID.ToString();
                    ImageKey = p_Element.p_IconName;
                    SelectedImageKey = p_Element.p_IconName;
                }
            }
        }
    }
}
