using Sadco.FamilyDoctor.Core.Entities;
using System.Drawing;
using System.Windows.Forms;
using System;
using System.Linq;
using FD.dat.mon.stb.lib;
using System.Collections.Generic;
using Sadco.FamilyDoctor.Core.Facades;

namespace Sadco.FamilyDoctor.Core.Controls.DesignerPanel
{
    public partial class Ctrl_Element : UserControl, I_Element
    {
        public const int m_ElementHeight = 24;

        public Ctrl_Element()
        {
            InitializeComponent();
            Height = m_ElementHeight;

            m_ToolTip.AutoPopDelay = 5000;
            m_ToolTip.InitialDelay = 1000;
            m_ToolTip.ReshowDelay = 500;
            m_ToolTip.ShowAlways = true;
        }

        ToolTip m_ToolTip = new ToolTip();

        public Cl_Element m_Element = null;
        public Cl_Element p_Element {
            get {
                return m_Element;
            }
            set {
                m_Element = value;
                m_ToolTip.SetToolTip(this, m_Element.p_Help);
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

        /// <summary>Установка значения элемента записи</summary>
        public void f_SetRecordElementValues(Cl_RecordValue a_RecordValue)
        {
            f_SetRecordElementValues(a_RecordValue, null, 0);
        }

        public event EventHandler e_ValueChanged;

        private string m_SeparatorMulti = ", ";
        private Cl_Record m_Record = null;
        private ComboBox ctrl_PartLocations;
        private Ctrl_CheckedComboBox ctrl_PartLocationsMulti;
        private Ctrl_SeparatorCombobox ctrl_Values;
        private Ctrl_CheckedComboBox ctrl_ValuesMulti;
        private TextBox ctrl_Value;
        private Ctrl_TextBoxAutoHeight ctrl_ValueBox;
        private Ctrl_SeparatorCombobox ctrl_DopValues;
        private Ctrl_CheckedComboBox ctrl_DopValuesMulti;
        private TextBox ctrl_DopValue;
        private Ctrl_TextBoxAutoHeight ctrl_DopValueBox;
        private Ctrl_Paint ctrl_Image;
        private decimal? m_Min = null;
        private decimal? m_Max = null;

        private Panel f_GetSymmetricalPanel(Control a_LeftControl, Control a_RightControl, string a_LeftText, string a_RightText)
        {
            var cellPanel = new TableLayoutPanel();
            cellPanel.AutoSize = true;
            cellPanel.Margin = new Padding(0);

            var title = new Label();
            title.Text = a_LeftText;
            title.RightToLeft = RightToLeft.Yes;
            title.AutoSize = true;
            title.Margin = new Padding(0, 6, 0, 0);
            cellPanel.Controls.Add(title, 0, 0);
            cellPanel.Controls.Add(a_LeftControl, 1, 0);

            title = new Label();
            title.Text = a_RightText;
            title.RightToLeft = RightToLeft.Yes;
            title.AutoSize = true;
            title.Margin = new Padding(0, 6, 0, 0);
            cellPanel.Controls.Add(title, 0, 1);
            cellPanel.Controls.Add(a_RightControl, 1, 1);

            return cellPanel;
        }

        /// <summary>Инициализация пользовательских контролов для записи</summary>
        public void f_SetRecordElementValues(Cl_RecordValue a_RecordValue, TableLayoutPanel a_Table, int a_RowIndex)
        {
            if (a_RecordValue == null || a_RecordValue.p_Record == null || p_Element == null) return;
            Visible = Cl_RecordsFacade.f_GetInstance().f_GetElementVisible(a_RecordValue.p_Record, p_Element.p_VisibilityFormula);

            m_Record = a_RecordValue.p_Record;
            ctrl_PartLocations = null;
            ctrl_PartLocationsMulti = null;
            ctrl_Values = null;
            ctrl_ValuesMulti = null;
            ctrl_Value = null;
            ctrl_ValueBox = null;
            ctrl_DopValues = null;
            ctrl_DopValuesMulti = null;
            ctrl_DopValue = null;
            ctrl_DopValueBox = null;
            ctrl_Image = null;

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
            if (p_Element.p_IsText)
            {
                byte age = a_RecordValue.p_Record.p_MedicalCard.f_GetPatientAge();
                var partNorm = p_Element.f_GetPartNormValue(a_RecordValue.p_Record.p_MedicalCard.p_PatientSex, age, out m_Min, out m_Max);
                if (p_Element.p_IsPartPre)
                {
                    l = new Label() { Text = p_Element.p_PartPre };
                    l.AutoSize = true;
                    l.Margin = new Padding(0, 6, 0, 0);
                    if (a_Table != null)
                        a_Table.Controls.Add(l, 0, a_RowIndex);
                    else
                        panel.Controls.Add(l);
                }
                if (p_Element.p_IsPartLocations && p_Element.p_PartLocations != null && p_Element.p_PartLocations.Length > 0 && a_Table == null)
                {
                    if (p_Element.p_IsPartLocationsMulti)
                    {
                        ctrl_PartLocationsMulti = new Ctrl_CheckedComboBox();
                        ctrl_PartLocationsMulti.Enabled = p_Element.p_Editing;
                        ctrl_PartLocationsMulti.DisplayMember = "p_Value";
                        ctrl_PartLocationsMulti.ValueMember = "p_ID";
                        ctrl_PartLocationsMulti.Width = 300;
                        ctrl_PartLocationsMulti.MaxDropDownItems = 10;
                        ctrl_PartLocationsMulti.ValueSeparator = m_SeparatorMulti;
                        for (int i = 0; i < p_Element.p_PartLocations.Length; i++)
                        {
                            ctrl_PartLocationsMulti.Items.Add(p_Element.p_PartLocations[i]);
                            if (a_RecordValue.p_Params != null)
                            {
                                var rValue = a_RecordValue.p_PartLocations.FirstOrDefault(rv => rv.p_ElementParamID == p_Element.p_PartLocations[i].p_ID);
                                if (rValue != null)
                                    ctrl_PartLocationsMulti.SetItemChecked(i, true);
                            }
                        }
                        panel.Controls.Add(ctrl_PartLocationsMulti);
                    }
                    else
                    {
                        ctrl_PartLocations = new ComboBox();
                        ctrl_PartLocations.Enabled = p_Element.p_Editing;
                        ctrl_PartLocations.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        ctrl_PartLocations.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        ctrl_PartLocations.AutoCompleteCustomSource.AddRange(p_Element.p_PartLocations.Select(e => e.p_Value).ToArray());
                        foreach (var pLoc in p_Element.p_PartLocations)
                        {
                            ctrl_PartLocations.Items.Add(pLoc);
                        }
                        ctrl_PartLocations.DisplayMember = "p_Value";
                        ctrl_PartLocations.ValueMember = "p_ID";
                        ctrl_PartLocations.Width = 300;
                        var lParam = a_RecordValue.p_PartLocations.FirstOrDefault();
                        if (lParam != null)
                            ctrl_PartLocations.SelectedItem = p_Element.p_PartLocations.FirstOrDefault(pl => pl.p_ID == lParam.p_ElementParamID);
                        panel.Controls.Add(ctrl_PartLocations);
                    }
                }
                if (p_Element.p_IsTextFromCatalog)
                {
                    var normValues = p_Element.p_NormValues.Select(e => e.p_Value).ToArray();
                    var patValues = p_Element.p_PatValues.Select(e => e.p_Value).ToArray();
                    if (p_Element.p_IsMultiSelect)
                    {
                        ctrl_ValuesMulti = new Ctrl_CheckedComboBox();
                        ctrl_ValuesMulti.Enabled = p_Element.p_Editing;
                        ctrl_ValuesMulti.p_SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                        ctrl_ValuesMulti.ValueSeparator = m_SeparatorMulti;
                        ctrl_ValuesMulti.DisplayMember = "p_Value";
                        ctrl_ValuesMulti.ValueMember = "p_ID";
                        ctrl_ValuesMulti.Width = 300;
                        ctrl_ValuesMulti.ItemCheck += Ctrl_ValueChanged;
                        if (p_Element.p_Symmetrical)
                        {
                            ctrl_DopValuesMulti = new Ctrl_CheckedComboBox();
                            ctrl_DopValuesMulti.Enabled = p_Element.p_Editing;
                            ctrl_DopValuesMulti.p_SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                            ctrl_DopValuesMulti.ValueSeparator = m_SeparatorMulti;
                            ctrl_DopValuesMulti.DisplayMember = "p_Value";
                            ctrl_DopValuesMulti.ValueMember = "p_ID";
                            ctrl_DopValuesMulti.Width = 300;
                            ctrl_DopValuesMulti.ItemCheck += Ctrl_ValueChanged;
                        }
                        for (int i = 0; i < p_Element.p_NormValues.Length; i++)
                        {
                            var val = p_Element.p_NormValues[i];
                            ctrl_ValuesMulti.Items.Add(val);
                            if (p_Element.p_Symmetrical)
                                ctrl_DopValuesMulti.Items.Add(val);
                        }
                        ctrl_ValuesMulti.f_SetSeparator(p_Element.p_NormValues.Length);
                        if (p_Element.p_Symmetrical)
                            ctrl_DopValuesMulti.f_SetSeparator(p_Element.p_NormValues.Length);
                        for (int i = 0; i < p_Element.p_PatValues.Length; i++)
                        {
                            var val = p_Element.p_PatValues[i];
                            ctrl_ValuesMulti.Items.Add(val);
                            if (p_Element.p_Symmetrical)
                                ctrl_DopValuesMulti.Items.Add(val);
                        }
                        for (int i = 0; i < ctrl_ValuesMulti.Items.Count; i++)
                        {
                            var val = (Cl_ElementParam)ctrl_ValuesMulti.Items[i];
                            var rValue = a_RecordValue.p_ValuesCatalog.FirstOrDefault(rv => rv.p_ElementParamID == val.p_ID);
                            if (rValue != null)
                                ctrl_ValuesMulti.SetItemChecked(i, true);
                        }
                        if (p_Element.p_Symmetrical)
                        {
                            for (int i = 0; i < ctrl_DopValuesMulti.Items.Count; i++)
                            {
                                var val = (Cl_ElementParam)ctrl_DopValuesMulti.Items[i];
                                var rValue = a_RecordValue.p_ValuesDopCatalog.FirstOrDefault(rv => rv.p_ElementParamID == val.p_ID);
                                if (rValue != null)
                                    ctrl_DopValuesMulti.SetItemChecked(i, true);
                            }
                        }
                        if (m_Record.p_Version == 0 && p_Element.p_Default != null)
                        {
                            for (int i = 0; i < ctrl_ValuesMulti.Items.Count; i++)
                            {
                                var val = (Cl_ElementParam)ctrl_ValuesMulti.Items[i];
                                if (p_Element.p_Default.p_ID == val.p_ID)
                                    ctrl_ValuesMulti.SetItemChecked(i, true);
                            }
                            if (p_Element.p_Symmetrical)
                            {
                                for (int i = 0; i < ctrl_DopValuesMulti.Items.Count; i++)
                                {
                                    var val = (Cl_ElementParam)ctrl_DopValuesMulti.Items[i];
                                    if (p_Element.p_Default.p_ID == val.p_ID)
                                        ctrl_DopValuesMulti.SetItemChecked(i, true);
                                }
                            }
                        }
                        f_UpdateColor(ctrl_ValuesMulti);
                        f_UpdateColor(ctrl_DopValuesMulti);
                        if (p_Element.p_Symmetrical)
                        {
                            var cellPanel = f_GetSymmetricalPanel(ctrl_ValuesMulti, ctrl_DopValuesMulti, p_Element.p_SymmetryParamLeft, p_Element.p_SymmetryParamRight);
                            if (a_Table != null)
                                a_Table.Controls.Add(cellPanel, 1, a_RowIndex);
                            else
                                panel.Controls.Add(cellPanel);
                        }
                        else
                        {
                            if (a_Table != null)
                                a_Table.Controls.Add(ctrl_ValuesMulti, 1, a_RowIndex);
                            else
                                panel.Controls.Add(ctrl_ValuesMulti);
                        }
                    }
                    else
                    {
                        ctrl_Values = new Ctrl_SeparatorCombobox();
                        ctrl_Values.Enabled = p_Element.p_Editing;
                        ctrl_Values.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        ctrl_Values.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        ctrl_Values.FormattingEnabled = true;
                        ctrl_Values.p_SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                        ctrl_Values.Width = 300;
                        ctrl_Values.AutoCompleteCustomSource.AddRange(normValues);
                        ctrl_Values.AutoCompleteCustomSource.AddRange(patValues);
                        ctrl_Values.SelectedValueChanged += Ctrl_ValueChanged;
                        if (p_Element.p_Symmetrical)
                        {
                            ctrl_DopValues = new Ctrl_SeparatorCombobox();
                            ctrl_DopValues.Enabled = p_Element.p_Editing;
                            ctrl_DopValues.AutoCompleteMode = ctrl_Values.AutoCompleteMode;
                            ctrl_DopValues.AutoCompleteSource = ctrl_Values.AutoCompleteSource;
                            ctrl_DopValues.FormattingEnabled = ctrl_Values.FormattingEnabled;
                            ctrl_DopValues.p_SeparatorStyle = ctrl_Values.p_SeparatorStyle;
                            ctrl_DopValues.Width = ctrl_Values.Width;
                            ctrl_DopValues.AutoCompleteCustomSource.AddRange(normValues);
                            ctrl_DopValues.AutoCompleteCustomSource.AddRange(patValues);
                            ctrl_DopValues.SelectedValueChanged += Ctrl_ValueChanged;
                        }
                        foreach (var val in p_Element.p_NormValues)
                        {
                            ctrl_Values.f_AddObject(val);
                            if (p_Element.p_Symmetrical)
                                ctrl_DopValues.f_AddObject(val);
                        }
                        ctrl_Values.f_SetSeparator(p_Element.p_NormValues.Length);
                        if (p_Element.p_Symmetrical)
                            ctrl_DopValues.f_SetSeparator(p_Element.p_NormValues.Length);
                        foreach (var val in p_Element.p_PatValues)
                        {
                            ctrl_Values.f_AddObject(val);
                            if (p_Element.p_Symmetrical)
                                ctrl_DopValues.f_AddObject(val);
                        }
                        var rValue = a_RecordValue.p_ValuesCatalog.FirstOrDefault();
                        if (rValue != null)
                        {
                            ctrl_Values.SelectedItem = rValue.p_ElementParam;
                        }
                        if (p_Element.p_Symmetrical)
                        {
                            rValue = a_RecordValue.p_ValuesDopCatalog.FirstOrDefault();
                            if (rValue != null)
                            {
                                ctrl_DopValues.SelectedItem = rValue.p_ElementParam;
                            }
                        }
                        if (m_Record.p_Version == 0 && p_Element.p_Default != null)
                        {
                            ctrl_Values.SelectedItem = p_Element.p_Default;
                            if (p_Element.p_Symmetrical)
                                ctrl_DopValues.SelectedItem = p_Element.p_Default;
                        }
                        f_UpdateColor(ctrl_Values);
                        f_UpdateColor(ctrl_DopValues);
                        if (p_Element.p_Symmetrical)
                        {
                            var cellPanel = f_GetSymmetricalPanel(ctrl_Values, ctrl_DopValues, p_Element.p_SymmetryParamLeft, p_Element.p_SymmetryParamRight);
                            if (a_Table != null)
                                a_Table.Controls.Add(cellPanel, 1, a_RowIndex);
                            else
                                panel.Controls.Add(cellPanel);
                        }
                        else
                        {
                            if (a_Table != null)
                                a_Table.Controls.Add(ctrl_Values, 1, a_RowIndex);
                            else
                                panel.Controls.Add(ctrl_Values);
                        }
                    }
                }
                else
                {
                    if (p_Element.p_ElementType == Cl_Element.E_ElementsTypes.Float || p_Element.p_ElementType == Cl_Element.E_ElementsTypes.Line)
                    {
                        ctrl_Value = new TextBox();
                        ctrl_Value.Enabled = p_Element.p_Editing;
                        ctrl_Value.Width = 400;
                        ctrl_Value.Text = a_RecordValue.p_ValueUser;
                        f_UpdateColor(ctrl_Value);
                        ctrl_Value.TextChanged += Ctrl_ValueChanged;
                        if (p_Element.p_IsNumber) ctrl_Value.KeyPress += ctrl_ValidNumber_KeyPress;
                        if (p_Element.p_Symmetrical)
                        {
                            ctrl_DopValue = new TextBox();
                            ctrl_DopValue.Enabled = p_Element.p_Editing;
                            ctrl_DopValue.Width = ctrl_Value.Width;
                            ctrl_DopValue.Text = a_RecordValue.p_ValueDopUser;
                            f_UpdateColor(ctrl_DopValue);
                            ctrl_DopValue.TextChanged += Ctrl_ValueChanged;
                            if (p_Element.p_IsNumber) ctrl_DopValue.KeyPress += ctrl_ValidNumber_KeyPress;
                            var cellPanel = f_GetSymmetricalPanel(ctrl_Value, ctrl_DopValue, p_Element.p_SymmetryParamLeft, p_Element.p_SymmetryParamRight);
                            if (a_Table != null)
                                a_Table.Controls.Add(cellPanel, 1, a_RowIndex);
                            else
                                panel.Controls.Add(cellPanel);
                        }
                        else
                        {
                            if (a_Table != null)
                                a_Table.Controls.Add(ctrl_Value, 1, a_RowIndex);
                            else
                                panel.Controls.Add(ctrl_Value);
                        }
                    }
                    else
                    {
                        ctrl_ValueBox = new Ctrl_TextBoxAutoHeight() { p_MinLines = 3 };
                        ctrl_ValueBox.Enabled = p_Element.p_Editing;
                        ctrl_ValueBox.Width = 400;
                        ctrl_ValueBox.Text = a_RecordValue.p_ValueUser;
                        f_UpdateColor(ctrl_ValueBox);
                        ctrl_ValueBox.TextChanged += Ctrl_ValueChanged;
                        if (p_Element.p_IsNumber) ctrl_ValueBox.KeyPress += ctrl_ValidNumber_KeyPress;
                        if (p_Element.p_Symmetrical)
                        {
                            ctrl_DopValueBox = new Ctrl_TextBoxAutoHeight() { p_MinLines = 3 };
                            ctrl_DopValueBox.Width = ctrl_ValueBox.Width;
                            ctrl_DopValueBox.Text = a_RecordValue.p_ValueDopUser;
                            f_UpdateColor(ctrl_DopValueBox);
                            ctrl_DopValueBox.TextChanged += Ctrl_ValueChanged;
                            if (p_Element.p_IsNumber) ctrl_DopValueBox.KeyPress += ctrl_ValidNumber_KeyPress;
                            var cellPanel = f_GetSymmetricalPanel(ctrl_ValueBox, ctrl_DopValueBox, p_Element.p_SymmetryParamLeft, p_Element.p_SymmetryParamRight);
                            if (a_Table != null)
                                a_Table.Controls.Add(cellPanel, 1, a_RowIndex);
                            else
                                panel.Controls.Add(cellPanel);
                        }
                        else
                        {
                            if (a_Table != null)
                                a_Table.Controls.Add(ctrl_ValueBox, 1, a_RowIndex);
                            else
                                panel.Controls.Add(ctrl_ValueBox);
                        }
                    }
                }
                if (p_Element.p_IsPartPost)
                {
                    l = new Label() { Text = p_Element.p_PartPost };
                    l.AutoSize = true;
                    l.Margin = new Padding(0, 6, 0, 0);
                    if (a_Table != null)
                        a_Table.Controls.Add(l, 2, a_RowIndex);
                    else
                        panel.Controls.Add(l);
                }
                if (!string.IsNullOrWhiteSpace(partNorm))
                {
                    l = new Label() { Text = partNorm };
                    l.AutoSize = true;
                    l.Margin = new Padding(0, 6, 0, 0);
                    if (a_Table != null)
                        a_Table.Controls.Add(l, 3, a_RowIndex);
                    else
                        panel.Controls.Add(l);
                }
            }
            else if (p_Element.p_IsImage)
            {
                var rowPanel = new FlowLayoutPanel();
                rowPanel.WrapContents = false;
                rowPanel.AutoSize = true;
                if (a_Table != null)
                {
                    rowPanel.Dock = DockStyle.Fill;
                    rowPanel.Margin = new Padding(0);
                    a_Table.Controls.Add(rowPanel, 0, a_RowIndex);
                    a_Table.SetColumnSpan(rowPanel, a_Table.ColumnCount);
                }
                else
                    panel.Controls.Add(rowPanel);
                l = new Label() { Text = p_Element.p_Name };
                l.Margin = new Padding(0, 6, 0, 0);
                rowPanel.Controls.Add(l);
                ctrl_Image = new Ctrl_Paint();
                ctrl_Image.Size = new Size(250, 200);
                ctrl_Image.SizeMode = PictureBoxSizeMode.AutoSize;
                rowPanel.Controls.Add(ctrl_Image);

                if (p_Element.p_Image == null)
                {
                    ctrl_Image.Image = a_RecordValue.p_Image;
                    ctrl_Image.p_ReadOnly = true;
                    var bImageLoad = new Button();
                    bImageLoad.AutoSize = true;
                    bImageLoad.Text = "загрузить";
                    bImageLoad.Click += BImageLoad_Click;
                    rowPanel.Controls.Add(bImageLoad);
                }
                else
                {
                    var bReset = new Button();
                    if (a_RecordValue.p_Image != null)
                    {
                        ctrl_Image.Image = a_RecordValue.p_Image;
                        bReset.Tag = a_RecordValue.p_Image;
                    }
                    else
                    {
                        ctrl_Image.Image = p_Element.p_Image;
                        bReset.Tag = p_Element.p_Image;
                    }
                    ctrl_Image.p_ReadOnly = false;
                    bReset.AutoSize = true;
                    bReset.Text = "сбросить";
                    bReset.Click += BReset_Click;
                    rowPanel.Controls.Add(bReset);
                    var bClear = new Button();
                    bClear.AutoSize = true;
                    bClear.Text = "очистить";
                    bClear.Click += BClear_Click;
                    rowPanel.Controls.Add(bClear);
                }
            }
        }

        private bool f_ValidNumber(string[] a_Texts, bool isKeyPass = false)
        {
            bool valid = true;
            if (p_Element != null && p_Element.p_IsNumber)
            {
                if (a_Texts != null && a_Texts.Length > 0)
                {
                    foreach (string txt in a_Texts)
                    {
                        if (string.IsNullOrWhiteSpace(txt) || (isKeyPass && txt == "-")) continue;
                        if (txt.LastOrDefault() == '-') return false;
                        decimal x;
                        valid = decimal.TryParse(txt, out x);
                        if (valid)
                        {
                            int index = txt.IndexOf(",");
                            if (index >= 0)
                            {
                                if (p_Element.p_NumberRound > 0)
                                {
                                    valid = index + p_Element.p_NumberRound >= txt.Length - 1;
                                }
                                else
                                {
                                    valid = false;
                                }
                            }
                        }
                        if (!valid)
                        {
                            return false;
                        }
                    }
                }
            }
            return valid;
        }

        private void ctrl_ValidNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb != null)
            {
                if (e.KeyChar == 32)
                {
                    e.Handled = true;
                }
                else if (e.KeyChar != 8)
                {
                    int pos = tb.SelectionStart - tb.GetFirstCharIndexOfCurrentLine();
                    string txt = tb.Text;
                    if (tb.Lines.Length > 0)
                    {
                        txt = tb.Lines.ElementAt(tb.GetLineFromCharIndex(tb.SelectionStart));
                    }
                    txt = string.Format("{0}{1}{2}", txt.Substring(0, pos), e.KeyChar, txt.Substring(pos, txt.Length - pos));
                    e.Handled = !f_ValidNumber(new string[] { txt }, true);
                }
            }
            else
            {
                e.Handled = false;
            }
        }

