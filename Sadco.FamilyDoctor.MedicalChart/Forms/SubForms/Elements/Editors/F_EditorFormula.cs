using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart.Forms.SubForms
{
    public partial class F_EditorFormula : Form
    {
        List<int> m_ItemsFormula = new List<int>();
        bool m_VisibilityFormula = false;

        public F_EditorFormula()
        {
            InitializeComponent();

            ctrlCBAddElement.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ctrlCBAddElement.AutoCompleteSource = AutoCompleteSource.CustomSource;
            var els = Cl_App.m_DataContext.p_Elements.Where(e => !e.p_IsArhive).GroupBy(e => e.p_ElementID)
                    .Select(grp => grp
                        .OrderByDescending(v => v.p_Version).FirstOrDefault()).Where(e => e.p_IsNumber).ToArray();
            ctrlCBAddElement.AutoCompleteCustomSource.AddRange(els.Select(e => e.p_Name).ToArray());
            ctrlCBAddElement.DataSource = new BindingSource(els, null);
            ctrlCBAddElement.DisplayMember = "p_Name";
            ctrlCBAddElement.ValueMember = "p_Tag";

            f_UpdateVisibilityFormula(false);
        }

        private void ctrlBAddTag_Click(object sender, EventArgs e)
        {
            if (ctrlCBAddElement.SelectedItem == null && !(ctrlCBAddElement.SelectedItem is Cl_Element)) return;
            Cl_Element el = (Cl_Element)ctrlCBAddElement.SelectedItem;
            if (string.IsNullOrWhiteSpace(el.p_Tag)) return;
            m_ItemsFormula.Add(f_AppendText("#" + el.p_Tag, Color.DarkGoldenrod));
            f_UpdateVisibilityFormula(true);
        }

        private void ctrlBDelLastAction_Click(object sender, EventArgs e)
        {
            if (m_ItemsFormula.Count == 0) return;
            int lastLenght = m_ItemsFormula[m_ItemsFormula.Count - 1];
            f_RemoveText(lastLenght);
            m_ItemsFormula.RemoveAt(m_ItemsFormula.Count - 1);
            f_UpdateVisibilityFormula(!m_VisibilityFormula);
        }

        private void addAction_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Color color = Color.Red;
            m_ItemsFormula.Add(f_AppendText(" " + btn.Tag.ToString() + " ", color));
            f_UpdateVisibilityFormula(false);
        }

        private void ctrlBAddValue_Click(object sender, EventArgs e)
        {
            int result = 0;
            if (!int.TryParse(ctrlTBValue.Text, out result)) return;
            m_ItemsFormula.Add(f_AppendText(result.ToString(), Color.Blue));
            f_UpdateVisibilityFormula(true);
        }

        private void ctrlBClear_Click(object sender, EventArgs e)
        {
            m_ItemsFormula.Clear();
            f_UpdateVisibilityFormula(false);
            f_ClearText();
        }


        private void f_UpdateVisibilityFormula(bool isValue)
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

        /// <summary>Добавление блока в формулу</summary>
        /// <param name="a_Text">Текст блока</param>
        /// <param name="a_Color">Цвет блока</param>
        /// <returns></returns>
        private int f_AppendText(string a_Text, Color a_Color)
        {
            ctrlRTBFormula.SelectionStart = ctrlRTBFormula.TextLength;
            ctrlRTBFormula.SelectionLength = 0;
            ctrlRTBFormula.SelectionColor = a_Color;
            ctrlRTBFormula.AppendText(a_Text);
            ctrlRTBFormula.SelectionColor = ctrlRTBFormula.ForeColor;
            return a_Text.Length;
        }

        /// <summary>Удаление блока из формулы</summary>
        /// <param name="a_Index">Индекс в формуле</param>
        private void f_RemoveText(int a_Index)
        {
            int start = 0;
            if (ctrlRTBFormula.TextLength == 0) return;
            if (a_Index > ctrlRTBFormula.TextLength)
            {
                start = 0;
            }
            else
            {
                start = ctrlRTBFormula.TextLength - a_Index;
            }
            ctrlRTBFormula.ReadOnly = false;
            ctrlRTBFormula.SelectionStart = start;
            ctrlRTBFormula.SelectionLength = a_Index;
            ctrlRTBFormula.SelectedText = "";
            ctrlRTBFormula.ReadOnly = true;
        }

        /// <summary>Очистка блоков из формулы</summary>
        private void f_ClearText()
        {
            ctrlRTBFormula.ReadOnly = false;
            ctrlRTBFormula.SelectionStart = 0;
            ctrlRTBFormula.SelectionLength = 0;
            ctrlRTBFormula.SelectedText = "";
            ctrlRTBFormula.Text = "";
            ctrlRTBFormula.ReadOnly = true;
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

        }
    }
}
