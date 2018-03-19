using Sadco.FamilyDoctor.Core.Entities;
using System.Drawing;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.Core.Controls
{
    public class Ctrl_TreeNodeTemplate : TreeNode
    {
        public Ctrl_TreeNodeTemplate(Cl_GroupElements a_Group, Cl_Template a_Template)
        {
            p_Group = a_Group;
            p_Template = a_Template;
            ForeColor = Color.Blue;
        }

        public Cl_GroupElements p_Group { get; private set; }
        public Cl_Template m_Template = null;
        public Cl_Template p_Template {
            get {
                return m_Template;
            }
            set {
                m_Template = value;
                if (m_Template != null)
                {
                    Text = string.Format("{0} v.{1}", m_Template.p_Name, m_Template.p_Version);
                    Name = p_Template.p_ID.ToString();
                    ImageKey = p_Template.p_IconName;
                    SelectedImageKey = p_Template.p_IconName;
                }
            }
        }
    }
}