        private void f_UpdateColor(Control a_Control)
        {
            if (p_Element.p_IsNumber && a_Control != null)
            {
                string[] texts = null;
                var chcb = a_Control as Ctrl_CheckedComboBox;
                if (chcb != null)
                    texts = chcb.Text.Split(new string[] { m_SeparatorMulti }, StringSplitOptions.RemoveEmptyEntries);
                else
                {
                    var tb = a_Control as TextBox;
                    if (tb != null)
                        texts = new string[] { tb.Text };
                    else
                    {
                        var cb = a_Control as ComboBox;
                        if (cb != null)
                            texts = new string[] { cb.Text };
                    }
                }
                decimal dVal = 0;
                bool isBeyondNorm = false;
                foreach (var text in texts)
                {
                    if (decimal.TryParse(text, out dVal))
                    {
                        if (m_Min != null && m_Min > dVal)
                            isBeyondNorm = true;
                        else if (m_Max != null && m_Max < dVal)
                            isBeyondNorm = true;
                    }
                    else
                    {
                        isBeyondNorm = true;
                    }
                    if (isBeyondNorm)
                        break;
                }
                if (isBeyondNorm)
                {
                    a_Control.ForeColor = Color.Red;
                }
                else
                {
                    a_Control.ForeColor = Color.Black;
                }
            }
        }

