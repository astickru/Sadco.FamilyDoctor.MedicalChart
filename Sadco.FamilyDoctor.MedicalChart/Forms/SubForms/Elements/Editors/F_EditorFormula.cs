using FD.dat.mon.stb.lib;
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
		private enum E_Opers
		{
			plus,
			minus,
			carve,
			multiply
		}

		private class Cl_Block
		{
			public Cl_Block(object a_Object)
			{
				p_Object = a_Object;
			}

			public object p_Object { get; set; }
			public const string m_OperatorTag = "tag_";

			public bool IsOperand {
				get {
					return p_Object is E_Opers;
				}
			}

			public string f_GetTextFromBlock()
			{
				if (p_Object is Cl_Element)
					return m_OperatorTag + ((Cl_Element)p_Object).p_Tag;
				else if (p_Object is E_Opers)
				{
					E_Opers oper = ((E_Opers)p_Object);
					switch (oper)
					{
						case E_Opers.plus:
							return " + ";
						case E_Opers.minus:
							return " - ";
						case E_Opers.carve:
							return " / ";
						case E_Opers.multiply:
							return " * ";
					}
				}
				else if (p_Object is int)
				{
					return p_Object.ToString();
				}
				return "";
			}

			/// <summary>Получение цвета блока</summary>
			/// <param name="a_Block">Блок</param>
			public Color f_GetColorBlock()
			{
				if (p_Object is Cl_Element)
					return Color.DarkGoldenrod;
				else if (p_Object is E_Opers)
				{
					E_Opers oper = ((E_Opers)p_Object);
					switch (oper)
					{
						case E_Opers.plus:
							return Color.Red;
						case E_Opers.minus:
							return Color.Red;
						case E_Opers.carve:
							return Color.Red;
						case E_Opers.multiply:
							return Color.Red;
					}
				}
				else if (p_Object is int)
				{
					return Color.Blue;
				}
				return Color.Green;
			}

			public override string ToString()
			{
				if (p_Object != null)
					return p_Object.ToString();
				else
					return null;
			}
		}

		private List<Cl_Block> m_Blocks = new List<Cl_Block>();
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
			ctrlBPlus.Tag = E_Opers.plus;
			ctrlBMinus.Tag = E_Opers.minus;
			ctrlBCarve.Tag = E_Opers.carve;
			ctrlBMultiply.Tag = E_Opers.multiply;
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
			f_AppendBlock(new Cl_Block(el));
			f_UpdateControls(true);
		}

		private void ctrlBDelLastAction_Click(object sender, EventArgs e)
		{
			if (m_Blocks.Count == 0) return;
			Cl_Block lastBlock = m_Blocks.LastOrDefault();
			f_RemoveBlock(lastBlock);
			f_UpdateControls(!m_VisibilityFormula);
		}

		private void addAction_Click(object sender, EventArgs e)
		{
			Button btn = (Button)sender;
			f_AppendBlock(new Cl_Block(btn.Tag));
			f_UpdateControls(false);
		}

		private void ctrlBAddValue_Click(object sender, EventArgs e)
		{
			int result = 0;
			if (!int.TryParse(ctrlTBValue.Text, out result)) return;
			f_AppendBlock(new Cl_Block(result));
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
		private bool f_AppendBlock(Cl_Block a_Block)
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
		private bool f_RemoveBlock(Cl_Block a_Block)
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

		/// <summary>Получение первого блока из текста</summary>
		/// <param name="a_Text">Текст</param>
		/// <returns></returns>
		private Cl_Block f_GetFirstBlockFromText(string a_Text)
		{
			if (!string.IsNullOrWhiteSpace(a_Text))
			{
				string txt = "";
				if (a_Text.Length >= 3)
				{
					txt = a_Text.Substring(0, 3);
					if (txt == " + ")
						return new Cl_Block(E_Opers.plus);
					else if (txt == " - ")
						return new Cl_Block(E_Opers.minus);
					else if (txt == " / ")
						return new Cl_Block(E_Opers.carve);
					else if (txt == " * ")
						return new Cl_Block(E_Opers.multiply);
				}
				if (a_Text.Length > Cl_Block.m_OperatorTag.Length)
				{
					txt = a_Text.Substring(0, Cl_Block.m_OperatorTag.Length);
					if (txt == Cl_Block.m_OperatorTag)
					{
						int indexEnd = a_Text.IndexOf(" ");
						if (indexEnd > -1)
							txt = a_Text.Substring(Cl_Block.m_OperatorTag.Length, indexEnd - Cl_Block.m_OperatorTag.Length);
						else
							txt = a_Text.Replace(Cl_Block.m_OperatorTag, "");
						Cl_Element element = m_Elements.FirstOrDefault(el => el.p_Tag == txt);
						if (element != null)
							return new Cl_Block(element);
					}
				}
				if (a_Text.Length > 0)
				{
					int indexEnd = a_Text.IndexOf(" ");
					if (indexEnd > -1)
						txt = a_Text.Substring(0, indexEnd);
					else
						txt = a_Text;
					int iVal = 0;
					if (int.TryParse(txt, out iVal))
					{
						return new Cl_Block(iVal);
					}
				}
			}
			return null;
		}

		/// <summary>Указание формулы</summary>
		/// <param name="a_Formula">Формула</param>
		public void f_SetFormula(string a_Formula)
		{
			f_ClearBlocks();
			string formula = a_Formula;
			while (!string.IsNullOrWhiteSpace(formula))
			{
				Cl_Block block = f_GetFirstBlockFromText(formula);
				if (block != null)
				{
					if (f_AppendBlock(block))
					{
						string txtBlock = block.f_GetTextFromBlock();
						formula = formula.Substring(txtBlock.Length);
					}
					else
					{
						formula = null;
					}
				}
				else
				{
					formula = null;
				}
			}
			f_UpdateControls(m_Blocks.Count > 0);
		}

		/// <summary>Проверка корректности формулы</summary>
		public bool f_Valid()
		{
			Cl_Block block = m_Blocks.LastOrDefault();
			if (block != null)
				return !block.IsOperand;
			else
				return true;
		}

		private void F_EditorFormula_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (IsDiscard) return;
			if (e.CloseReason == CloseReason.UserClosing) return;

			if (!f_Valid())
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
