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
        
        public Cl_Template m_Template = null;
        public Cl_Template p_Template {
            get {
                return m_Template;
            }
            set {
                m_Template = value;
                Height = f_GetHeight(m_Template) + 25;
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

        /// <summary>Прорисовка контрола</summary>
        public void f_Draw(Graphics a_Graphics, Rectangle a_Bounds)
        {
            f_Draw(a_Graphics, a_Bounds, Font, ForeColor);
        }

        /// <summary>Прорисовка контрола</summary>
        public void f_Draw(Graphics a_Graphics, Rectangle a_Bounds, Font a_Font, Color a_ForeColor)
        {
            GroupBoxState state = base.Enabled ? GroupBoxState.Normal : GroupBoxState.Disabled;
            TextFormatFlags flags = TextFormatFlags.PreserveGraphicsTranslateTransform |
                TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.TextBoxControl |
                TextFormatFlags.WordBreak;
            Color titleColor = a_ForeColor;
            if (!this.ShowKeyboardCues)
                flags |= TextFormatFlags.HidePrefix;
            if (this.RightToLeft == RightToLeft.Yes)
                flags |= TextFormatFlags.RightToLeft | TextFormatFlags.Right;
            if (!this.Enabled)
                titleColor = SystemColors.GrayText;
            f_DrawUnthemedGroupBoxWithText(a_Graphics, a_Bounds, this.Text, a_Font, titleColor, flags, state);

            f_DrawTemplate(a_Graphics, a_Bounds, p_Template, a_Font, a_ForeColor);
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
            int startLeftHeader = 0;
            Size size = TextRenderer.MeasureText(g, groupBoxText, font, new Size(rectangle.Width, rectangle.Height), flags);
            rectangle.Width = size.Width;
            rectangle.Height = size.Height;
            if ((flags & TextFormatFlags.Right) == TextFormatFlags.Right)
                rectangle.X = (bounds.Right - rectangle.Width) - 12;
            else
                rectangle.X += 12;
            startLeftHeader = rectangle.X;

            Image imgIcon = (Image)Properties.Resources.ResourceManager.GetObject(p_Template.p_IconName);
            Rectangle imageBounds = new Rectangle(rectangle.X, bounds.Top, imgIcon.Width, imgIcon.Height);
            g.DrawImage(imgIcon, imageBounds);
            rectangle.X += imageBounds.Width + 3;

            TextRenderer.DrawText(g, groupBoxText, font, rectangle, titleColor, flags);
            if (rectangle.Width > 0)
                rectangle.Inflate(2, 0);
            using (var pen = new Pen(this.p_BorderColor))
            {
                int num = bounds.Top + (rectangle.Height / 2);
                g.DrawLine(pen, bounds.Left + 3, num, bounds.Left + 3, num + bounds.Height - 8);
                g.DrawLine(pen, bounds.Left + 3, bounds.Y + bounds.Height - 2, bounds.Width - 5, bounds.Y + bounds.Height - 2);
                g.DrawLine(pen, bounds.Left + 3, num, startLeftHeader - 5, num);
                g.DrawLine(pen, startLeftHeader + imageBounds.Width + rectangle.Width - 3, num, bounds.Width - 5, num);
                g.DrawLine(pen, bounds.Width - 4, num, bounds.Width - 4, num + bounds.Height - 8);
            }
        }

        private void f_DrawTemplate(Graphics a_Graphics, Rectangle a_Bounds, Cl_Template a_Template, Font a_Font, Color a_ForeColor)
        {
            if (a_Template != null)
            {
                if (a_Template.p_TemplateElements != null && a_Template.p_TemplateElements.Count > 0)
                {
                    int top = a_Bounds.Top + 25;
                    for (int i = 0; i < a_Template.p_TemplateElements.Count; i++)
                    {
                        var te = a_Template.p_TemplateElements.ElementAt(i);
                        if (te.p_ChildTemplate != null)
                        {
                            f_DrawTemplate(a_Graphics, a_Bounds, te.p_ChildTemplate, a_Font, a_ForeColor);
                        }
                        else if (te.p_ChildElement != null)
                        {
                            Image imgIcon = (Image)Properties.Resources.ResourceManager.GetObject(te.p_ChildElement.p_IconName);
                            Rectangle imageBounds = new Rectangle(a_Bounds.Left + 7, top, imgIcon.Width, imgIcon.Height);
                            Rectangle textBounds = new Rectangle(imageBounds.Right + 5, imageBounds.Top, a_Bounds.Width - (imageBounds.Right + 10), imageBounds.Height);
                            a_Graphics.DrawImage(imgIcon, imageBounds);
                            TextRenderer.DrawText(a_Graphics, te.p_ChildElement.p_Name, a_Font, textBounds, a_ForeColor, TextFormatFlags.ExpandTabs | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter);
                            if (p_Template.p_Type == Cl_Template.E_TemplateType.Table && i < a_Template.p_TemplateElements.Count - 1)
                            {
                                a_Graphics.DrawLine(new Pen(this.p_BorderColor, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash }, a_Bounds.Left + 3, top - 5 + Ctrl_Element.m_ElementHeight, a_Bounds.Width - 5, top - 5 + Ctrl_Element.m_ElementHeight);
                            }
                            top += Ctrl_Element.m_ElementHeight;
                        }
                    }
                }
            }
        }
    }
}
