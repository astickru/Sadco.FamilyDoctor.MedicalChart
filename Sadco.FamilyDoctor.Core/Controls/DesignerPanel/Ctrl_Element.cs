using Sadco.FamilyDoctor.Core.Entities;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.Core.Controls.DesignerPanel
{
    public partial class Ctrl_Element : UserControl, I_Element
    {
        public const int m_ElementHeight = 24;

        public Ctrl_Element()
        {
            InitializeComponent();
            Height = m_ElementHeight;
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

        /// <summary>Прорисовка контрола для дизайнера</summary>
        public void f_Draw(Graphics a_Graphics, Rectangle a_Bounds)
        {
            f_Draw(a_Graphics, a_Bounds, Font, ForeColor);
        }

        /// <summary>Прорисовка контрола для дизайнера</summary>
        public void f_Draw(Graphics a_Graphics, Rectangle a_Bounds, Font a_Font, Color a_ForeColor)
        {
            if (m_Element != null)
            {
                Rectangle imageBounds = new Rectangle(a_Bounds.Left + 4, a_Bounds.Top + a_Bounds.Height / 2 - p_ImageIcon.Height / 2, p_ImageIcon.Width, p_ImageIcon.Height);
                Rectangle textBounds = new Rectangle(imageBounds.Right + 5, imageBounds.Top, a_Bounds.Width - (imageBounds.Right + 10), imageBounds.Height);
                a_Graphics.DrawImage(p_ImageIcon, imageBounds);
                TextRenderer.DrawText(a_Graphics, string.Format("{0} ({1})", p_Name, p_Element.p_Version == 0 ? "Черновик" : "v" + p_Element.p_Version), a_Font, textBounds, a_ForeColor, TextFormatFlags.ExpandTabs | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter);
            }
        }

        /// <summary>Инициализация пользовательских контролов</summary>
        public void f_InitUIControls()
        {
            f_InitUIControls(null, 0);
        }

        /// <summary>Инициализация пользовательских контролов</summary>
        public void f_InitUIControls(TableLayoutPanel a_Table, int a_RowIndex)
        {
            FlowLayoutPanel panel = null;
            if (a_Table == null)
            {
                panel = new FlowLayoutPanel();
                panel.WrapContents = false;
                panel.Height = 20;
                panel.AutoSize = true;
                Controls.Add(panel);
            }
            Label l = null;
            TextBox tb = null;
            ComboBox cb = null;
            if (p_Element.p_IsPartPre)
            {
                l = new Label() { Text = p_Element.p_PartPre };
                l.TextAlign = ContentAlignment.MiddleLeft;
                if (a_Table != null)
                    a_Table.Controls.Add(l, 0, a_RowIndex);
                else
                    panel.Controls.Add(l);
            }
            if (p_Element.p_IsPartLocations && p_Element.p_PartLocations != null && p_Element.p_PartLocations.Length > 0)
            {
                cb = new ComboBox();
                cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb.AutoCompleteSource = AutoCompleteSource.CustomSource;
                cb.AutoCompleteCustomSource.AddRange(p_Element.p_PartLocations.Select(e => e.p_Value).ToArray());
                cb.DataSource = new BindingSource(p_Element.p_PartLocations, null);
                cb.DisplayMember = "p_Value";
                cb.ValueMember = "p_ID";
                cb.Width = 200;
                
                if (a_Table != null)
                    a_Table.Controls.Add(cb, 1, a_RowIndex);
                else
                    panel.Controls.Add(cb);
            }
            if (p_Element.p_IsListBox)
            {
                var scb = new Ctrl_SeparatorCombobox();
                scb.FormattingEnabled = true;
                scb.p_SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                scb.Width = 200;
                foreach (var val in p_Element.p_NormValues)
                {
                    scb.f_AddObject(val);
                }
                scb.f_SetSeparator(p_Element.p_NormValues.Length);
                foreach (var val in p_Element.p_PatValues)
                {
                    scb.f_AddObject(val);
                }
                if (scb.Items.Count > 0)
                    scb.SelectedIndex = 0;
                if (a_Table != null)
                    a_Table.Controls.Add(scb, 2, a_RowIndex);
                else
                    panel.Controls.Add(scb);
            }
            else
            {
                if (p_Element.p_ElementType == Cl_Element.E_ElementsTypes.Float || p_Element.p_ElementType == Cl_Element.E_ElementsTypes.Line)
                {
                    tb = new TextBox();
                    tb.Width = 200;
                    if (a_Table != null)
                        a_Table.Controls.Add(tb, 2, a_RowIndex);
                    else
                        panel.Controls.Add(tb);
                }
                else
                {
                    Ctrl_TextBoxAutoHeight tbh = new Ctrl_TextBoxAutoHeight() { p_MinLines = 3 };
                    tbh.Width = 200;
                    if (a_Table != null)
                        a_Table.Controls.Add(tbh, 2, a_RowIndex);
                    else
                        panel.Controls.Add(tbh);
                }
                
            }
            if (p_Element.p_IsPartPost)
            {
                l = new Label() { Text = p_Element.p_PartPost };
                l.TextAlign = ContentAlignment.MiddleLeft;
                if (a_Table != null)
                    a_Table.Controls.Add(l, 3, a_RowIndex);
                else
                    panel.Controls.Add(l);
            }
            if (p_Element.p_IsPartNorm)
            {
                l = new Label() { Text = p_Element.p_PartNorm.ToString() };
                l.TextAlign = ContentAlignment.MiddleLeft;
                if (a_Table != null)
                    a_Table.Controls.Add(l, 4, a_RowIndex);
                else
                    panel.Controls.Add(l);
            }
            else if (p_Element.p_IsPartNormRange && p_Element.p_PartAgeNorms != null && p_Element.p_PartAgeNorms.Count > 0)
            {
                cb = new ComboBox();

                //cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                //cb.AutoCompleteSource = AutoCompleteSource.CustomSource;
                //m_Elements = Cl_App.m_DataContext.p_Elements.Where(e => !e.p_IsArhive && e.p_ElementType != Cl_Element.E_ElementsTypes.Image).GroupBy(e => e.p_ElementID)
                //                    .Select(grp => grp
                //                                .OrderByDescending(v => v.p_Version).FirstOrDefault()).ToArray();
                ////cb.AutoCompleteCustomSource.AddRange(p_Element.p_PartAgeNorms.Select(e => e.p_).ToArray());
                //cb.AutoCompleteCustomSource.AddRange(p_Element.p_PartAgeNorms.Select(e => e.p_).ToArray());
                //cb.DataSource = new BindingSource(m_Elements, null);
                //cb.DisplayMember = "p_Name";
                //cb.ValueMember = "p_Tag";

                cb.Width = 200;
                foreach (var loc in p_Element.p_PartAgeNorms)
                {
                    cb.Items.Add(loc);
                }
                cb.SelectedIndex = 0;
                if (a_Table != null)
                    a_Table.Controls.Add(cb, 4, a_RowIndex);
                else
                    panel.Controls.Add(cb);
            }
        }
    }
}
