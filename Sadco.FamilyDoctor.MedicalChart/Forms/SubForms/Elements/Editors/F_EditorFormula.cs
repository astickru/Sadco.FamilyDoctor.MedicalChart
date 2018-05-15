using FD.dat.mon.stb.lib;
using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.Core.Facades;
using Sadco.FamilyDoctor.Core.Formula;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
    public partial class F_EditorFormula : Form
    {
        private List<Cl_FormulaMathematicalBlock> m_Blocks = new List<Cl_FormulaMathematicalBlock>();
        private bool m_VisibilityFormula = false;
        private Cl_Element[] m_Elements = null;

        public F_EditorFormula()
        {
            this.Font = new System.Drawing.Font(ConfigurationManager.AppSettings["FontFamily"],
                    float.Parse(ConfigurationManager.AppSettings["FontSize"]),
                    (System.Drawing.FontStyle)int.Parse(ConfigurationManager.AppSettings["FontStyle"]),
                    System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            InitializeComponent();
            InitializeOptions();

            ctrlCBAddElement.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ctrlCBAddElement.AutoCompleteSource = AutoCompleteSource.CustomSource;
            m_Elements = Cl_App.m_DataContext.p_Elements.Where(e => !e.p_IsDelete && e.p_ElementType != Cl_Element.E_ElementsTypes.Image).GroupBy(e => e.p_ElementID)
                                .Select(grp => grp
                                            .OrderByDescending(v => v.p_Version).FirstOrDefault()).Where(e => e.p_IsNumber).ToArray();
            ctrlCBAddElement.AutoCompleteCustomSource.AddRange(m_Elements.Select(e => e.p_Name).ToArray());
            ctrlCBAddElement.DataSource = new BindingSource(m_Elements, null);
            ctrlCBAddElement.DisplayMember = "p_Name";
            ctrlCBAddElement.ValueMember = "p_Tag";

            f_UpdateControls(false);
        }

        private void InitializeOptions()
        {
            ctrlBPlus.Tag = Cl_FormulaMathematicalBlock.E_Opers.plus;
            ctrlBMinus.Tag = Cl_FormulaMathematicalBlock.E_Opers.minus;
            ctrlBCarve.Tag = Cl_FormulaMathematicalBlock.E_Opers.carve;
            ctrlBMultiply.Tag = Cl_FormulaMathematicalBlock.E_Opers.multiply;
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
            f_AppendBlock(new Cl_FormulaMathematicalBlock(el));
            f_UpdateControls(true);
        }

        private void ctrlBDelLastAction_Click(object sender, EventArgs e)
        {
            if (m_Blocks.Count == 0) return;
            Cl_FormulaMathematicalBlock lastBlock = m_Blocks.LastOrDefault();
            f_RemoveBlock(lastBlock);
            f_UpdateControls(!m_VisibilityFormula);
        }

        private void addAction_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            f_AppendBlock(new Cl_FormulaMathematicalBlock(btn.Tag));
            f_UpdateControls(false);
        }

        private void ctrlBAddValue_Click(object sender, EventArgs e)
        {
            int result = 0;
            if (!int.TryParse(ctrlTBValue.Text, out result)) return;
            f_AppendBlock(new Cl_FormulaMathematicalBlock(result));
            f_UpdateControls(true);
        }

        private void ctrlBClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Очистить формулу?", "Очистка", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            f_UpdateControls(false);
            f_ClearBlocks();
        }

        private void f_UpdateControls(bool isValue)
        {
            m_VisibilityFormula = isValue;

            ctrlCBAddElement.Enabled = !isValue;
            ctrlBAddTag.Enabled = !isValue;
            ctrlTBValue.Enabled = !isValue;
            ctrlBAddValue.Enabled = !isValue;
            ctrlBPlus.Enabled = isValue;
            ctrlBMinus.Enabled = isValue;
            ctrlBCarve.Enabled = isValue;
            ctrlBMultiply.Enabled = isValue;
        }

        // <summary>Добавление блока в формулу</summary>
        /// <param name="a_Block">Блок</param>
        /// <returns></returns>
        private bool f_AppendBlock(Cl_FormulaMathematicalBlock a_Block)
        {
            if (a_Block == null) return false;
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
        private bool f_RemoveBlock(Cl_FormulaMathematicalBlock a_Block)
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
            var blocks = Cl_FormulaFacade.f_GetInstance().f_GetMathematicalsBlocks(m_Elements, a_Formula);
            if (blocks != null)
            {
                foreach (var block in blocks)
                {
                    f_AppendBlock(block);
                }
            }
            f_UpdateControls(m_Blocks.Count > 0);
        }

        private void F_EditorFormula_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsDiscard) return;
            if (e.CloseReason == CloseReason.UserClosing) return;

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
