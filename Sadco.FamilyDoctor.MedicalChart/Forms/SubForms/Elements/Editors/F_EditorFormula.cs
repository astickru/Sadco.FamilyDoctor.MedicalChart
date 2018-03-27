using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Entities;
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
		private const string operatorTag = "тег_";
		private const string operatorPlus = "+";
		private const string operatorMinus = "-";
		private const string operatorCarve = "/";
		private const string operatorMultiply = "*";

		List<int> m_ItemsFormula = new List<int>();
		bool m_VisibilityFormula = false;

		public F_EditorFormula() {
            this.Font = new System.Drawing.Font(ConfigurationManager.AppSettings["FontFamily"],
                float.Parse(ConfigurationManager.AppSettings["FontSize"]),
                (System.Drawing.FontStyle)int.Parse(ConfigurationManager.AppSettings["FontStyle"]),
                System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            InitializeComponent();
			InitializeOptions();

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

		private void InitializeOptions() {
			ctrlBPlus.Tag = operatorPlus;
			ctrlBMinus.Tag = operatorMinus;
			ctrlBCarve.Tag = operatorCarve;
			ctrlBMultiply.Tag = operatorMultiply;
		}

		private void ctrlBAddTag_Click(object sender, EventArgs e) {
			if (ctrlCBAddElement.SelectedItem == null || !(ctrlCBAddElement.SelectedItem is Cl_Element)) return;
			Cl_Element el = (Cl_Element)ctrlCBAddElement.SelectedItem;
			if (string.IsNullOrWhiteSpace(el.p_Tag)) {
				MessageBox.Show("Невозможно добавить элемент \"" + el.p_Name + "\" в редактор формул, т.к. у него не заполнено поле \"Тег элемента\"", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			m_ItemsFormula.Add(f_AppendText(operatorTag + el.p_Tag, Color.DarkGoldenrod));
			f_UpdateVisibilityFormula(true);
		}

		private void ctrlBDelLastAction_Click(object sender, EventArgs e) {
			if (m_ItemsFormula.Count == 0) return;
			int lastLenght = m_ItemsFormula[m_ItemsFormula.Count - 1];
			f_RemoveText(lastLenght);
			m_ItemsFormula.RemoveAt(m_ItemsFormula.Count - 1);
			f_UpdateVisibilityFormula(!m_VisibilityFormula);
		}

		private void addAction_Click(object sender, EventArgs e) {
			Button btn = (Button)sender;
			Color color = Color.Red;
			m_ItemsFormula.Add(f_AppendText(" " + btn.Tag.ToString() + " ", color));
			f_UpdateVisibilityFormula(false);
		}

		private void ctrlBAddValue_Click(object sender, EventArgs e) {
			int result = 0;
			if (!int.TryParse(ctrlTBValue.Text, out result)) return;
			m_ItemsFormula.Add(f_AppendText(result.ToString(), Color.Blue));
			f_UpdateVisibilityFormula(true);
		}

		private void ctrlBClear_Click(object sender, EventArgs e) {
			if (MessageBox.Show("Очистить формулу?", "Очистка", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

			m_ItemsFormula.Clear();
			f_UpdateVisibilityFormula(false);
			f_ClearText();
		}


		private void f_UpdateVisibilityFormula(bool isValue) {
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
		private int f_AppendText(string a_Text, Color a_Color) {
			ctrlRTBFormula.SelectionStart = ctrlRTBFormula.TextLength;
			ctrlRTBFormula.SelectionLength = 0;
			ctrlRTBFormula.SelectionColor = a_Color;
			ctrlRTBFormula.AppendText(a_Text);
			ctrlRTBFormula.SelectionColor = ctrlRTBFormula.ForeColor;
			return a_Text.Length;
		}

		/// <summary>Удаление блока из формулы</summary>
		/// <param name="a_Index">Индекс в формуле</param>
		private void f_RemoveText(int a_Index) {
			int start = 0;
			if (ctrlRTBFormula.TextLength == 0) return;
			if (a_Index > ctrlRTBFormula.TextLength) {
				start = 0;
			} else {
				start = ctrlRTBFormula.TextLength - a_Index;
			}
			ctrlRTBFormula.ReadOnly = false;
			ctrlRTBFormula.SelectionStart = start;
			ctrlRTBFormula.SelectionLength = a_Index;
			ctrlRTBFormula.SelectedText = "";
			ctrlRTBFormula.ReadOnly = true;
		}

		/// <summary>Очистка блоков из формулы</summary>
		private void f_ClearText() {
			ctrlRTBFormula.ReadOnly = false;
			ctrlRTBFormula.SelectionStart = 0;
			ctrlRTBFormula.SelectionLength = 0;
			ctrlRTBFormula.SelectedText = "";
			ctrlRTBFormula.Text = "";
			ctrlRTBFormula.ReadOnly = true;
		}

		/// <summary>Получение формулы</summary>
		/// <returns>Формула</returns>
		public string f_GetFormula() {
			return ctrlRTBFormula.Text;
		}

		/// <summary>Указание формулы</summary>
		/// <param name="a_Formula">Формула</param>
		public void f_SetFormula(string a_Formula) {
			int lastPos = 0;
			string[] blocks = f_getOperators(a_Formula);

			for (int i = 0; i < blocks.Count(); i++) {
				string opParts = blocks[i].Trim();

				if (i > 0) {
					int pos = a_Formula.IndexOf(opParts, lastPos);
					string selectOp = a_Formula.Substring(lastPos, pos - lastPos).Trim();
					m_ItemsFormula.Add(f_AppendText(" " + selectOp + " ", Color.Red));
					f_UpdateVisibilityFormula(false);


					lastPos = pos;
				}

				if (opParts.Length > operatorTag.Length && opParts.Substring(0, operatorTag.Length) == operatorTag) {
					m_ItemsFormula.Add(f_AppendText(opParts, Color.DarkGoldenrod));
				} else {
					m_ItemsFormula.Add(f_AppendText(opParts, Color.Blue));
				}
				f_UpdateVisibilityFormula(true);

				lastPos += opParts.Length;
			}

			if (a_Formula.Length > lastPos) {
				string lastOp = a_Formula.Substring(lastPos, a_Formula.Length - lastPos).Trim();

				if (!string.IsNullOrEmpty(lastOp)) {
					m_ItemsFormula.Add(f_AppendText(" " + lastOp + " ", Color.Red));
					f_UpdateVisibilityFormula(false);
				}
			}
		}

		private string[] f_getOperators(string comparePart) {
			if (string.IsNullOrEmpty(comparePart))
				return new string[0];

			return comparePart.Trim().Split(new string[] {
				" " + operatorPlus,
				" " + operatorMinus,
				" " + operatorCarve,
				" " + operatorMultiply}, StringSplitOptions.RemoveEmptyEntries);
		}
	}
}
