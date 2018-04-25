using Sadco.FamilyDoctor.Core.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.Core.Controls
{
    public class Ctrl_TreeNodeGroup : TreeNode, I_TreeNode
    {
        public Ctrl_TreeNodeGroup(Cl_Group a_Group)
        {
            p_Group = a_Group;
            Name = a_Group.p_ID.ToString();
            Text = a_Group.p_Name;
        }

        private Cl_Group m_Group = null;
        public Cl_Group p_Group {
            get {
                return m_Group;
            }
            set {
                m_Group = value;
                f_Update();
            }
        }

        public void f_SetGroupName(string a_Name)
        {
            p_Group.p_Name = a_Name;
            Text = p_Group.p_Name;
        }

        /// <summary>Обновление части дерева</summary>
        public void f_Update()
        {
            if (m_Group != null)
            {
                this.ImageKey = "FOLDER_16" + (m_Group.p_IsDelete ? "_DEL" : "");
                this.SelectedImageKey = "FOLDER_16" + (m_Group.p_IsDelete ? "_DEL" : "");
            }
            else
            {
                Text = "Группа отсутствует";
                this.NodeFont = new Font(this.NodeFont, FontStyle.Bold);
                ForeColor = Color.Red;
            }
        }
    }
}
