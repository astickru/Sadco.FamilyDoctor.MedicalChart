using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sadco.FamilyDoctor.Core.Entities;
using System.Windows.Forms.VisualStyles;

namespace Sadco.FamilyDoctor.Core.Controls.DesignerPanel
{
    public partial class Ctrl_Template : GroupBox, I_Element
    {
        public Ctrl_Template()
        {
            InitializeComponent();
            BackColor = Color.Gray;
        }

        private Color m_BorderColor = Color.Black;
        [DefaultValue(typeof(Color), "Black")]
        public Color p_BorderColor {
            get { return m_BorderColor; }
            set { m_BorderColor = value; this.Invalidate(); }
        }
        private Color m_TextColor = Color.Black;
        [DefaultValue(typeof(Color), "Black")]
        public Color p_TextColor {
            get { return m_TextColor; }
            set { m_TextColor = value; this.Invalidate(); }
        }

        public Cl_Template m_Template = null;
        public Cl_Template p_Template {
            get {
                return m_Template;
            }
            set {
                m_Template = value;
                Height = f_GetHeight(m_Template);
                if (m_Template != null)
                {
                    Text = m_Template.p_Name;
                }
            }
        }

        /// <summary>ID элемента</summary>
        public int p_ID {
            get {
                if (p_Template != null)
                    return p_Template.p_ID;
                return -1;
            }
        }

        /// <summary>Наименование элемента</summary>
        public string p_Name {
            get {
                if (p_Template != null)
                    return p_Template.p_Name;
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
                if (p_Template != null)
                    return (Image)Properties.Resources.ResourceManager.GetObject(p_Template.p_IconName);
                return null;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            f_Draw(e.Graphics, e.ClipRectangle);
            base.OnPaint(e);
        }

        public int f_GetHeight()
        {
            return f_GetHeight(p_Template);
        }

        public void f_Draw(Graphics a_Graphics, Rectangle a_Bounds)
        {
            GroupBoxState state = base.Enabled ? GroupBoxState.Normal : GroupBoxState.Disabled;
            TextFormatFlags flags = TextFormatFlags.PreserveGraphicsTranslateTransform |
                TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.TextBoxControl |
                TextFormatFlags.WordBreak;
            Color titleColor = this.p_TextColor;
            if (!this.ShowKeyboardCues)
                flags |= TextFormatFlags.HidePrefix;
            if (this.RightToLeft == RightToLeft.Yes)
                flags |= TextFormatFlags.RightToLeft | TextFormatFlags.Right;
            if (!this.Enabled)
                titleColor = SystemColors.GrayText;
            f_DrawUnthemedGroupBoxWithText(a_Graphics, a_Bounds, this.Text, this.Font, titleColor, flags, state);
            //RaisePaintEvent(this, e);

            f_DrawTemplate(a_Graphics, a_Bounds, p_Template);
        }

        private int f_GetHeight(Cl_Template a_Template)
        {
            if (a_Template != null && a_Template.p_TemplateElements != null)
            {
                int height = 0;
                foreach (var te in a_Template.p_TemplateElements)
                {
                    if (te.p_ChildTemplate != null)
                    {
                        height += f_GetHeight(te.p_ChildTemplate);
                    }
                    else if (te.p_ChildElement != null)
                    {
                        height += Ctrl_Element.m_ElementHeight;
                    }
                }
                return height;
            }
            return Ctrl_Element.m_ElementHeight;
        }

        private void f_DrawUnthemedGroupBoxWithText(Graphics g, Rectangle bounds, string groupBoxText, Font font, Color titleColor, TextFormatFlags flags, GroupBoxState state)
        {
            Rectangle rectangle = bounds;
            rectangle.Width -= 8;
            Size size = TextRenderer.MeasureText(g, groupBoxText, font, new Size(rectangle.Width, rectangle.Height), flags);
            rectangle.Width = size.Width;
            rectangle.Height = size.Height;
            if ((flags & TextFormatFlags.Right) == TextFormatFlags.Right)
                rectangle.X = (bounds.Right - rectangle.Width) - 12;
            else
                rectangle.X += 12;
            TextRenderer.DrawText(g, groupBoxText, font, rectangle, titleColor, flags);
            if (rectangle.Width > 0)
                rectangle.Inflate(2, 0);
            using (var pen = new Pen(this.p_BorderColor))
            {
                int num = bounds.Top + (rectangle.Height / 2);
                g.DrawLine(pen, bounds.Left + 3, num, bounds.Left + 3, num + bounds.Height - 8);
                g.DrawLine(pen, bounds.Left + 3, bounds.Y + bounds.Height - 2, bounds.Width - 5, bounds.Y + bounds.Height - 2);
                g.DrawLine(pen, bounds.Left + 3, num, rectangle.X - 1, num);
                g.DrawLine(pen, rectangle.X + rectangle.Width - 3, num, bounds.Width - 5, num);
                
                g.DrawLine(pen, bounds.Width - 4, num, bounds.Width - 4, num + bounds.Height - 8);
            }
        }

        private void f_DrawTemplate(Graphics a_Graphics, Rectangle a_Bounds, Cl_Template a_Template)
        {
            if (a_Template != null)
            {
                if (a_Template.p_TemplateElements != null && a_Template.p_TemplateElements.Count > 0)
                {
                    int top = a_Bounds.Top;
                    foreach (var te in a_Template.p_TemplateElements)
                    {
                        if (te.p_ChildTemplate != null)
                        {
                            f_DrawTemplate(a_Graphics, a_Bounds, te.p_ChildTemplate);
                        }
                        else if (te.p_ChildElement != null)
                        {
                            Image imgIcon = (Image)Properties.Resources.ResourceManager.GetObject(te.p_ChildElement.p_IconName);
                            //Rectangle imageBounds = new Rectangle(a_Bounds.Left + 4, a_Bounds.Top + a_Bounds.Height / 2 - imgIcon.Height / 2, imgIcon.Width, imgIcon.Height);
                            Rectangle imageBounds = new Rectangle(a_Bounds.Left + 4, top, imgIcon.Width, imgIcon.Height);
                            Rectangle textBounds = new Rectangle(imageBounds.Right + 5, imageBounds.Top, a_Bounds.Width - (imageBounds.Right + 10), imageBounds.Height);
                            //a_Graphics.DrawImage(imgIcon, imageBounds);
                            //TextRenderer.DrawText(a_Graphics, te.p_ChildElement.p_Name, new Font(FontFamily.GenericSansSerif, 50, FontStyle.Bold), textBounds, ForeColor, TextFormatFlags.ExpandTabs | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter);
                            top += Ctrl_Element.m_ElementHeight;
                        }
                    }
                }
            }
        }
    }
}
