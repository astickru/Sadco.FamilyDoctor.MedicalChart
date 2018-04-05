using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sadco.FamilyDoctor.Core.Entities;

namespace Sadco.FamilyDoctor.Core.Controls.DesignerPanel
{
    public partial class Ctrl_Element : Panel, I_Element
    {
        public const int m_ElementHeight = 24;

        public Ctrl_Element()
        {
            InitializeComponent();
            Height = m_ElementHeight;
            BackColor = Color.Gray;
        }

        public Cl_Element m_Element = null;
        public Cl_Element p_Element {
            get {
                return m_Element;
            }
            set {
                m_Element = value;
            }
        }

        /// <summary>ID элемента</summary>
        public int p_ID {
            get {
                if (p_Element != null)
                    return p_Element.p_ID;
                return -1;
            }
        }

        /// <summary>Наименование элемента</summary>
        public string p_Name {
            get {
                if (p_Element != null)
                    return p_Element.p_Name;
                return "";
            }
        }

        private bool m_ReadOnly = false;
        /// <summary>Флаг только чтения</summary>
        public bool p_ReadOnly {
            get {
                return m_ReadOnly;
            }
            set {
                m_ReadOnly = value;
            }
        }

        /// <summary>Наименование элемента</summary>
        public Image p_ImageIcon {
            get {
                if (p_Element != null)
                    return (Image)Properties.Resources.ResourceManager.GetObject(p_Element.p_IconName);
                return null;
            }
        }

        /// <summary>Является ли элемент стройкой</summary>
        public bool f_IsLine {
            get {
                if (p_Element != null)
                    return p_Element.p_ElementType == Cl_Element.E_ElementsTypes.Line || p_Element.p_ElementType == Cl_Element.E_ElementsTypes.Bigbox;
                return false;
            }
        }

        /// <summary>Прорисовка контрола</summary>
        public void f_Draw(Graphics a_Graphics, Rectangle a_Bounds)
        {
            if (m_Element != null)
            {
                Rectangle imageBounds = new Rectangle(a_Bounds.Left + 4, a_Bounds.Top + a_Bounds.Height / 2 - p_ImageIcon.Height / 2, p_ImageIcon.Width, p_ImageIcon.Height);
                Rectangle textBounds = new Rectangle(imageBounds.Right + 5, imageBounds.Top, a_Bounds.Width - (imageBounds.Right + 10), imageBounds.Height);
                a_Graphics.DrawImage(p_ImageIcon, imageBounds);
                TextRenderer.DrawText(a_Graphics, p_Name, Font, textBounds, ForeColor, TextFormatFlags.ExpandTabs | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            f_Draw(e.Graphics, e.ClipRectangle);
            base.OnPaint(e);
        }
    }
}
