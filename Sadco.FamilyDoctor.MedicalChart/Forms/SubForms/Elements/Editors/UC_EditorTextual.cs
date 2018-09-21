using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.Core.EntityLogs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;
using static Sadco.FamilyDoctor.Core.Entities.Cl_Element;
using FD.dat.mon.stb.lib;

namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
    public partial class UC_EditorTextual : UserControl, I_EditPanel
    {
        private Cl_EntityLog m_Log = new Cl_EntityLog();

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

        public Cl_Template[] f_GetConflictTemplates(Cl_Element a_Element)
        {
            var templates = new List<Cl_Template>();
            if (a_Element == null) return templates.ToArray();
            var templatesElements = Cl_App.m_DataContext.p_TemplatesElements.Include(te => te.p_Template).Where(te => te.p_ChildElementID == a_Element.p_ID).ToArray();
            foreach (var te in templatesElements)
            {
                templates.Add(te.p_Template);
                templates.AddRange(f_GetConflictTemplates(te.p_Template));
            }
            return templates.ToArray();
        }

        public Cl_Template[] f_GetConflictTemplates(Cl_Template a_Template)
        {
            var templates = new List<Cl_Template>();
            if (a_Template == null) return templates.ToArray();
            var templatesElements = Cl_App.m_DataContext.p_TemplatesElements.Include(te => te.p_Template).Where(te => te.p_ChildTemplateID == a_Template.p_ID).ToArray();
            foreach (var te in templatesElements)
            {
                templates.Add(te.p_Template);
                templates.AddRange(f_GetConflictTemplates(te.p_Template));
            }
            return templates.ToArray();
        }

        #region TPartNormRangeValues
        private byte? f_GetAgeFrom(DataGridViewRow a_Row)
        {
            byte bVal = 0;
            if (a_Row.Cells[0].Value == null)
            {
                MonitoringStub.Message("Поле \"Возраст от\" пустое!");
                return null;
            }
            else
            {
                if (byte.TryParse(a_Row.Cells[0].Value.ToString(), out bVal))
                    return bVal;
                else
                {
                    MonitoringStub.Message("Значение поля \"Возраст от\" не корректное!");
                    return null;
                }
            }
        }

        private byte? f_GetAgeTo(DataGridViewRow a_Row)
        {
            byte bVal = 0;
            if (a_Row.Cells[1].Value == null)
            {
                MonitoringStub.Message("Поле \"Возраст до\" пустое!");
                return null;
            }
            else
            {
                if (byte.TryParse(a_Row.Cells[1].Value.ToString(), out bVal))
                    return bVal;
                else
                {
                    MonitoringStub.Message("Значение поля \"Возраст до\" не корректное!");
                    return null;
                }
            }
        }

        private decimal? f_GetMaleMin(DataGridViewRow a_Row)
        {
            decimal dVal = 0;
            if (a_Row.Cells[2].Value == null)
            {
                MonitoringStub.Message("Поле \"Муж мин\" пустое!");
                return null;
            }
            else
            {
                if (decimal.TryParse(a_Row.Cells[2].Value.ToString(), out dVal))
                {
                    if (f_ValidNumber(new string[] { dVal.ToString() }))
                        return dVal;
                    else
                    {
                        MonitoringStub.Message("Значение поля \"Муж мин\" не соответствует точности!");
                        return null;
                    }
                }
                else
                {
                    MonitoringStub.Message("Значение поля \"Муж мин\" не корректное!");
                    return null;
                }
            }
        }

        private decimal? f_GetMaleMax(DataGridViewRow a_Row)
        {
            decimal dVal = 0;
            if (a_Row.Cells[3].Value == null)
            {
                MonitoringStub.Message("Поле \"Муж макс\" пустое!");
                return null;
            }
            else
            {
                if (decimal.TryParse(a_Row.Cells[3].Value.ToString(), out dVal))
                {
                    if (f_ValidNumber(new string[] { dVal.ToString() }))
                        return dVal;
                    else
                    {
                        MonitoringStub.Message("Значение поля \"Муж макс\" не соответствует точности!");
                        return null;
                    }
                }
                else
                {
                    MonitoringStub.Message("Значение поля \"Муж макс\" не корректное!");
                    return null;
                }
            }
        }

        private decimal? f_GetFemaleMin(DataGridViewRow a_Row)
        {
            decimal dVal = 0;
            if (a_Row.Cells[4].Value == null)
            {
                MonitoringStub.Message("Поле \"Жен мин\" пустое!");
                return null;
            }
            else
            {
                if (decimal.TryParse(a_Row.Cells[4].Value.ToString(), out dVal))
                {
                    if (f_ValidNumber(new string[] { dVal.ToString() }))
                        return dVal;
                    else
                    {
                        MonitoringStub.Message("Значение поля \"Жен мин\" не соответствует точности!");
                        return null;
                    }
                }
                else
                {
                    MonitoringStub.Message("Значение поля \"Жен мин\" не корректное!");
                    return null;
                }
            }
        }

        private decimal? f_GetFemaleMax(DataGridViewRow a_Row)
        {
            decimal dVal = 0;
            if (a_Row.Cells[5].Value == null)
            {
                MonitoringStub.Message("Поле \"Жен макс\" пустое!");
                return null;
            }
            else
            {
                if (decimal.TryParse(a_Row.Cells[5].Value.ToString(), out dVal))
                {
                    if (f_ValidNumber(new string[] { dVal.ToString() }))
                        return dVal;
                    else
                    {
                        MonitoringStub.Message("Значение поля \"Жен макс\" не соответствует точности!");
                        return null;
                    }
                }
                else
                {
                    MonitoringStub.Message("Значение поля \"Жен макс\" не корректное!");
                    return null;
                }
            }
        }

        private List<Cl_AgeNorm> f_GetPartNormRanges(int a_ElementID)
        {
            var norms = new List<Cl_AgeNorm>();
            decimal? dVal = null;
            byte? bVal = null;
            foreach (DataGridViewRow row in ctrl_TPartNormRangeValues.Rows)
            {
                if (!row.IsNewRow)
                {
                    Cl_AgeNorm norm = new Cl_AgeNorm();
                    bVal = f_GetAgeFrom(row);
                    if (bVal != null) norm.p_AgeFrom = (byte)bVal;
                    else return null;

                    bVal = f_GetAgeTo(row);
                    if (bVal != null) norm.p_AgeTo = (byte)bVal;
                    else return null;

                    if (norm.p_AgeFrom > norm.p_AgeTo)
                    {
                        MonitoringStub.Message("Значение поля \"Возраст от\" больше значения поля \"Возраст до\"!");
                        return null;
                    }

                    dVal = f_GetMaleMin(row);
                    if (dVal != null) norm.p_MaleMin = (decimal)dVal;
                    else return null;
                    dVal = f_GetMaleMax(row);
                    if (dVal != null) norm.p_MaleMax = (decimal)dVal;
                    else return null;

                    dVal = f_GetFemaleMin(row);
                    if (dVal != null) norm.p_FemaleMin = (decimal)dVal;
                    else return null;
                    dVal = f_GetFemaleMax(row);
                    if (dVal != null) norm.p_FemaleMax = (decimal)dVal;
                    else return null;

                    norm.p_ElementID = a_ElementID;
                    norms.Add(norm);
                }
            }
            return norms;
        }

        private void ctrl_TPartNormRangeValues_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == 0) f_GetAgeFrom(ctrl_TPartNormRangeValues.Rows[e.RowIndex]);
                if (e.ColumnIndex == 1) f_GetAgeTo(ctrl_TPartNormRangeValues.Rows[e.RowIndex]);
                if (e.ColumnIndex == 2) f_GetMaleMin(ctrl_TPartNormRangeValues.Rows[e.RowIndex]);
                if (e.ColumnIndex == 3) f_GetMaleMax(ctrl_TPartNormRangeValues.Rows[e.RowIndex]);
                if (e.ColumnIndex == 4) f_GetFemaleMin(ctrl_TPartNormRangeValues.Rows[e.RowIndex]);
                if (e.ColumnIndex == 5) f_GetFemaleMax(ctrl_TPartNormRangeValues.Rows[e.RowIndex]);
            }
        }

        #endregion

        public object f_ConfirmChanges()
        {
            if (!f_ValidNumber(ctrl_NormValues.Lines))
            {
                MonitoringStub.Message("Нормальные значения не являются числовыми или не соответствуют точности числа");
                return null;
            }
            if (!f_ValidNumber(ctrl_PatValues.Lines))
            {
                MonitoringStub.Message("Патологические значения не являются числовыми или не соответствуют точности числа");
                return null;
            }
            if (!f_ValidNumber(new string[] { ctrl_Default.Text }))
            {
                MonitoringStub.Message("Значение по-умолчанию не является числовым или не соответствует точности числа");
                return null;
            }
            var templates = f_GetConflictTemplates(p_EditingElement);
            if (templates.Length > 0)
            {
                if (MessageBox.Show("Этот элемент уже используется в шаблонах. Сохранить новую версию элмента?", "", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    return null;
                }
            }
            using (var transaction = Cl_App.m_DataContext.Database.BeginTransaction())
            {
                try
                {
                    decimal dVal = 0;
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
                    el.p_Name = ctrl_Name.Text.Trim();
                    el.p_Tag = ctrlTag.Text.Trim();
                    el.p_Help = ctrl_Hint.Text;

                    el.p_IsPartPre = ctrl_IsPartPre.Checked;
                    if (el.p_IsPartPre)
                        el.p_PartPre = ctrl_PartPreValue.Text.Trim();
                    el.p_IsPartPost = ctrl_IsPartPost.Checked;
                    if (el.p_IsPartPost)
                        el.p_PartPost = ctrl_PartPostValue.Text.Trim();
                    el.p_IsPartLocations = ctrl_IsPartLocations.Checked;
                    if (el.p_IsPartLocations)
                    {
                        el.p_IsPartLocationsMulti = ctrl_IsPartLocationsMulti.Checked;
                    }
                    el.p_IsPartNorm = ctrl_IsPartNorm.Checked;
                    if (el.p_IsPartNorm)
                    {
                        if (decimal.TryParse(ctrl_PartNormValue.Text, out dVal))
                        {
                            if (f_ValidNumber(new string[] { dVal.ToString() }))
                                el.p_PartNorm = dVal;
                            else
                            {
                                MonitoringStub.Message("Значение поля \"Норма\" не соответствует точности!");
                                transaction.Rollback();
                                return null;
                            }
                        }
                        else
                        {
                            MonitoringStub.Message("Значение поля \"Норма\" не корректное!");
                            transaction.Rollback();
                            return null;
                        }
                    }
                    el.p_IsPartNormRange = ctrl_IsPartNormRange.Checked;

                    el.p_IsChangeNotNormValues = ctrl_IsChangeNotNormValues.Checked;
                    el.p_Visible = ctrl_IsVisible.Checked;
                    el.p_VisiblePatient = ctrl_IsVisiblePatient.Checked;
                    el.p_Required = ctrl_IsRequiredFIeld.Checked;
                    el.p_Editing = ctrl_IsEditing.Checked;
                    el.p_IsMultiSelect = ctrl_IsMultiSelect.Checked;
                    el.p_Symmetrical = ctrl_IsSymmentry.Checked;
                    el.p_SymmetryParamLeft = ctrl_Symmetry1.Text.Trim();
                    el.p_SymmetryParamRight = ctrl_Symmetry2.Text.Trim();
                    el.p_IsNumber = ctrl_IsNumber.Checked;
                    el.p_NumberRound = Convert.ToByte(ctrl_NumberRound.Value);
                    el.p_NumberFormula = ctrl_NumberFormula.Text;
                    el.p_VisibilityFormula = ctrl_VisibilityFormula.Text;
                    el.p_Comment = ctrl_Note.Text;


                    Cl_App.m_DataContext.SaveChanges();
                    el.p_ParamsValues.Clear();
                    if (el.p_IsPartLocations)
                    {
                        el.f_AddValues(Cl_ElementParam.E_TypeParam.Location, ctrl_PartLocationsValue.Lines);
                    }
                    el.f_AddValues(Cl_ElementParam.E_TypeParam.NormValues, ctrl_NormValues.Lines);
                    el.f_AddValues(Cl_ElementParam.E_TypeParam.PatValues, ctrl_PatValues.Lines);
                    el.p_PartAgeNorms.Clear();
                    if (el.p_IsPartNormRange)
                    {
                        var norms = f_GetPartNormRanges(el.p_ID);
                        if (norms != null)
                        {
                            el.p_PartAgeNorms.AddRange(norms);
                        }
                        else
                        {
                            transaction.Rollback();
                            return null;
                        }
                    }

                    Cl_App.m_DataContext.SaveChanges();
                    if (ctrl_Default.SelectedItem != null)
                    {
                        el.p_Default = el.p_NormValues.FirstOrDefault(v => v.p_Value == ctrl_Default.SelectedItem.ToString());
                        if (el.p_Default == null)
                            el.p_Default = el.p_PatValues.FirstOrDefault(v => v.p_Value == ctrl_Default.SelectedItem.ToString());
                        if (el.p_Default != null)
                            el.p_DefaultID = el.p_Default.p_ID;
                        else
                            el.p_DefaultID = null;
                    }
                    Cl_App.m_DataContext.SaveChanges();
                    if (templates.Length > 0)
                    {
                        foreach (var t in templates)
                        {
                            t.p_IsConflict = true;
                        }
                    }

                    if (m_Log.f_IsChanged(el) == false)
                    {
                        if (el.Equals(p_EditingElement) && el.p_Version == 1)
                        {
                            el.p_Version = 0;
                        }

                        MonitoringStub.Message("Элемент не изменялся!");
                        transaction.Rollback();
                        return null;
                    }

                    m_Log.f_SaveEntity(el);
                    transaction.Commit();
                    f_SetElement(el);
                    return el;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MonitoringStub.Error("Error_Editor", "При сохранении изменений произошла ошибка", ex, null, null);
                    return null;
                }
            }
        }

        public void f_SetElement(Cl_Element a_Element)
        {
            if (a_Element == null || !a_Element.f_IsText()) return;
            m_Log.f_SetEntity(a_Element);
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
            ctrl_PartNormValue.Text = p_EditingElement.p_PartNorm.ToString();
            ctrl_IsPartNormRange.Checked = ctrl_TPartNormRangeValues.Enabled = p_EditingElement.p_IsPartNormRange;

            List<Cl_AgeNorm> ageNorms = Cl_App.m_DataContext.p_AgeNorms.Where(n => n.p_ElementID == a_Element.p_ID).ToList();
            DataTable dt = new DataTable();
            foreach (DataGridViewColumn col in ctrl_TPartNormRangeValues.Columns)
            {
                dt.Columns.Add(col.Name);
                col.DataPropertyName = col.Name;
            };
            foreach (var norm in ageNorms)
            {
                var row = dt.NewRow();
                row["p_AgeFrom"] = norm.p_AgeFrom;
                row["p_AgeTo"] = norm.p_AgeTo;
                row["p_MaleMin"] = Math.Round(norm.p_MaleMin, p_EditingElement.p_NumberRound);
                row["p_MaleMax"] = Math.Round(norm.p_MaleMax, p_EditingElement.p_NumberRound);
                row["p_FemaleMin"] = Math.Round(norm.p_FemaleMin, p_EditingElement.p_NumberRound);
                row["p_FemaleMax"] = Math.Round(norm.p_FemaleMax, p_EditingElement.p_NumberRound);
                dt.Rows.Add(row);
            }
            ctrl_TPartNormRangeValues.DataSource = dt;
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
            ctrl_Note.Text = p_EditingElement.p_Comment;

            ctrl_PartLocationsValue.Text = string.Join("\n", p_EditingElement.p_PartLocations.ToList());
            ctrl_NormValues.Text = string.Join("\n", p_EditingElement.p_NormValues.ToList());
            ctrl_PatValues.Text = string.Join("\n", p_EditingElement.p_PatValues.ToList());
            if (p_EditingElement.p_Default != null)
            {
                ctrl_Default.SelectedIndex = ctrl_Default.Items.IndexOf(p_EditingElement.p_Default.p_Value);
            }
            f_UpdateModeTextType((E_TextTypes)ctrl_ControlType.f_GetSelectedItem());
        }

        private void f_UpdateIsNumber()
        {
            ctrl_NumberParams.Visible = ctrl_IsPartNorm.Enabled = ctrl_IsPartNormRange.Enabled = ctrl_IsNumber.Visible && ctrl_IsNumber.Enabled && ctrl_IsNumber.Checked;
            if (!ctrl_IsNumber.Checked)
            {
                ctrl_IsPartNorm.Checked = ctrl_IsPartNormRange.Checked = false;
            }
            ctrl_PartNormValue.Enabled = ctrl_IsPartNorm.Enabled && ctrl_IsPartNorm.Checked;
            ctrl_TPartNormRangeValues.Enabled = ctrl_IsPartNormRange.Enabled && ctrl_IsPartNormRange.Checked;
        }

        private void f_UpdateModeTextType(E_TextTypes a_TextType)
        {
            bool enable = true;
            if (a_TextType == E_TextTypes.Bigbox)
            {
                enable = false;
                ctrl_IsChangeNotNormValues.Checked = true;
            }
            ctrl_IsChangeNotNormValues.Enabled = enable;
            ctrl_LPatValues.Enabled = ctrl_PatValues.Enabled = enable;
            ctrl_IsPartLocations.Enabled = enable;
            ctrl_PartLocationsValue.Enabled = ctrl_IsPartLocationsMulti.Enabled = ctrl_IsPartLocations.Enabled && ctrl_IsPartLocations.Checked;
            ctrl_IsMultiSelect.Enabled = enable;
            ctrl_IsPartPost.Enabled = enable;
            ctrl_PartPostValue.Enabled = ctrl_IsPartPost.Enabled && ctrl_IsPartPost.Checked;

            ctrl_IsNumber.Visible = enable;
            f_UpdateIsNumber();
        }

        #region CheckedChanged
        private void ctrl_IsSymmentry_CheckedChanged(object sender, EventArgs e)
        {
            ctrl_SymmetryVals.Visible = ctrl_IsSymmentry.Checked;
        }

        private void ctrl_CB_IsNumber_CheckedChanged(object sender, EventArgs e)
        {
            f_UpdateIsNumber();
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

        private bool m_IsBlockPartNorm = false;
        private void ctrl_IsPartNorm_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_IsBlockPartNorm)
            {
                ctrl_PartNormValue.Enabled = ctrl_IsPartNorm.Checked;
                m_IsBlockPartNorm = true;
                ctrl_IsPartNormRange.Checked = ctrl_TPartNormRangeValues.Enabled = false;
                m_IsBlockPartNorm = false;
            }
        }

        private void ctrl_IsPartNormRange_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_IsBlockPartNorm)
            {
                ctrl_TPartNormRangeValues.Enabled = ctrl_IsPartNormRange.Checked;
                m_IsBlockPartNorm = true;
                ctrl_IsPartNorm.Checked = ctrl_PartNormValue.Enabled = false;
                m_IsBlockPartNorm = false;
            }
        }

        private void ctrl_IsEditing_CheckedChanged(object sender, EventArgs e)
        {
            if (!ctrl_IsPartPost.Checked)
            {
                ctrl_IsRequiredFIeld.Checked = false;
            }
            ctrl_IsRequiredFIeld.Enabled = ctrl_IsEditing.Checked;
        }
        #endregion

        #region Number
        private bool f_ValidNumber(string[] a_Texts, bool isKeyPass = false)
        {
            bool valid = true;
            if (ctrl_IsNumber.Checked)
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
                if (e.KeyChar == 32)
                {
                    e.Handled = true;
                }
                else if (e.KeyChar != 8)
                {
                    int pos = el.SelectionStart - el.GetFirstCharIndexOfCurrentLine();
                    string txt = el.Text;
                    if (el.Lines.Length > 0)
                    {
                        txt = el.Lines.ElementAt(el.GetLineFromCharIndex(el.SelectionStart));
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

        private void ctrl_NormValues_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '"')
            {
                e.Handled = true;
                return;
            }

            ctrl_ValidNumber_KeyPress(sender, e);
        }

        private void ctrl_PatValues_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '"')
            {
                e.Handled = true;
                return;
            }

            ctrl_ValidNumber_KeyPress(sender, e);
        }

        private void ctrl_PartNormValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox el = ((TextBox)sender);
            e.Handled = !f_ValidNumber(new string[] { el.Text + e.KeyChar }, true);
        }

        private void ctrl_Default_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        #endregion

        private void ctrlTag_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 22)
            {
                IDataObject iData = Clipboard.GetDataObject();
                var txt = iData.GetData(DataFormats.Text).ToString();
                e.Handled = txt.Any(ch => ch > 47 && ch < 58);
            }
            else
            {
                e.Handled = e.KeyChar != 3 && e.KeyChar != Keys.Back.GetHashCode() && e.KeyChar != Keys.Delete.GetHashCode() && (e.KeyChar < 65 || (e.KeyChar > 90 && e.KeyChar < 97) || e.KeyChar > 122);
            }
        }

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
            f_UpdateModeTextType((E_TextTypes)ctrl_ControlType.f_GetSelectedItem());
        }
    }
}
