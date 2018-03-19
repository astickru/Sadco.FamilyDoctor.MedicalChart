using Sadco.FamilyDoctor.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.Core.Controls
{
    public class Ctrl_TreeNodeGroup : TreeNode
    {
        public Ctrl_TreeNodeGroup(Cl_GroupElements a_Group)
        {
            p_Group = a_Group;
            Name = a_Group.p_ID.ToString();
            Text = a_Group.p_Name;
        }

        public Cl_GroupElements p_Group { get; private set; }

        public void f_SetGroupName(string a_Name)
        {
            p_Group.p_Name = a_Name;
            Text = p_Group.p_Name;
        }
    }
}