        private void Ctrl_ValueChanged(object sender, EventArgs e)
        {
            if (p_Element != null)
            {
                f_UpdateColor((Control)sender);
                e_ValueChanged?.Invoke(this, e);
            }
        }

        /// <summary>Получение значения элемента записи</summary>
        public Cl_RecordValue f_GetRecordElementValues(Cl_Record a_Record, bool a_IsValid = false)
        {
            if (a_Record == null || p_Element == null) return null;
            var recordValue = new Cl_RecordValue();
            recordValue.p_ElementID = p_Element.p_ID;
            recordValue.p_Element = p_Element;
            recordValue.p_RecordID = a_Record.p_ID;
            recordValue.p_Record = a_Record;
            recordValue.p_Params = new List<Cl_RecordParam>();

            if (p_Element.p_IsText)
            {
                if (p_Element.p_IsPartLocations)
                {
                    if (p_Element.p_IsPartLocationsMulti)
                    {
                        if (ctrl_PartLocationsMulti != null && ctrl_PartLocationsMulti.CheckedItems != null)
                        {
                            foreach (Cl_ElementParam ep in ctrl_PartLocationsMulti.CheckedItems)
                            {
                                recordValue.p_Params.Add(new Cl_RecordParam() { p_ElementParam = ep, p_ElementParamID = ep.p_ID, p_RecordValue = recordValue });
                            }
                        }
                        else if (a_IsValid && p_Element.p_Required)
                        {
                            MonitoringStub.Message(string.Format("Заполните поле \"Локация с множественным выбором\" элемента {0}", p_Element.p_Name));
                            return null;
                        }
                    }
                    else
                    {
                        if (ctrl_PartLocations != null && ctrl_PartLocations.SelectedItem != null)
                        {
                            var ep = (Cl_ElementParam)ctrl_PartLocations.SelectedItem;
                            recordValue.p_Params.Add(new Cl_RecordParam() { p_ElementParam = ep, p_ElementParamID = ep.p_ID, p_RecordValue = recordValue });
                        }
                        else if (a_IsValid && p_Element.p_Required)
                        {
                            MonitoringStub.Message(string.Format("Заполните поле \"Локация\" элемента {0}", p_Element.p_Name));
                            return null;
                        }
                    }
                }
                if (p_Element.p_IsTextFromCatalog)
                {
                    if (p_Element.p_IsMultiSelect)
                    {
                        if (ctrl_ValuesMulti != null && ctrl_ValuesMulti.CheckedItems != null)
                        {
                            foreach (Cl_ElementParam ep in ctrl_ValuesMulti.CheckedItems)
                            {
                                recordValue.p_Params.Add(new Cl_RecordParam() { p_ElementParam = ep, p_ElementParamID = ep.p_ID, p_RecordValue = recordValue, p_IsDop = false });
                            }
                            if (p_Element.p_Symmetrical)
                            {
                                if (ctrl_DopValuesMulti != null && ctrl_DopValuesMulti.CheckedItems != null)
                                {
                                    foreach (Cl_ElementParam ep in ctrl_DopValuesMulti.CheckedItems)
                                    {
                                        recordValue.p_Params.Add(new Cl_RecordParam() { p_ElementParam = ep, p_ElementParamID = ep.p_ID, p_RecordValue = recordValue, p_IsDop = true });
                                    }
                                }
                                else if (a_IsValid && p_Element.p_Required)
                                {
                                    MonitoringStub.Message(string.Format("Заполните поле \"Значения с множественным выбором из справочника для симметрического параметра\" элемента {0}", p_Element.p_Name));
                                    return null;
                                }
                            }
                        }
                        else if (a_IsValid && p_Element.p_Required)
                        {
                            MonitoringStub.Message(string.Format("Заполните поле \"Значения с множественным выбором из справочника\" элемента {0}", p_Element.p_Name));
                            return null;
                        }
                    }
                    else
                    {
                        if (ctrl_Values != null && ctrl_Values.SelectedItem != null)
                        {
                            var ep = (Cl_ElementParam)ctrl_Values.SelectedItem;
                            recordValue.p_Params.Add(new Cl_RecordParam() { p_ElementParam = ep, p_ElementParamID = ep.p_ID, p_RecordValue = recordValue, p_IsDop = false });
                            if (p_Element.p_Symmetrical)
                            {
                                if (ctrl_DopValues != null && ctrl_DopValues.SelectedItem != null)
                                {
                                    ep = (Cl_ElementParam)ctrl_DopValues.SelectedItem;
                                    recordValue.p_Params.Add(new Cl_RecordParam() { p_ElementParam = ep, p_ElementParamID = ep.p_ID, p_RecordValue = recordValue, p_IsDop = true });
                                }
                                else if (a_IsValid && p_Element.p_Required)
                                {
                                    MonitoringStub.Message(string.Format("Заполните поле \"Значение из справочника для симметрического параметра\" элемента {0}", p_Element.p_Name));
                                    return null;
                                }
                            }
                        }
                        else if (a_IsValid && p_Element.p_Required)
                        {
                            MonitoringStub.Message(string.Format("Заполните поле \"Значение из справочника\" элемента {0}", p_Element.p_Name));
                            return null;
                        }
                    }
                }
                else
                {
                    if (p_Element.p_ElementType == Cl_Element.E_ElementsTypes.Float || p_Element.p_ElementType == Cl_Element.E_ElementsTypes.Line)
                    {
                        if (ctrl_Value != null && !string.IsNullOrWhiteSpace(ctrl_Value.Text))
                        {
                            if (a_IsValid && p_Element.p_IsNumber)
                            {
                                if (!f_ValidNumber(new[] { ctrl_Value.Text }))
                                {
                                    MonitoringStub.Message($"Значения поля \"Значение\" не являются числовыми или не соответствуют точности числа элемента {p_Element.p_Name}");
                                    return null;
                                }
                            }
                            recordValue.p_ValueUser = ctrl_Value.Text;
                            if (p_Element.p_Symmetrical)
                            {
                                if (ctrl_DopValue != null && !string.IsNullOrWhiteSpace(ctrl_DopValue.Text))
                                {
                                    if (a_IsValid && p_Element.p_IsNumber)
                                    {
                                        if (!f_ValidNumber(new[] { ctrl_DopValue.Text }))
                                        {
                                            MonitoringStub.Message($"Значения поля \"Значение для симметрического параметра\" не являются числовыми или не соответствуют точности числа элемента {p_Element.p_Name}");
                                            return null;
                                        }
                                    }
                                    recordValue.p_ValueDopUser = ctrl_DopValue.Text;
                                }
                                else if (a_IsValid && p_Element.p_Required)
                                {
                                    MonitoringStub.Message(string.Format("Заполните поле \"Значение для симметрического параметра\" элемента {0}", p_Element.p_Name));
                                    return null;
                                }
                            }
                        }
                        else if (a_IsValid && p_Element.p_Required)
                        {
                            MonitoringStub.Message(string.Format("Заполните поле \"Значение\" элемента {0}", p_Element.p_Name));
                            return null;
                        }
                    }
                    else
                    {
                        if (ctrl_ValueBox != null && !string.IsNullOrWhiteSpace(ctrl_ValueBox.Text))
                        {
                            recordValue.p_ValueUser = ctrl_ValueBox.Text;
                            if (p_Element.p_Symmetrical)
                            {
                                if (ctrl_DopValueBox != null && !string.IsNullOrWhiteSpace(ctrl_DopValueBox.Text))
                                {
                                    recordValue.p_ValueDopUser = ctrl_DopValueBox.Text;
                                }
                                else if (a_IsValid && p_Element.p_Required)
                                {
                                    MonitoringStub.Message(string.Format("Заполните поле \"Значение для симметрического параметра\" элемента {0}", p_Element.p_Name));
                                    return null;
                                }
                            }
                        }
                        else if (a_IsValid && p_Element.p_Required)
                        {
                            MonitoringStub.Message(string.Format("Заполните поле \"Значение\" элемента {0}", p_Element.p_Name));
                            return null;
                        }
                    }
                }
            }
            else if (p_Element.p_IsImage)
            {
                if (ctrl_Image != null)
                {
                    recordValue.p_Image = ctrl_Image.Image;
                }
            }

            return recordValue;
        }

