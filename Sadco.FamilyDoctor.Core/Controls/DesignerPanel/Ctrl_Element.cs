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

        /// <summary>Значение элемента</summary>
        public string p_Value { get; set; }

        /// <summary>Возвращает является ли вкладкой</summary>
        public bool f_IsTab()
        {
            return p_Element != null ? p_Element.p_IsTab : false;
        }

        /// <summary>Возвращает является ли заголовком</summary>
        public bool f_IsHeader()
        {
            return p_Element != null ? p_Element.p_IsHeader : false;
        }

        /// <summary>Возвращает уровень заголовка</summary>
        public int f_GetHeaderLevel()
        {
            return p_Element?.p_IsHeader == true ? int.Parse(p_Element.p_Name.Substring(10)) : 0;
        }

        /// <summary>ID элемента</summary>
        public int p_ID {
            get {
                if (p_Element != null)
                    return p_Element.p_ID;
                return -1;
            }
        }

        public int p_ElementID {
            get {
                if (p_Element != null)
                    return p_Element.p_ElementID;
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
                string text = "";
                if (p_Element.p_IsHeader)
                {
                    text = $"h{p_Element.p_HeaderLevel}: {p_Value}";
                }
                else
                {
                    text = string.Format("{0} ({1})", p_Name, p_Element.p_Version == 0 ? "Черновик" : "v" + p_Element.p_Version);
                }
                TextRenderer.DrawText(a_Graphics, text, a_Font, textBounds, a_ForeColor, TextFormatFlags.ExpandTabs | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter);
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
            cellPanel.Dock = DockStyle.Top;
            cellPanel.AutoSize = true;
            cellPanel.Margin = new Padding(0);
            cellPanel.ColumnCount = 4;
            cellPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            cellPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            var title = new Label();
            title.Font = new Font(title.Font.FontFamily, title.Font.Size, p_Element.p_Required ? FontStyle.Bold : FontStyle.Italic);
            title.Text = a_LeftText;
            title.RightToLeft = RightToLeft.Yes;
            title.AutoSize = true;
            title.Margin = new Padding(0, 6, 0, 0);
            cellPanel.Controls.Add(title, 0, 0);
            a_LeftControl.Dock = DockStyle.Top;
            cellPanel.Controls.Add(a_LeftControl, 1, 0);

            title = new Label();
            title.Font = new Font(title.Font.FontFamily, title.Font.Size, p_Element.p_Required ? FontStyle.Bold : FontStyle.Italic);
            title.Text = a_RightText;
            title.RightToLeft = RightToLeft.Yes;
            title.AutoSize = true;
            title.Margin = new Padding(0, 6, 0, 0);
            cellPanel.Controls.Add(title, 0, 1);
            a_RightControl.Dock = DockStyle.Top;
            cellPanel.Controls.Add(a_RightControl, 1, 1);

            return cellPanel;
        }

        private void f_ResizeImage(Image img)
        {
            if (img == null) return;
            var panel = (Ctrl_BorderedPanel)ctrl_Image.Parent;
            if ((img.Height > panel.Height | img.Width > panel.Width))
            {
                int height = panel.Size.Height - panel.p_BorderWidth * 2 + 1;
                int width = panel.Size.Width - panel.p_BorderWidth * 2 + 1;

                if (img.Height > img.Width)
                {
                    var _width = (height * img.Width) / (double)img.Height;
                    if (_width < panel.Width)
                        width = (int)_width;
                    else
                    {
                        height = (int)((width * img.Height) / (double)img.Width);
                    }
                }
                else
                {
                    var _height = (width * img.Height) / (double)img.Width;
                    if (_height < panel.Height)
                        height = (int)_height;
                    else
                    {
                        width = (int)((height * img.Width) / (double)img.Height);
                    }
                }
                ctrl_Image.Size = new Size(width, height);
                ctrl_Image.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                ctrl_Image.Size = img.Size;
                ctrl_Image.SizeMode = PictureBoxSizeMode.Normal;
            }
        }

        private Control f_UpdateControlValue(Control a_Control)
        {
            a_Control.Font = new Font(a_Control.Font.FontFamily, a_Control.Font.Size, FontStyle.Bold);
            a_Control.GotFocus += Ctrl_GotFocus;
            a_Control.LostFocus += Ctrl_LostFocus;
            a_Control.KeyUp += ControlValue_KeyUp;
            a_Control.Dock = DockStyle.Top;
            a_Control.Enabled = p_Element.p_Editing;
            if (!a_Control.Enabled)
                a_Control.BackColor = Cl_App.f_GetRecordSetting().p_RecordReadOnlyBackColor;
            if (a_Control is ComboBox && !(a_Control is Ctrl_CheckedComboBox))
            {
                {
                    var comboBox = (ComboBox)a_Control;
                    comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
                    if (!p_Element.p_IsChangeNotNormValues)
                    {
                        comboBox.AutoCompleteMode = AutoCompleteMode.None;
                        comboBox.AutoCompleteSource = AutoCompleteSource.None;
                        comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                    }
                    else
                    {
                        ctrl_Values.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        ctrl_Values.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    }
                }
            }
            Panel panelBorder = null;
            if (p_Element.p_VisiblePatient)
            {
                panelBorder = new Panel();
                panelBorder.Dock = DockStyle.Top;
                panelBorder.BackColor = Cl_App.f_GetRecordSetting().p_RecordPatientControlBorderColor;
                panelBorder.AutoSize = true;
                panelBorder.Padding = new Padding(Cl_App.f_GetRecordSetting().p_RecordPatientControlBorderWidth);
                panelBorder.Controls.Add(a_Control);
            }
            var ctrl = panelBorder != null ? panelBorder : a_Control;
            return ctrl;
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SendKeys.Send("{TAB}");
        }

        private void ControlValue_KeyUp(object sender, KeyEventArgs e)
        {
            var control = (Control)sender;
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        /// <summary>Инициализация пользовательских контролов для записи</summary>
        public void f_SetRecordElementValues(Cl_RecordValue a_RecordValue, TableLayoutPanel a_Table, int a_RowIndex)
        {
            if (a_RecordValue == null || a_RecordValue.p_Record == null || p_Element == null) return;
            Visible = p_Element.p_Visible && Cl_RecordsFacade.f_GetInstance().f_GetElementVisible(a_RecordValue.p_Record, p_Element.p_VisibilityFormula);

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

            Label l = null;

            if (p_Element.p_IsHeader)
            {
                l = new Label() { Text = p_Value };
                l.TextAlign = ContentAlignment.MiddleCenter;
                l.Font = new Font(l.Font.FontFamily, Cl_App.f_GetRecordSetting().p_SizeH1 - 2 * (p_Element.p_HeaderLevel - 1));
                l.Dock = DockStyle.Top;
                l.Height = l.Font.Height + 6;
                if (a_Table != null)
                {
                    a_Table.Controls.Add(l, 0, a_RowIndex);
                    a_Table.SetColumnSpan(l, a_Table.ColumnCount);
                }
                else
                {
                    Controls.Add(l);
                }
                return;
            }
            if (p_Element.p_IsText)
            {
                bool isLocation = p_Element.p_IsPartLocations && p_Element.p_PartLocations != null && p_Element.p_PartLocations.Length > 0 && a_Table == null;
                bool isPartPost = p_Element.p_IsPartPost && !string.IsNullOrWhiteSpace(p_Element.p_PartPost);
                byte age = a_RecordValue.p_Record.p_MedicalCard.f_GetPatientAgeByYear(a_RecordValue.p_Record.p_DateCreate);
                var partNorm = p_Element.f_GetPartNormValue(a_RecordValue.p_Record.p_MedicalCard.p_PatientSex, age, out m_Min, out m_Max);
                bool isNorm = !string.IsNullOrWhiteSpace(partNorm);
                TableLayoutPanel tablePanel = a_Table;
                if (tablePanel == null)
                {
                    tablePanel = new TableLayoutPanel();
                    int colCount = 5;
                    if (!isLocation)
                        colCount--;
                    if (!isPartPost)
                        colCount--;
                    if (!isNorm)
                        colCount--;

                    tablePanel.ColumnCount = colCount;
                    tablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                    if (isLocation)
                        tablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                    tablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
                    if (isPartPost)
                        tablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40));
                    if (isNorm)
                        tablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                    tablePanel.RowCount = 0;

                    tablePanel.Dock = DockStyle.Top;
                    tablePanel.Height = 20;
                    tablePanel.AutoSize = true;
                    Controls.Add(tablePanel);
                }
                if (p_Element.p_IsPartPre)
                {
                    l = new Label() { Text = p_Element.p_PartPre };
                    l.Font = new Font(l.Font.FontFamily, l.Font.Size, p_Element.p_Required ? FontStyle.Bold : FontStyle.Italic);
                    l.AutoSize = true;
                    l.Margin = new Padding(0, 6, 0, 0);
                    tablePanel.Controls.Add(l, 0, a_RowIndex);
                }
                if (isLocation)
                {
                    if (p_Element.p_IsPartLocationsMulti)
                    {
                        ctrl_PartLocationsMulti = new Ctrl_CheckedComboBox();
                        f_UpdateControlValue(ctrl_PartLocationsMulti);
                        ctrl_PartLocationsMulti.DisplayMember = "p_Value";
                        ctrl_PartLocationsMulti.ValueMember = "p_ID";
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
                        tablePanel.Controls.Add(ctrl_PartLocationsMulti, 1, a_RowIndex);
                    }
                    else
                    {
                        ctrl_PartLocations = new ComboBox();
                        f_UpdateControlValue(ctrl_PartLocations);
                        ctrl_PartLocations.AutoCompleteCustomSource.AddRange(p_Element.p_PartLocations.Select(e => e.p_Value).ToArray());
                        foreach (var pLoc in p_Element.p_PartLocations)
                        {
                            ctrl_PartLocations.Items.Add(pLoc);
                        }
                        ctrl_PartLocations.DisplayMember = "p_Value";
                        ctrl_PartLocations.ValueMember = "p_ID";
                        var lParam = a_RecordValue.p_PartLocations.FirstOrDefault();
                        if (lParam != null)
                            ctrl_PartLocations.SelectedItem = p_Element.p_PartLocations.FirstOrDefault(pl => pl.p_ID == lParam.p_ElementParamID);
                        tablePanel.Controls.Add(ctrl_PartLocations, 1, a_RowIndex);
                    }
                }
                if (p_Element.p_IsTextFromCatalog)
                {
                    var normValues = p_Element.p_NormValues.Select(e => e.p_Value).ToArray();
                    var patValues = p_Element.p_PatValues.Select(e => e.p_Value).ToArray();
                    if (p_Element.p_IsMultiSelect)
                    {
                        ctrl_ValuesMulti = new Ctrl_CheckedComboBox();
                        var ctrlVal = f_UpdateControlValue(ctrl_ValuesMulti);
                        Control ctrlDop = null;
                        ctrl_ValuesMulti.p_SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                        ctrl_ValuesMulti.ValueSeparator = m_SeparatorMulti;
                        ctrl_ValuesMulti.DisplayMember = "p_Value";
                        ctrl_ValuesMulti.ValueMember = "p_ID";
                        ctrl_ValuesMulti.ItemCheck += Ctrl_ValueChanged;
                        if (p_Element.p_Symmetrical)
                        {
                            ctrl_DopValuesMulti = new Ctrl_CheckedComboBox();
                            ctrlDop = f_UpdateControlValue(ctrl_DopValuesMulti);
                            ctrl_DopValuesMulti.p_SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                            ctrl_DopValuesMulti.ValueSeparator = m_SeparatorMulti;
                            ctrl_DopValuesMulti.DisplayMember = "p_Value";
                            ctrl_DopValuesMulti.ValueMember = "p_ID";
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
                            var cellPanel = f_GetSymmetricalPanel(ctrlVal, ctrlDop, p_Element.p_SymmetryParamLeft, p_Element.p_SymmetryParamRight);
                            tablePanel.Controls.Add(cellPanel, isLocation ? 2 : 1, a_RowIndex);
                        }
                        else
                            tablePanel.Controls.Add(ctrlVal, isLocation ? 2 : 1, a_RowIndex);
                    }
                    else
                    {
                        ctrl_Values = new Ctrl_SeparatorCombobox();
                        var ctrlVal = f_UpdateControlValue(ctrl_Values);
                        Control ctrlDop = null;
                        ctrl_Values.FormattingEnabled = true;
                        ctrl_Values.p_SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                        ctrl_Values.AutoCompleteCustomSource.AddRange(normValues);
                        ctrl_Values.AutoCompleteCustomSource.AddRange(patValues);
                        ctrl_Values.SelectedValueChanged += Ctrl_ValueChanged;
                        if (p_Element.p_Symmetrical)
                        {
                            ctrl_DopValues = new Ctrl_SeparatorCombobox();
                            ctrlDop = f_UpdateControlValue(ctrl_DopValues);
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
                        else
                        {
                            ctrl_Values.Text = a_RecordValue.p_ValueUser;
                        }
                        if (p_Element.p_Symmetrical)
                        {
                            rValue = a_RecordValue.p_ValuesDopCatalog.FirstOrDefault();
                            if (rValue != null)
                            {
                                ctrl_DopValues.SelectedItem = rValue.p_ElementParam;
                            }
                            else
                            {
                                ctrl_DopValues.Text = a_RecordValue.p_ValueDopUser;
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
                            var cellPanel = f_GetSymmetricalPanel(ctrlVal, ctrlDop, p_Element.p_SymmetryParamLeft, p_Element.p_SymmetryParamRight);
                            tablePanel.Controls.Add(cellPanel, isLocation ? 2 : 1, a_RowIndex);
                        }
                        else
                            tablePanel.Controls.Add(ctrlVal, isLocation ? 2 : 1, a_RowIndex);
                    }
                }
                else
                {
                    if (p_Element.p_ElementType == Cl_Element.E_ElementsTypes.Float || p_Element.p_ElementType == Cl_Element.E_ElementsTypes.Line)
                    {
                        ctrl_Value = new TextBox();
                        var ctrlVal = f_UpdateControlValue(ctrl_Value);
                        ctrl_Value.Text = a_RecordValue.p_ValueUser;
                        f_UpdateColor(ctrl_Value);
                        ctrl_Value.TextChanged += Ctrl_ValueChanged;
                        if (p_Element.p_IsNumber) ctrl_Value.KeyPress += ctrl_ValidNumber_KeyPress;
                        if (p_Element.p_Symmetrical)
                        {
                            ctrl_DopValue = new TextBox();
                            var ctrlDop = f_UpdateControlValue(ctrl_DopValue);
                            ctrl_DopValue.Width = ctrl_Value.Width;
                            ctrl_DopValue.Text = a_RecordValue.p_ValueDopUser;
                            f_UpdateColor(ctrl_DopValue);
                            ctrl_DopValue.TextChanged += Ctrl_ValueChanged;
                            if (p_Element.p_IsNumber) ctrl_DopValue.KeyPress += ctrl_ValidNumber_KeyPress;
                            var cellPanel = f_GetSymmetricalPanel(ctrlVal, ctrlDop, p_Element.p_SymmetryParamLeft, p_Element.p_SymmetryParamRight);
                            tablePanel.Controls.Add(cellPanel, isLocation ? 2 : 1, a_RowIndex);
                        }
                        else
                        {
                            tablePanel.Controls.Add(ctrlVal, isLocation ? 2 : 1, a_RowIndex);
                        }
                    }
                    else
                    {
                        ctrl_ValueBox = new Ctrl_TextBoxAutoHeight() { p_MinLines = 3 };
                        var ctrlVal = f_UpdateControlValue(ctrl_ValueBox);
                        ctrl_ValueBox.Text = a_RecordValue.p_ValueUser;
                        f_UpdateColor(ctrl_ValueBox);
                        ctrl_ValueBox.TextChanged += Ctrl_ValueChanged;
                        if (p_Element.p_IsNumber) ctrl_ValueBox.KeyPress += ctrl_ValidNumber_KeyPress;
                        if (p_Element.p_Symmetrical)
                        {
                            ctrl_DopValueBox = new Ctrl_TextBoxAutoHeight() { p_MinLines = 3 };
                            var ctrlDop = f_UpdateControlValue(ctrl_DopValueBox);
                            ctrl_DopValueBox.Width = ctrl_ValueBox.Width;
                            ctrl_DopValueBox.Text = a_RecordValue.p_ValueDopUser;
                            f_UpdateColor(ctrl_DopValueBox);
                            ctrl_DopValueBox.TextChanged += Ctrl_ValueChanged;
                            if (p_Element.p_IsNumber) ctrl_DopValueBox.KeyPress += ctrl_ValidNumber_KeyPress;
                            var cellPanel = f_GetSymmetricalPanel(ctrlVal, ctrlDop, p_Element.p_SymmetryParamLeft, p_Element.p_SymmetryParamRight);
                            tablePanel.Controls.Add(cellPanel, isLocation ? 2 : 1, a_RowIndex);
                        }
                        else
                        {
                            tablePanel.Controls.Add(ctrlVal, isLocation ? 2 : 1, a_RowIndex);
                        }
                    }
                }
                if (isPartPost)
                {
                    l = new Label() { Text = p_Element.p_PartPost };
                    l.Font = new Font(l.Font.FontFamily, l.Font.Size, p_Element.p_Required ? FontStyle.Bold : FontStyle.Italic);
                    l.AutoSize = true;
                    l.Margin = new Padding(0, 6, 0, 0);
                    tablePanel.Controls.Add(l, isLocation ? 3 : 2, a_RowIndex);
                }
                if (isNorm)
                {
                    l = new Label() { Text = partNorm };
                    l.Font = new Font(l.Font.FontFamily, l.Font.Size, p_Element.p_Required ? FontStyle.Bold : FontStyle.Italic);
                    l.AutoSize = true;
                    l.Margin = new Padding(0, 6, 0, 0);
                    tablePanel.Controls.Add(l, isLocation ? 4 : 3, a_RowIndex);
                }
            }
            else if (p_Element.p_IsImage)
            {
                var elImgPanel = new Panel();
                elImgPanel.Dock = DockStyle.Top;
                elImgPanel.AutoSize = true;
                if (a_Table != null)
                {
                    elImgPanel.Dock = DockStyle.Fill;
                    elImgPanel.Margin = new Padding(0);
                    a_Table.Controls.Add(elImgPanel, 0, a_RowIndex);
                    a_Table.SetColumnSpan(elImgPanel, a_Table.ColumnCount);
                }
                else
                    Controls.Add(elImgPanel);
                var imgPanel = new Ctrl_BorderedPanel();
                imgPanel.p_BorderWidth = 1;
                imgPanel.p_BorderColor = Color.Black;
                imgPanel.Height = 250;
                imgPanel.Dock = DockStyle.Top;
                imgPanel.Resize += ImgPanel_Resize;

                ctrl_Image = new Ctrl_Paint();
                ctrl_Image.Left = imgPanel.p_BorderWidth;
                ctrl_Image.Top = imgPanel.p_BorderWidth;
                imgPanel.Controls.Add(ctrl_Image);

                var buttonsPanel = new FlowLayoutPanel();
                buttonsPanel.Dock = DockStyle.Top;
                buttonsPanel.WrapContents = false;
                buttonsPanel.AutoSize = true;
                buttonsPanel.FlowDirection = FlowDirection.RightToLeft;
                elImgPanel.Controls.Add(buttonsPanel);
                elImgPanel.Controls.Add(imgPanel);

                if (p_Element.p_Image == null)
                {
                    f_ResizeImage(a_RecordValue.p_Image);
                    ctrl_Image.Image = a_RecordValue.p_Image;
                    ctrl_Image.p_ReadOnly = true;
                    var bImageLoad = new Button();
                    bImageLoad.AutoSize = true;
                    bImageLoad.Text = "загрузить";
                    bImageLoad.Click += BImageLoad_Click;
                    buttonsPanel.Controls.Add(bImageLoad);
                }
                else
                {
                    var bReset = new Button();
                    if (a_RecordValue.p_Image != null)
                    {
                        f_ResizeImage(a_RecordValue.p_Image);
                        ctrl_Image.Image = a_RecordValue.p_Image;
                        bReset.Tag = a_RecordValue.p_Image;
                    }
                    else
                    {
                        f_ResizeImage(p_Element.p_Image);
                        ctrl_Image.Image = p_Element.p_Image;
                        bReset.Tag = p_Element.p_Image;
                    }
                    ctrl_Image.p_ReadOnly = false;
                    bReset.AutoSize = true;
                    bReset.Text = "сбросить";
                    bReset.Click += BReset_Click;
                    buttonsPanel.Controls.Add(bReset);
                    var bClear = new Button();
                    bClear.AutoSize = true;
                    bClear.Text = "очистить";
                    bClear.Click += BClear_Click;
                    buttonsPanel.Controls.Add(bClear);
                }
            }
        }

        private void Ctrl_GotFocus(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = Cl_App.f_GetRecordSetting().p_RecordCurrentEditColor;
        }

        private void Ctrl_LostFocus(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = Color.White;
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
                    a_Control.Font = new Font(a_Control.Font.FontFamily, a_Control.Font.Size, FontStyle.Bold);
                    a_Control.ForeColor = Cl_App.f_GetRecordSetting().p_RecordOutRangeColor;
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
                        if (ctrl_Values != null && (ctrl_Values.SelectedItem != null || (p_Element.p_IsChangeNotNormValues && !string.IsNullOrWhiteSpace(ctrl_Values.Text))))
                        {
                            if (ctrl_Values.SelectedItem != null)
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
                            else
                            {
                                recordValue.p_ValueUser = ctrl_Values.Text;
                                if (p_Element.p_Symmetrical)
                                {
                                    if (ctrl_DopValues != null && !string.IsNullOrWhiteSpace(ctrl_DopValues.Text))
                                    {
                                        recordValue.p_ValueDopUser = ctrl_DopValues.Text;
                                    }
                                    else if (a_IsValid && p_Element.p_Required)
                                    {
                                        MonitoringStub.Message(string.Format("Заполните поле \"Значение для симметрического параметра\" элемента {0}", p_Element.p_Name));
                                        return null;
                                    }
                                }
                            }
                        }
                        else if (a_IsValid && p_Element.p_Required)
                        {
                            MonitoringStub.Message(string.Format("Заполните поле \"Значение\" элемента {0}", p_Element.p_Name));
                            return null;
                        }
                        else
                        {
                            recordValue.p_ValueUser = ctrl_Values != null ? ctrl_Values.Text : "";
                            recordValue.p_ValueDopUser = ctrl_DopValues != null ? ctrl_DopValues.Text : "";
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

        private void ImgPanel_Resize(object sender, EventArgs e)
        {
            f_ResizeImage(ctrl_Image.Image);
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
            f_ResizeImage(result);
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
