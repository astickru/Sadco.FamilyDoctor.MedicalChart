using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Controls;
using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.MedicalChart.Forms.MegaTemplate;
using static Sadco.FamilyDoctor.Core.Entities.Cl_Element;

namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
    public partial class UC_EditorTextual : UserControl, I_EditPanel
    {
        public Cl_Element p_EditingElement { get; private set; }

        public UC_EditorTextual()
        {
            InitializeComponent();
            ctrl_Default.p_SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            ctrl_ControlType.f_SetEnum(typeof(E_TextTypes));
        }

        private bool m_ReadOnly = true;
        public bool p_ReadOnly {
            get { return m_ReadOnly; }
            set {
                m_ReadOnly = value;
                Enabled = m_ReadOnly;
            }
        }

        public object f_ConfirmChanges()
        {
            if (!f_ValidNumber(ctrl_NormValues.Lines))
            {
                MessageBox.Show("Нормальные значения не являются числовыми или не соответствуют точности числа");
                return null;
            }
            if (!f_ValidNumber(ctrl_PatValues.Lines))
            {
                MessageBox.Show("Патологические значения не являются числовыми или не соответствуют точности числа");
                return null;
            }
            if (!f_ValidNumber(new string[] { ctrl_Default.Text }))
            {
                MessageBox.Show("Значение по-умолчанию не является числовым или не соответствует точности числа");
                return null;
            }

            Cl_Element el = null;
            if (p_EditingElement.p_Version == 0)
            {
                el = p_EditingElement;
                el.p_Version = 1;
            }
            else
            {
                el = new Cl_Element();
                el.p_Version = p_EditingElement.p_Version + 1;
                el.p_ParentGroupID = p_EditingElement.p_ParentGroupID;
                el.p_ParentGroup = p_EditingElement.p_ParentGroup;
                Cl_App.m_DataContext.p_Elements.Add(el);
            }
            el.p_ElementType = (E_ElementsTypes)ctrl_ControlType.f_GetSelectedItem();
            el.p_ElementID = p_EditingElement.p_ElementID;
            el.p_Name = ctrl_Name.Text;
            el.p_Tag = ctrlTag.Text;
            el.p_Help = ctrl_Hint.Text;

            el.p_IsPartPre = ctrl_IsPartPre.Checked;
            if (el.p_IsPartPre)
                el.p_PartPre = ctrl_PartPreValue.Text;
            el.p_IsPartPost = ctrl_IsPartPost.Checked;
            if (el.p_IsPartPost)
                el.p_PartPost = ctrl_PartPostValue.Text;
            el.p_IsPartLocations = ctrl_IsPartLocations.Checked;
            if (el.p_IsPartLocations)
            {
                el.p_IsPartLocationsMulti = ctrl_IsPartLocationsMulti.Checked;
            }
            el.p_IsPartNorm = ctrl_IsPartNorm.Checked;
            if (el.p_IsPartNorm)
                el.p_PartNorm = ctrl_PartNormValue.Text;
            el.p_IsPartNormRange = ctrl_IsPartNormRange.Checked;
            if (el.p_IsPartNormRange)
                el.p_PartNormRange = ctrl_PartNormRangeValue.Text;

            el.p_IsChangeNotNormValues = ctrl_IsChangeNotNormValues.Checked;
            el.p_Visible = ctrl_IsVisible.Checked;
            el.p_VisiblePatient = ctrl_IsVisiblePatient.Checked;
            el.p_Required = ctrl_IsRequiredFIeld.Checked;
            el.p_Editing = ctrl_IsEditing.Checked;
            el.p_IsMultiSelect = ctrl_IsMultiSelect.Checked;
            el.p_Symmetrical = ctrl_IsSymmentry.Checked;
            el.p_SymmetryParamLeft = ctrl_Symmetry1.Text;
            el.p_SymmetryParamRight = ctrl_Symmetry2.Text;
            el.p_IsNumber = ctrl_IsNumber.Checked;
            el.p_NumberRound = Convert.ToByte(ctrl_NumberRound.Value);
            el.p_NumberFormula = ctrl_NumberFormula.Text;
            el.p_Default = ctrl_Default.Text;
            el.p_VisibilityFormula = ctrl_VisibilityFormula.Text;
            el.p_Comment = ctrl_Note.Text;

            Cl_App.m_DataContext.SaveChanges();
            el.p_ParamsValues.Clear();
            if (el.p_IsPartLocations)
            {
                el.f_AddValues(Cl_ElementsParams.E_TypeParam.Location, ctrl_PartLocationsValue.Lines);
            }
            el.f_AddValues(Cl_ElementsParams.E_TypeParam.NormValues, ctrl_NormValues.Lines);
            el.f_AddValues(Cl_ElementsParams.E_TypeParam.PatValues, ctrl_PatValues.Lines);
            Cl_App.m_DataContext.SaveChanges();
            f_SetElement(el);
            return el;
        }

        public void f_SetElement(Cl_Element a_Element)
        {
            if (a_Element == null || !a_Element.f_IsText()) return;
            p_EditingElement = a_Element;
            if (p_EditingElement.p_Version == 0)
                ctrl_Version.Text = "Черновик";
            else
                ctrl_Version.Text = p_EditingElement.p_Version.ToString();
            ctrl_ControlType.f_SetSelectedItem(p_EditingElement.p_ElementType);

            ctrl_Name.Text = p_EditingElement.p_Name;
            ctrlTag.Text = p_EditingElement.p_Tag;
            ctrl_Hint.Text = p_EditingElement.p_Help;

            ctrl_IsPartPre.Checked = ctrl_PartPreValue.Enabled = p_EditingElement.p_IsPartPre;
            ctrl_PartPreValue.Text = p_EditingElement.p_PartPre;
            ctrl_IsPartPost.Checked = ctrl_PartPostValue.Enabled = p_EditingElement.p_IsPartPost;
            ctrl_PartPostValue.Text = p_EditingElement.p_PartPost;
            ctrl_IsPartLocations.Checked = ctrl_PartLocationsValue.Enabled = ctrl_IsPartLocationsMulti.Enabled = p_EditingElement.p_IsPartLocations;
            ctrl_IsPartLocationsMulti.Checked = p_EditingElement.p_IsPartLocationsMulti;
            ctrl_IsPartNorm.Checked = ctrl_PartNormValue.Enabled = p_EditingElement.p_IsPartNorm;
            ctrl_PartNormValue.Text = p_EditingElement.p_PartNorm;
            ctrl_IsPartNormRange.Checked = ctrl_PartNormRangeValue.Enabled = p_EditingElement.p_IsPartNormRange;
            ctrl_PartNormRangeValue.Text = p_EditingElement.p_PartNormRange;
            ctrl_IsChangeNotNormValues.Checked = p_EditingElement.p_IsChangeNotNormValues;

            ctrl_IsVisible.Checked = p_EditingElement.p_Visible;
            ctrl_IsVisiblePatient.Checked = p_EditingElement.p_VisiblePatient;
            ctrl_IsRequiredFIeld.Checked = p_EditingElement.p_Required;
            ctrl_IsEditing.Checked = p_EditingElement.p_Editing;
            ctrl_IsMultiSelect.Checked = p_EditingElement.p_IsMultiSelect;
            ctrl_SymmetryVals.Visible = ctrl_IsSymmentry.Checked = p_EditingElement.p_Symmetrical;
            ctrl_Symmetry1.Text = p_EditingElement.p_SymmetryParamLeft;
            ctrl_Symmetry2.Text = p_EditingElement.p_SymmetryParamRight;
            ctrl_NumberParams.Visible = ctrl_IsNumber.Checked = p_EditingElement.p_IsNumber;
            ctrl_NumberRound.Value = p_EditingElement.p_NumberRound;
            ctrl_NumberFormula.Text = p_EditingElement.p_NumberFormula;
            ctrl_VisibilityFormula.Text = p_EditingElement.p_VisibilityFormula;
            ctrl_Default.Text = p_EditingElement.p_Default;
            ctrl_Note.Text = p_EditingElement.p_Comment;

            ctrl_PartLocationsValue.Text = string.Join("\n", p_EditingElement.p_PartLocations.ToList());
            ctrl_NormValues.Text = string.Join("\n", p_EditingElement.p_NormValues.ToList());
            ctrl_PatValues.Text = string.Join("\n", p_EditingElement.p_PatValues.ToList());
        }
        #region CheckedChanged
        private void ctrl_IsSymmentry_CheckedChanged(object sender, EventArgs e)
        {
            ctrl_SymmetryVals.Visible = ctrl_IsSymmentry.Checked;
        }

        private void ctrl_CB_IsNumber_CheckedChanged(object sender, EventArgs e)
        {
            ctrl_NumberParams.Visible = ctrl_IsNumber.Checked;
        }

        private void ctrl_IsPartPre_CheckedChanged(object sender, EventArgs e)
        {
            ctrl_PartPreValue.Enabled = ctrl_IsPartPre.Checked;
        }

        private void ctrl_IsPartPost_CheckedChanged(object sender, EventArgs e)
        {
            ctrl_PartPostValue.Enabled = ctrl_IsPartPost.Checked;
        }

        private void ctrl_IsPartLocations_CheckedChanged(object sender, EventArgs e)
        {
            ctrl_PartLocationsValue.Enabled = ctrl_IsPartLocations.Checked;
            ctrl_IsPartLocationsMulti.Enabled = ctrl_IsPartLocations.Checked;
        }

        private void ctrl_IsPartNorm_CheckedChanged(object sender, EventArgs e)
        {
            ctrl_PartNormValue.Enabled = ctrl_IsPartNorm.Checked;
        }

        private void ctrl_IsPartNormRange_CheckedChanged(object sender, EventArgs e)
        {
            ctrl_PartNormRangeValue.Enabled = ctrl_IsPartNormRange.Checked;
        }
        #endregion

        #region Number
        private bool f_ValidNumber(string[] a_Texts)
        {
            bool valid = true;
            if (ctrl_IsNumber.Checked)
            {
                if (a_Texts != null && a_Texts.Length > 0)
                {
                    foreach (string txt in a_Texts)
                    {
                        decimal x;
                        valid = decimal.TryParse(txt, out x);
                        if (valid)
                        {
                            int index = txt.IndexOf(",");
                            if (index >= 0)
                            {
                                if ((int)ctrl_NumberRound.Value > 0)
                                {
                                    valid = index + (int)ctrl_NumberRound.Value >= txt.Length - 1;
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
            RichTextBox el = ((RichTextBox)sender);
            if (ctrl_IsNumber.Checked)
            {
                int pos = el.SelectionStart - el.GetFirstCharIndexOfCurrentLine();
                string txt = el.Text;
                if (el.Lines.Length > 0)
                {
                    txt = el.Lines.ElementAt(el.GetLineFromCharIndex(el.SelectionStart));
                }
                txt = string.Format("{0}{1}{2}", txt.Substring(0, pos), e.KeyChar, txt.Substring(pos, txt.Length - pos));
                e.Handled = !f_ValidNumber(new string[] { txt });
            }
            else
            {
                e.Handled = false;
            }
        }

        private void ctrl_NormValues_KeyPress(object sender, KeyPressEventArgs e)
        {
            ctrl_ValidNumber_KeyPress(sender, e);
        }

        private void ctrl_PatValues_KeyPress(object sender, KeyPressEventArgs e)
        {
            ctrl_ValidNumber_KeyPress(sender, e);
        }

        private void ctrl_Default_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        #endregion

        #region Default
        private void f_SetDataDefault()
        {
            ctrl_Default.f_Clear();
            foreach (string val in ctrl_NormValues.Lines)
            {
                if (!string.IsNullOrWhiteSpace(val))
                    ctrl_Default.f_AddString(val);
            }
            ctrl_Default.f_SetSeparator(ctrl_NormValues.Lines.Length);
            foreach (string val in ctrl_PatValues.Lines)
            {
                if (!string.IsNullOrWhiteSpace(val))
                    ctrl_Default.f_AddString(val);
            }
        }

        private void ctrl_NormValues_TextChanged(object sender, EventArgs e)
        {
            f_SetDataDefault();
        }

        private void ctrl_PatValues_TextChanged(object sender, EventArgs e)
        {
            f_SetDataDefault();
        }
        #endregion

        private void ctrlBEditFormula_Click(object sender, EventArgs e)
        {
            F_EditorFormula fEditor = new F_EditorFormula();
            fEditor.Text = string.Format("Редактор формулы числового элемента \"{0}\"", p_EditingElement.p_Name);
            fEditor.f_SetFormula(ctrl_NumberFormula.Text);
            if (fEditor.ShowDialog(this) == DialogResult.OK)
            {
                ctrl_NumberFormula.Text = fEditor.f_GetFormula();
            }
        }

        private void ctrlBEditHideFormula_Click(object sender, EventArgs e)
        {
            F_EditorСondition fEditor = new F_EditorСondition();
            fEditor.Text = string.Format("Редактор формулы видимости элемента \"{0}\"", p_EditingElement.p_Name);
            fEditor.f_SetFormula(ctrl_VisibilityFormula.Text);
            if (fEditor.ShowDialog(this) == DialogResult.OK)
            {
                ctrl_VisibilityFormula.Text = fEditor.f_GetFormula();
            }
        }

        private void ctrl_ControlType_Click(object sender, EventArgs e)
        {
            E_TextTypes textType = (E_TextTypes)ctrl_ControlType.f_GetSelectedItem();
            bool enable = true;
            if (textType == E_TextTypes.Bigbox)
                enable = false;
            ctrl_IsChangeNotNormValues.Enabled = enable;
            ctrl_LNormValues.Enabled = ctrl_NormValues.Enabled = enable;
            ctrl_LPatValues.Enabled = ctrl_PatValues.Enabled = enable;
            ctrl_IsPartLocations.Enabled = ctrl_PartLocationsValue.Enabled = ctrl_IsPartLocationsMulti.Enabled = enable;
            ctrl_IsMultiSelect.Enabled = enable;
            ctrl_IsPartPost.Enabled = enable;
            ctrl_PartPostValue.Enabled = enable;
            ctrl_IsNumber.Visible = ctrl_NumberParams.Visible = enable;
        }
    }
}
