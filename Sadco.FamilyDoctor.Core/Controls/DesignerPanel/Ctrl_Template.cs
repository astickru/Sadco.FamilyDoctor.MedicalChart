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
using System.Data.Entity;

namespace Sadco.FamilyDoctor.Core.Controls.DesignerPanel
{
    public partial class Ctrl_Template : UserControl, I_Element
    {
        public Ctrl_Template()
        {
            InitializeComponent();
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

        private int m_PaddingX = 5;
        [Category("p_Padding")]
        [DefaultValue(false)]
        public int p_PaddingX {
            get { return m_PaddingX; }
            set {
                if (this.m_PaddingX != value)
                {
                    m_PaddingX = value;
                }
            }
        }

        private int m_PaddingY = 5;
        [Category("p_Padding")]
        [DefaultValue(false)]
        public int p_PaddingY {
            get { return m_PaddingY; }
            set {
                if (this.m_PaddingY != value)
                {
                    m_PaddingY = value;
                }
            }
        }

        /// <summary>Получение высоты шаблона</summary>
        public int f_GetHeight()
        {
            return f_GetHeight(p_Template);
        }

        /// <summary>Прорисовка контрола для дизайнера</summary>
        public void f_Draw(Graphics a_Graphics, Rectangle a_Bounds)
        {
            f_Draw(a_Graphics, a_Bounds, Font, ForeColor);
        }

        /// <summary>Прорисовка контрола для дизайнера</summary>
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

        private TableLayoutPanel f_GetControlTable()
        {
            var ctrlTable = new TableLayoutPanel();
            ctrlTable.ColumnCount = 5;
            ctrlTable.ColumnStyles.Add(new ColumnStyle());
            ctrlTable.ColumnStyles.Add(new ColumnStyle());
            ctrlTable.ColumnStyles.Add(new ColumnStyle());
            ctrlTable.ColumnStyles.Add(new ColumnStyle());
            ctrlTable.ColumnStyles.Add(new ColumnStyle());
            ctrlTable.RowCount = 0;
            return ctrlTable;
        }

        /// <summary>Инициализация пользовательских контролов</summary>
        private void f_AddControlsTemplate(Cl_Template a_Template, ControlCollection a_Controls = null)
        {
            if (a_Template != null)
            {
                if (a_Template.p_TemplateElements == null)
                {
                    var cTe = Cl_App.m_DataContext.Entry(a_Template).Collection(g => g.p_TemplateElements).Query().Include(te => te.p_ChildElement).Include(te => te.p_ChildTemplate);
                    cTe.Load();
                }
                if (a_Template.p_TemplateElements != null && a_Template.p_TemplateElements.Count > 0)
                {
                    int top = 0;
                    ControlCollection controls = null;
                    if (a_Controls != null)
                    {
                        controls = a_Controls;
                    }
                    else
                        controls = ctrlContent.Controls;
                    foreach (var te in a_Template.p_TemplateElements)
                    {
                        if (te.p_ChildElement != null)
                        {
                            var ctrlEl = new Ctrl_Element();
                            ctrlEl.p_Element = te.p_ChildElement;
                            if (controls is TableLayoutControlCollection && controls.Owner is TableLayoutPanel)
                            {
                                var table = (TableLayoutPanel)controls.Owner;
                                table.RowCount++;
                                table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                                ctrlEl.f_InitUIControls(table, table.RowCount - 1);
                            }
                            else
                                ctrlEl.f_InitUIControls();

                            ctrlEl.Top = top;
                            ctrlEl.Left = m_PaddingX;
                            top += ctrlEl.Height + m_PaddingY;
                            controls.Add(ctrlEl);
                        }
                        else if (te.p_ChildTemplate != null)
                        {
                            if (te.p_ChildTemplate.p_Type == Cl_Template.E_TemplateType.Block)
                            {
                                var ctrlGroup = new GroupBox();
                                ctrlGroup.Text = te.p_ChildTemplate.p_Name;
                                ctrlGroup.AutoSize = true;
                                ctrlGroup.Top = top;
                                FlowLayoutPanel panel = new FlowLayoutPanel();
                                panel.Top = 20;
                                panel.Left = 3;
                                panel.WrapContents = false;
                                panel.AutoSize = true;
                                panel.FlowDirection = FlowDirection.TopDown;
                                ctrlGroup.Controls.Add(panel);
                                controls.Add(ctrlGroup);
                                f_AddControlsTemplate(te.p_ChildTemplate, panel.Controls);
                                top += ctrlGroup.Height + m_PaddingY;
                            }
                            else if (te.p_ChildTemplate.p_Type == Cl_Template.E_TemplateType.Table)
                            {
                                var ctrlTable = f_GetControlTable();
                                ctrlTable.AutoSize = true;
                                top += 10;
                                ctrlTable.Top = top;
                                ctrlTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
                                ctrlTable.RowCount++;
                                ctrlTable.Controls.Add(new Label() { Text = "Показатель", TextAlign = System.Drawing.ContentAlignment.MiddleLeft }, 0, 0);
                                ctrlTable.Controls.Add(new Label() { Text = "Локация", TextAlign = System.Drawing.ContentAlignment.MiddleLeft }, 1, 0);
                                ctrlTable.Controls.Add(new Label() { Text = "Значение", TextAlign = System.Drawing.ContentAlignment.MiddleLeft }, 2, 0);
                                ctrlTable.Controls.Add(new Label() { Text = "Ед. изм.", TextAlign = System.Drawing.ContentAlignment.MiddleLeft }, 3, 0);
                                ctrlTable.Controls.Add(new Label() { Text = "Нормa", TextAlign = System.Drawing.ContentAlignment.MiddleLeft }, 4, 0);
                                
                                controls.Add(ctrlTable);
                                f_AddControlsTemplate(te.p_ChildTemplate, ctrlTable.Controls);
                                top += ctrlTable.Height + m_PaddingY;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>Инициализация пользовательских контролов</summary>
        public void f_InitUIControls()
        {
            ctrlContent.Controls.Clear();
            f_AddControlsTemplate(m_Template);
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
                    int top = a_Bounds.Top + 20;
                    for (int i = 0; i < a_Template.p_TemplateElements.Count; i++)
                    {
                        var te = a_Template.p_TemplateElements.ElementAt(i);
                        if (te.p_ChildTemplate != null)
                        {
                            f_DrawTemplate(a_Graphics, a_Bounds, te.p_ChildTemplate, a_Font, a_ForeColor);
                        }
                        else if (te.p_ChildElement != null)
                        {
                            var ctrlEl = new Ctrl_Element() { p_Element = te.p_ChildElement };
                            ctrlEl.f_Draw(a_Graphics, new Rectangle(a_Bounds.Left + 4, top, a_Bounds.Width, Ctrl_Element.m_ElementHeight), a_Font, a_ForeColor);
                            if (p_Template.p_Type == Cl_Template.E_TemplateType.Table && i < a_Template.p_TemplateElements.Count - 1)
                            {
                                a_Graphics.DrawLine(new Pen(this.p_BorderColor, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash }, a_Bounds.Left + 3, top + Ctrl_Element.m_ElementHeight, a_Bounds.Width - 5, top + Ctrl_Element.m_ElementHeight);
                            }
                            top += Ctrl_Element.m_ElementHeight;
                        }
                    }
                }
            }
        }
    }
}
