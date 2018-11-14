using Sadco.FamilyDoctor.Core.Entities;
using System.Drawing;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.Core.Controls.DesignerPanel
{
    public partial class Ctrl_Bookmark : UserControl, I_Element
    {
        public const int m_ElementHeight = 24;

        public Ctrl_Bookmark()
        {
            InitializeComponent();
            Height = m_ElementHeight;
        }

        public Cl_Bookmark m_Bookmark = null;
        public Cl_Bookmark p_Bookmark {
            get {
                return m_Bookmark;
            }
            set {
                m_Bookmark = value;
            }
        }

        /// <summary>Возвращает является ли вкладкой</summary>
        public bool f_IsTab()
        {
            return false;
        }

        /// <summary>ID элемента</summary>
        public int p_ID {
            get {
                if (p_Bookmark != null)
                    return (int)p_Bookmark.p_Type;
                return (int)E_Bookmarks.Bookmark_1;
            }
        }

        /// <summary>Наименование элемента</summary>
        public string p_Name {
            get {
                if (p_Bookmark != null)
                    return p_Bookmark.p_Text;
                return "";
            }
        }

        /// <summary>Флаг только чтения</summary>
        public bool p_ReadOnly { get; set; }

        /// <summary>Наименование элемента</summary>
        public Image p_ImageIcon {
            get {
                return (Image)Properties.Resources.ResourceManager.GetObject("TAB_16");
            }
        }

        /// <summary>Прорисовка контрола для дизайнера</summary>
        public void f_Draw(Graphics a_Graphics, Rectangle a_Bounds)
        {
            f_Draw(a_Graphics, a_Bounds, Font, ForeColor);
        }

        /// <summary>Прорисовка контрола для дизайнера</summary>
        public void f_Draw(Graphics a_Graphics, Rectangle a_Bounds, Font a_Font, Color a_ForeColor)
        {
            if (p_Bookmark != null)
            {
                Rectangle imageBounds = new Rectangle(a_Bounds.Left + 4, a_Bounds.Top + a_Bounds.Height / 2 - p_ImageIcon.Height / 2, p_ImageIcon.Width, p_ImageIcon.Height);
                Rectangle textBounds = new Rectangle(imageBounds.Right + 5, imageBounds.Top, a_Bounds.Width - (imageBounds.Right + 10), imageBounds.Height);
                a_Graphics.DrawImage(p_ImageIcon, imageBounds);
                TextRenderer.DrawText(a_Graphics, p_Name, a_Font, textBounds, a_ForeColor, TextFormatFlags.ExpandTabs | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter);
            }
        }
    }
}