        /// <summary>Установка значения элемента записи</summary>
        public void f_SetValue(Cl_Record a_Record, Cl_ElementParam a_Value)
        {
            if (a_Record == null || p_Element == null || a_Value == null) return;
            if (p_Element.p_IsText)
            {
                if (p_Element.p_IsTextFromCatalog)
                {
                    if (!p_Element.p_IsMultiSelect)
                    {
                        if (ctrl_Values != null)
                        {
                            ctrl_Values.SelectedItem = a_Value;
                            if (p_Element.p_Symmetrical)
                            {
                                if (ctrl_DopValues != null)
                                {
                                    ctrl_DopValues.SelectedItem = a_Value;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>Установка значения элемента записи</summary>
        public void f_SetValue(Cl_Record a_Record, decimal a_Value)
        {
            f_SetValue(a_Record, a_Value.ToString());
        }

        /// <summary>Установка значения элемента записи</summary>
        public void f_SetValue(Cl_Record a_Record, string a_Value)
        {
            if (a_Record == null || p_Element == null || a_Value == null) return;
            if (p_Element.p_IsText)
            {
                if (!p_Element.p_IsTextFromCatalog)
                {
                    if (p_Element.p_ElementType == Cl_Element.E_ElementsTypes.Float || p_Element.p_ElementType == Cl_Element.E_ElementsTypes.Line)
                    {
                        if (ctrl_Value != null)
                        {
                            ctrl_Value.Text = a_Value;
                            if (p_Element.p_Symmetrical)
                            {
                                if (ctrl_DopValue != null)
                                {
                                    ctrl_DopValue.Text = a_Value;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (ctrl_ValueBox != null)
                        {
                            ctrl_ValueBox.Text = a_Value;
                            if (p_Element.p_Symmetrical)
                            {
                                if (ctrl_DopValueBox != null)
                                {
                                    ctrl_DopValueBox.Text = a_Value;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>Установка значения элемента записи</summary>
        public void f_SetValue(Cl_Record a_Record, Image a_Value)
        {
            if (a_Record == null || p_Element == null || a_Value == null) return;
            else if (p_Element.p_IsImage)
            {
                if (ctrl_Image != null)
                {
                    ctrl_Image.Image = a_Value;
                }
            }
        }

        private void BImageLoad_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Image Files |*.bmp; *.gif; *.jpg; *.jpeg; *.png";
            openFile.FilterIndex = 1;
            if (openFile.ShowDialog() != DialogResult.OK)
                return;
            Image result = null;
            try
            {
                result = Image.FromFile(openFile.FileName);
            }
            catch (Exception ex)
            {
                MonitoringStub.Problem("Problem_Record", "Выбранный файл не является изображением", ex, null, null);
                return;
            }
            ctrl_Image.Image = result;
        }

        private void BReset_Click(object sender, System.EventArgs e)
        {
            var img = ((Button)sender).Tag as Image;
            ctrl_Image.Image = img;
        }

        private void BClear_Click(object sender, System.EventArgs e)
        {
            if (p_Element.p_Image != null)
            {
                ctrl_Image.Image = p_Element.p_Image;
            }
        }
    }
}
