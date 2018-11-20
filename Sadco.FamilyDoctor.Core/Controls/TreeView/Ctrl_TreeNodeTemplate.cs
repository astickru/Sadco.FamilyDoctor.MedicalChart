using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.Core.Facades;
using System.Drawing;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.Core.Controls
{
    public class Ctrl_TreeNodeTemplate : TreeNode, I_TreeNode
    {
        public Ctrl_TreeNodeTemplate(Cl_Group a_Group, Cl_Template a_Template)
        {
            p_Group = a_Group;
            p_Template = a_Template;
        }

        public Cl_Group p_Group { get; private set; }
        public Cl_Template m_Template = null;
        public Cl_Template p_Template {
            get {
                return m_Template;
            }
            set {
                m_Template = value;
                f_ReDraw();
            }
        }

        /// <summary>Обновление части дерева</summary>
        public void f_Update()
        {
            m_Template = Cl_TemplatesFacade.f_GetInstance().f_GetLastVersionTemplate(m_Template);
            f_ReDraw();
        }

        private void f_ReDraw()
        {
            if (m_Template != null)
            {
                Text = m_Template.p_Name;
                Name = p_Template.p_ID.ToString();
                ImageKey = p_Template.p_IconName;
                SelectedImageKey = p_Template.p_IconName;
                if (p_Template.p_IsConflict)
                    ForeColor = Color.Red;
                else
                    ForeColor = Color.Blue;
            }
            else
            {
                Text = "Шаблон отсутствует";
                this.NodeFont = new Font(this.NodeFont, FontStyle.Bold);
                ForeColor = Color.Red;
            }
        }
    }
}
