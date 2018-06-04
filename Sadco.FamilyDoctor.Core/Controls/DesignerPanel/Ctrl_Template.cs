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
using FD.dat.mon.stb.lib;
using Sadco.FamilyDoctor.Core.Facades;

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
                    Text = string.Format("{0} ({1})", p_Name, m_Template.p_Version == 0 ? "Черновик" : "v" + m_Template.p_Version);
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
            ctrlTable.ColumnCount = 4;
            ctrlTable.ColumnStyles.Add(new ColumnStyle());
            ctrlTable.ColumnStyles.Add(new ColumnStyle());
            ctrlTable.ColumnStyles.Add(new ColumnStyle());
            ctrlTable.ColumnStyles.Add(new ColumnStyle());
            ctrlTable.RowCount = 0;
            return ctrlTable;
        }

        private List<Ctrl_Element> m_Elements = new List<Ctrl_Element>();

        private Cl_Record m_Record = null;
        /// <summary>Инициализация пользовательских контролов</summary>
        private void f_AddControlsTemplate(Cl_Template a_Template, ControlCollection a_Controls = null)
        {
            if (m_Record != null && a_Template != null)
            {
                if (a_Template.p_TemplateElements == null)
                {
                    var cTe = Cl_App.m_DataContext.Entry(a_Template).Collection(g => g.p_TemplateElements).Query().Include(te => te.p_ChildElement).Include(te => te.p_ChildElement.p_Default).Include(te => te.p_ChildElement.p_ParamsValues).Include(te => te.p_ChildElement.p_PartAgeNorms).Include(te => te.p_ChildTemplate);
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
                            ctrlEl.e_ValueChanged += CtrlEl_e_ValueChanged;

                            Cl_RecordValue recval = m_Record.p_Values.FirstOrDefault(v => v.p_ElementID == te.p_ChildElement.p_ID);
                            if (recval == null)
                            {
                                recval = new Cl_RecordValue();
                                recval.p_ElementID = ctrlEl.p_Element.p_ID;
                                recval.p_Element = ctrlEl.p_Element;
                                recval.p_RecordID = m_Record.p_ID;
                                recval.p_Record = m_Record;
                            }

                            if (controls is TableLayoutControlCollection && controls.Owner is TableLayoutPanel)
                            {
                                var table = (TableLayoutPanel)controls.Owner;
                                table.RowCount++;
                                table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                                ctrlEl.f_SetRecordElementValues(recval, table, table.RowCount - 1);
                            }
                            else
                            {
                                ctrlEl.f_SetRecordElementValues(recval);
                                controls.Add(ctrlEl);
                            }

                            ctrlEl.Top = top;
                            ctrlEl.Left = m_PaddingX;
                            top += ctrlEl.Height + m_PaddingY;
                            m_Elements.Add(ctrlEl);
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
                                ctrlTable.RowCount = 1;
                                ctrlTable.Controls.Add(new Label() { Text = "Показатель", TextAlign = System.Drawing.ContentAlignment.MiddleLeft }, 0, 0);
                                //ctrlTable.Controls.Add(new Label() { Text = "Локация", TextAlign = System.Drawing.ContentAlignment.MiddleLeft }, 1, 0);
                                ctrlTable.Controls.Add(new Label() { Text = "Значение", TextAlign = System.Drawing.ContentAlignment.MiddleLeft }, 1, 0);
                                ctrlTable.Controls.Add(new Label() { Text = "Ед. изм.", TextAlign = System.Drawing.ContentAlignment.MiddleLeft }, 2, 0);
                                ctrlTable.Controls.Add(new Label() { Text = "Нормa", TextAlign = System.Drawing.ContentAlignment.MiddleLeft }, 3, 0);

                                controls.Add(ctrlTable);
                                f_AddControlsTemplate(te.p_ChildTemplate, ctrlTable.Controls);
                                top += ctrlTable.Height + m_PaddingY;
                            }
                        }
                    }
                }
            }
        }

        private bool m_IsBlockChanging = false;
        private void CtrlEl_e_ValueChanged(object sender, EventArgs e)
        {
            var curEl = (Ctrl_Element)sender;
            if (!m_IsBlockChanging)
            {
                m_IsBlockChanging = true;
                var record = f_GetNewRecord();
                if (record != null)
                {
                    foreach (var el in m_Elements)
                    {
                        if (el.p_Element != null && el != curEl)
                        {
                            el.Visible = Cl_RecordsFacade.f_GetInstance().f_GetElementVisible(record, el.p_Element.p_VisibilityFormula);
                            if (el.p_Element.p_IsNumber)
                            {
                                var val = Cl_RecordsFacade.f_GetInstance().f_GetElementMathematicValue(record, el.p_Element.p_NumberFormula);
                                if (val != null)
                                {
                                    var dVal = (decimal)val;
                                    dVal = Math.Round(dVal, el.p_Element.p_NumberRound);
                                    el.f_SetValue(record, dVal);
                                }
                            }
                        }
                    }
                }
                m_IsBlockChanging = false;
            }
        }

        /// <summary>Установка записи</summary>
        public void f_SetRecord(Cl_Record a_Record)
        {
            ctrlContent.Controls.Clear();
            m_Record = a_Record;
            if (m_Record.p_Version > 0)
            {
                if (m_Record.p_Values == null)
                {
                    var recs = Cl_App.m_DataContext.Entry(m_Record).Collection(g => g.p_Values).Query().Include(te => te.p_Element).Include(te => te.p_Element.p_Default).Include(te => te.p_Params);
                    recs.Load();
                }
            }
            if (m_Record.p_Values == null)
            {
                m_Record.p_Values = new List<Cl_RecordValue>();
            }
            f_AddControlsTemplate(m_Template);
        }

        /// <summary>Получение новой версии записи</summary>
        public Cl_Record f_GetNewRecord()
        {
            if (m_Template == null || m_Record == null) return null;
            var record = new Cl_Record();
            record.p_RecordID = m_Record.p_RecordID;
            record.p_DateLastChange = DateTime.Now;
            record.p_Template = m_Template;
            record.p_ClinikName = m_Record.p_ClinikName;
            record.p_Title = m_Record.p_Title;
            record.p_CategoryTotalID = m_Record.p_CategoryTotalID;
            record.p_CategoryTotal = m_Record.p_CategoryTotal;
            record.p_CategoryClinikID = m_Record.p_CategoryClinikID;
            record.p_CategoryClinik = m_Record.p_CategoryClinik;
            record.p_UserID = m_Record.p_UserID;
            record.p_UserSurName = m_Record.p_UserSurName;
            record.p_UserName = m_Record.p_UserName;
            record.p_UserLastName = m_Record.p_UserLastName;
            record.p_PatientID = m_Record.p_PatientID;
            record.p_PatientSurName = m_Record.p_PatientSurName;
            record.p_PatientName = m_Record.p_PatientName;
            record.p_PatientLastName = m_Record.p_PatientLastName;
            record.p_Sex = m_Record.p_Sex;
            record.p_DateBirth = m_Record.p_DateBirth;
            record.p_DateCreate = m_Record.p_DateCreate;
            record.p_DateForming = m_Record.p_DateForming;
            record.p_Version = m_Record.p_Version + 1;
            record.p_Values = new List<Cl_RecordValue>();
            foreach (var el in m_Elements)
            {
                var recEl = el.f_GetRecordElementValues(record);
                if (recEl != null)
                {
                    record.p_Values.Add(recEl);
                }
                else
                {
                    return null;
                }
            }
            return record;
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
