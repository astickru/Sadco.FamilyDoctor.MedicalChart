using FD.dat.mon.stb.lib;
using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.Core.Facades;
using Sadco.FamilyDoctor.Core.Formula;
using Sadco.FamilyDoctor.Core.Permision;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
    public partial class F_EditorСondition : Form
    {
        public F_EditorСondition()
        {
            this.Font = new Font(ConfigurationManager.AppSettings["FontFamily"],
                    float.Parse(ConfigurationManager.AppSettings["FontSize"]),
                    (FontStyle)int.Parse(ConfigurationManager.AppSettings["FontStyle"]),
                    GraphicsUnit.Point, ((byte)(204)));
            InitializeComponent();
            InitializeOptions();

            ctrlStandartValues.p_SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            ctrlCBAddElement.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ctrlCBAddElement.AutoCompleteSource = AutoCompleteSource.CustomSource;
            var elements = Cl_App.m_DataContext.p_Elements.Where(e => !e.p_IsDelete && e.p_ElementType != Cl_Element.E_ElementsTypes.Image).GroupBy(e => e.p_ElementID)
                                .Select(grp => grp.OrderByDescending(v => v.p_Version).FirstOrDefault()).ToList();

            elements.Insert(0, new Cl_Element() { p_Tag = "age", p_Name = "Возраст", p_IsNumber = true });
            elements.Insert(1, new Cl_Element() { p_Tag = "gender", p_Name = "Пол" });
            m_Elements = elements.ToArray();

            ctrlCBAddElement.AutoCompleteCustomSource.AddRange(m_Elements.Select(e => e.p_Name).ToArray());
            ctrlCBAddElement.DataSource = new BindingSource(m_Elements, null);
            ctrlCBAddElement.DisplayMember = "p_Name";
            ctrlCBAddElement.ValueMember = "p_Tag";

            f_UpdateControls(m_NumberBlockOper);
        }

        private List<Cl_FormulaConditionBlock> m_Blocks = new List<Cl_FormulaConditionBlock>();
        private int m_NumberBlockOper = 1;
        private Cl_Element[] m_Elements = null;

        private void InitializeOptions()
        {
            ctrlBOperMore.Tag = Cl_FormulaConditionBlock.E_Opers.more;
            ctrlBOperLess.Tag = Cl_FormulaConditionBlock.E_Opers.less;
            ctrlBOperEquals.Tag = Cl_FormulaConditionBlock.E_Opers.equals;
            ctrlBOperNotEquals.Tag = Cl_FormulaConditionBlock.E_Opers.notEquals;
            ctrlBOperAnd.Tag = Cl_FormulaConditionBlock.E_Opers.and;
            ctrlBOperOr.Tag = Cl_FormulaConditionBlock.E_Opers.or;
        }

        private bool f_GetIsNumberBlockNumber()
        {
            Cl_Element el = null;
            int index = m_Blocks.Count - m_NumberBlockOper + 1;
            if (index >= 0 && m_Blocks.Count > index)
                el = m_Blocks[index].p_Object as Cl_Element;
            if (el != null)
                return el.p_IsNumber;
            return false;
        }

        private bool f_GetIsGenderBlock()
        {
            Cl_Element el = null;
            int index = m_Blocks.Count - m_NumberBlockOper + 1;
            if (index >= 0 && m_Blocks.Count > index)
                el = m_Blocks[index].p_Object as Cl_Element;
            if (el != null)
                return el.p_Tag == "gender";
            return false;
        }

        private void f_UpdateStandartValues(Cl_Element a_Element)
        {
            ctrlStandartValues.f_Clear();
            if (!a_Element.p_IsNumber)
            {
                foreach (Cl_ElementParam val in a_Element.p_NormValues)
                {
                    ctrlStandartValues.f_AddObject(val);
                }
                ctrlStandartValues.f_SetSeparator(a_Element.p_NormValues.Length);
                foreach (Cl_ElementParam val in a_Element.p_PatValues)
                {
                    ctrlStandartValues.f_AddObject(val);
                }
            }
        }

        private void ctrlBAddTag_Click(object sender, EventArgs e)
        {
            if (ctrlCBAddElement.SelectedItem == null || !(ctrlCBAddElement.SelectedItem is Cl_Element)) return;
            Cl_Element el = (Cl_Element)ctrlCBAddElement.SelectedItem;
            if (string.IsNullOrWhiteSpace(el.p_Tag))
            {
                MonitoringStub.Problem("Problem_Formula", "Невозможно добавить элемент \"" + el.p_Name + "\" в редактор формул, т.к. у него не заполнено поле \"Тег элемента\"", new Exception("EX PROBLEM"), "el.p_Tag = null", null);
                return;
            }
            f_AppendBlock(new Cl_FormulaConditionBlock(el));
            f_UpdateStandartValues(el);
            f_UpdateControls(++m_NumberBlockOper);
        }

        private void ctrlBDelLastAction_Click(object sender, EventArgs e)
        {
            if (m_Blocks.Count == 0) return;
            Cl_FormulaConditionBlock lastBlock = m_Blocks.LastOrDefault();
            f_RemoveBlock(lastBlock);
            m_NumberBlockOper--;
            if (m_Blocks.Count > 1 && m_NumberBlockOper == 3)
            {
                if (m_Blocks[m_Blocks.Count - 2].p_Object is Cl_Element)
                {
                    f_UpdateStandartValues((Cl_Element)m_Blocks[m_Blocks.Count - 2].p_Object);
                }
            }
            f_UpdateControls(m_NumberBlockOper);
        }

        private void addAction_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            f_AppendBlock(new Cl_FormulaConditionBlock(btn.Tag));
            f_UpdateControls(++m_NumberBlockOper);
        }

        private void ctrlBAddValue_Click(object sender, EventArgs e)
        {
            int result = 0;
            if (!int.TryParse(ctrlTBValue.Text, out result)) return;
            f_AppendBlock(new Cl_FormulaConditionBlock(result));
            f_UpdateControls(++m_NumberBlockOper);
        }

        private void ctrlBAddMan_Click(object sender, EventArgs e)
        {
            f_AppendBlock(new Cl_FormulaConditionBlock(Cl_User.E_Sex.Man));
            f_UpdateControls(++m_NumberBlockOper);
        }

        private void ctrlBAddFemale_Click(object sender, EventArgs e)
        {
            f_AppendBlock(new Cl_FormulaConditionBlock(Cl_User.E_Sex.Female));
            f_UpdateControls(++m_NumberBlockOper);
        }

        private void ctrlBClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Очистить формулу?", "Очистка", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            m_NumberBlockOper = 1;
            f_UpdateControls(m_NumberBlockOper);
            f_ClearBlocks();
        }


        private void ctrlStandartValues_SelectedIndexChanged(object sender, EventArgs e)
        {
            f_AppendBlock(new Cl_FormulaConditionBlock(ctrlStandartValues.SelectedItem));
            f_UpdateControls(++m_NumberBlockOper);
            ctrlStandartValues.f_Clear();
        }

        private void f_UpdateControls(int state)
        {
            bool visGroup1 = false;
            bool visGroup2 = false;
            bool visGroup3 = false;

            if (state > 4)
            {
                m_NumberBlockOper = 1;
                f_UpdateControls(m_NumberBlockOper);
                return;
            }
            else if (state == 0)
            {
                m_NumberBlockOper = 4;
                f_UpdateControls(m_NumberBlockOper);
                return;
            }
            else
            {
                if ((state % 2) == 1)
                {
                    visGroup1 = true;
                    visGroup2 = visGroup3 = !visGroup1;
                }
                else if (state == 2)
                {
                    visGroup2 = true;
                    visGroup1 = visGroup3 = !visGroup2;
                }
                else
                {
                    visGroup3 = true;
                    visGroup1 = visGroup2 = !visGroup3;
                }
            }

            ctrlCBAddElement.Enabled = visGroup1;
            ctrlBAddTag.Enabled = visGroup1;

            ctrlBOperNotEquals.Enabled = visGroup2;
            ctrlBOperEquals.Enabled = visGroup2;

            if (f_GetIsGenderBlock())
            {
                ctrlPAddGender.Visible = true;
                ctrlPAddValue.Visible = false;
                ctrlPAddGender.Enabled = visGroup1;
            }
            else
            {
                ctrlPAddGender.Visible = false;
                ctrlPAddValue.Visible = true;
            }
            if (!f_GetIsNumberBlockNumber())
            {
                ctrlBOperLess.Enabled = false;
                ctrlBOperMore.Enabled = false;
                ctrlCBAddElement.DataSource = new BindingSource(m_Elements, null);
                ctrlStandartValues.Enabled = m_NumberBlockOper == 3;
                ctrlPAddValue.Enabled = false;
            }
            else
            {
                ctrlBOperLess.Enabled = visGroup2;
                ctrlBOperMore.Enabled = visGroup2;
                ctrlCBAddElement.DataSource = new BindingSource(m_Elements.Where(el => el.p_IsNumber), null);
                ctrlStandartValues.Enabled = false;
                if (m_NumberBlockOper > 1)
                {
                    ctrlPAddValue.Enabled = visGroup1;
                }
            }

            ctrlBOperAnd.Enabled = visGroup3;
            ctrlBOperOr.Enabled = visGroup3;
        }

        /// <summary>Добавление блока в формулу</summary>
        /// <param name="a_Block">Блок</param>
        /// <returns></returns>
        private bool f_AppendBlock(Cl_FormulaConditionBlock a_Block)
        {
            if (a_Block == null) return false;

            if (a_Block.p_Object is string)
            {
                string txt = a_Block.p_Object.ToString();
                if (m_Blocks.Count < 2) return false;
                Cl_Element el = m_Blocks[m_Blocks.Count - 2].p_Object as Cl_Element;
                if (el == null || el.p_IsNumber) return false;
                Cl_ElementParam prm = el.p_NormValues.FirstOrDefault(val => val.p_Value == txt);
                if (prm != null)
                {
                    a_Block.p_Object = prm;
                }
                else
                {
                    prm = el.p_PatValues.FirstOrDefault(val => val.p_Value == txt);
                    if (prm != null)
                    {
                        a_Block.p_Object = prm;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            string text = a_Block.f_GetTextFromBlock();
            if (string.IsNullOrWhiteSpace(text)) return false;
            ctrlRTBFormula.SelectionStart = ctrlRTBFormula.TextLength;
            ctrlRTBFormula.SelectionLength = 0;
            ctrlRTBFormula.SelectionColor = a_Block.f_GetColorBlock();
            ctrlRTBFormula.AppendText(text);
            ctrlRTBFormula.SelectionColor = ctrlRTBFormula.ForeColor;
            m_Blocks.Add(a_Block);
            return true;
        }

        /// <summary>Удаление блока из формулы</summary>
        /// <param name="a_Block">Блок</param>
        private bool f_RemoveBlock(Cl_FormulaConditionBlock a_Block)
        {
            if (a_Block == null) return false;
            if (ctrlRTBFormula.TextLength == 0) return false;
            string text = a_Block.f_GetTextFromBlock();
            if (string.IsNullOrWhiteSpace(text)) return false;
            int start = 0;
            if (text.Length > ctrlRTBFormula.TextLength)
            {
                start = 0;
            }
            else
            {
                start = ctrlRTBFormula.TextLength - text.Length;
            }
            ctrlRTBFormula.ReadOnly = false;
            ctrlRTBFormula.SelectionStart = start;
            ctrlRTBFormula.SelectionLength = text.Length;
            ctrlRTBFormula.SelectedText = "";
            ctrlRTBFormula.ReadOnly = true;
            m_Blocks.RemoveAt(m_Blocks.LastIndexOf(a_Block));
            return true;
        }

        /// <summary>Очистка блоков из формулы</summary>
        private void f_ClearBlocks()
        {
            ctrlRTBFormula.ReadOnly = false;
            ctrlRTBFormula.SelectionStart = 0;
            ctrlRTBFormula.SelectionLength = 0;
            ctrlRTBFormula.SelectedText = "";
            ctrlRTBFormula.Text = "";
            ctrlRTBFormula.ReadOnly = true;
            m_Blocks.Clear();
        }

        /// <summary>Получение формулы</summary>
        /// <returns>Формула</returns>
        public string f_GetFormula()
        {
            return ctrlRTBFormula.Text;
        }

        /// <summary>Указание формулы</summary>
        /// <param name="a_Formula">Формула</param>
        public void f_SetFormula(string a_Formula)
        {
            f_ClearBlocks();
            var blocks = Cl_FormulaFacade.f_GetInstance().f_GetConditionsBlocks(m_Elements, a_Formula);
            if (blocks != null)
            {
                foreach (var block in blocks)
                {
                    f_AppendBlock(block);
                }
            }
            if (m_Blocks.Count > 0)
                m_NumberBlockOper = 4;
            else
                m_NumberBlockOper = 1;
            f_UpdateControls(m_NumberBlockOper);
        }

        private void F_EditorСondition_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.IsDiscard) return;

            if (!Cl_FormulaFacade.f_GetInstance().f_Valid(m_Blocks))
            {
                MonitoringStub.Message("Формула не корректная!");
                e.Cancel = true;
            }
        }

        private bool IsDiscard = false;
        private void ctrlBCancel_Click(object sender, EventArgs e)
        {
            IsDiscard = true;
            this.Close();
        }
    }
}
