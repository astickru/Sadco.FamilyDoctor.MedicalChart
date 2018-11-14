using Sadco.FamilyDoctor.Core.Entities;
using System.Drawing;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.Core.Controls
{
    public class Ctrl_TreeNodeBookmark : TreeNode, I_TreeNode
    {
        public Ctrl_TreeNodeBookmark(Cl_Group a_Group, E_Bookmarks a_Bookmark)
        {
            p_Group = a_Group;
            p_Bookmark = a_Bookmark;
            ForeColor = Color.Blue;
        }

        public Cl_Group p_Group { get; private set; }

        private E_Bookmarks m_Bookmark = E_Bookmarks.Bookmark_1;
        public E_Bookmarks p_Bookmark {
            get {
                return m_Bookmark;
            }
            set {
                m_Bookmark = value;
                f_Update();
            }
        }

        /// <summary>Обновление части дерева</summary>
        public void f_Update()
        {
            Text = m_Element.p_Name;
            Name = p_Element.p_ID.ToString();
            ImageKey = p_Element.p_IconName;
            SelectedImageKey = p_Element.p_IconName;
        }
    }
}
