using Sadco.FamilyDoctor.Core;
using Sadco.FamilyDoctor.Core.Entities;
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
		private enum E_Opers
		{
			more,
			less,
			equals,
			notEquals,
			and,
			or
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
						case E_Opers.more:
							return " > ";
						case E_Opers.less:
							return " < ";
						case E_Opers.equals:
							return " = ";
						case E_Opers.notEquals:
							return " != ";
						case E_Opers.and:
							return " И ";
						case E_Opers.or:
							return " ИЛИ ";
					}
				}
				else if (p_Object is int)
				{
					return p_Object.ToString();
				}
				else if (p_Object is Cl_ElementsParams)
				{
					string preValue = p_Object.ToString();
					return preValue = "\"" + preValue.Trim() + "\"";
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
						case E_Opers.more:
							return Color.Red;
						case E_Opers.less:
							return Color.Red;
						case E_Opers.equals:
							return Color.Red;
						case E_Opers.notEquals:
							return Color.Red;
						case E_Opers.and:
							return Color.Red;
						case E_Opers.or:
							return Color.Red;
					}
				}
				else if (p_Object is int)
				{
					return Color.Blue;
				}
				else if (p_Object is Cl_ElementsParams)
				{
					return Color.BlueViolet;
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

		public F_EditorСondition()
		{
			this.Font = new System.Drawing.Font(ConfigurationManager.AppSettings["FontFamily"],
					float.Parse(ConfigurationManager.AppSettings["FontSize"]),
					(System.Drawing.FontStyle)int.Parse(ConfigurationManager.AppSettings["FontStyle"]),
					System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			InitializeComponent();
			InitializeOptions();

			ctrlStandartValues.p_SeparatorStyle = System.Drawing.Drawing2D.DashStyle.Dash;
			ctrlCBAddElement.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			ctrlCBAddElement.AutoCompleteSource = AutoCompleteSource.CustomSource;
			m_Elements = Cl_App.m_DataContext.p_Elements.Where(e => !e.p_IsArhive && e.p_ElementType != Cl_Element.E_ElementsTypes.Image).GroupBy(e => e.p_ElementID)
								.Select(grp => grp
											.OrderByDescending(v => v.p_Version).FirstOrDefault()).ToArray();
			ctrlCBAddElement.AutoCompleteCustomSource.AddRange(m_Elements.Select(e => e.p_Name).ToArray());
			ctrlCBAddElement.DataSource = new BindingSource(m_Elements, null);
			ctrlCBAddElement.DisplayMember = "p_Name";
			ctrlCBAddElement.ValueMember = "p_Tag";

			f_UpdateControls(m_NumberBlockOper);
		}

		private List<Cl_Block> m_Blocks = new List<Cl_Block>();
		private int m_NumberBlockOper = 1;
		private Cl_Element[] m_Elements = null;

		private void InitializeOptions()
		{
			ctrlBOperMore.Tag = E_Opers.more;
			ctrlBOperLess.Tag = E_Opers.less;
			ctrlBOperEquals.Tag = E_Opers.equals;
			ctrlBOperNotEquals.Tag = E_Opers.notEquals;
			ctrlBOperAnd.Tag = E_Opers.and;
			ctrlBOperOr.Tag = E_Opers.or;
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

		private void f_UpdateStandartValues(Cl_Element a_Element)
		{
			ctrlStandartValues.f_Clear();
			if (!a_Element.p_IsNumber)
			{
				foreach (Cl_ElementsParams val in a_Element.p_NormValues)
				{
					ctrlStandartValues.f_AddObject(val);
				}
				ctrlStandartValues.f_SetSeparator(a_Element.p_NormValues.Length);
				foreach (Cl_ElementsParams val in a_Element.p_PatValues)
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
				MessageBox.Show("Невозможно добавить элемент \"" + el.p_Name + "\" в редактор формул, т.к. у него не заполнено поле \"Тег элемента\"", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			f_AppendBlock(new Cl_Block(el));
			f_UpdateStandartValues(el);
			f_UpdateControls(++m_NumberBlockOper);
		}

		private void ctrlBDelLastAction_Click(object sender, EventArgs e)
		{
			if (m_Blocks.Count == 0) return;
			Cl_Block lastBlock = m_Blocks.LastOrDefault();
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
			f_AppendBlock(new Cl_Block(btn.Tag));
			f_UpdateControls(++m_NumberBlockOper);
		}

		private void ctrlBAddValue_Click(object sender, EventArgs e)
		{
			int result = 0;
			if (!int.TryParse(ctrlTBValue.Text, out result)) return;
			f_AppendBlock(new Cl_Block(result));
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
			f_AppendBlock(new Cl_Block(ctrlStandartValues.SelectedItem));
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

			if (!f_GetIsNumberBlockNumber())
			{
				ctrlBOperLess.Enabled = false;
				ctrlBOperMore.Enabled = false;
				ctrlCBAddElement.DataSource = new BindingSource(m_Elements, null);
				ctrlStandartValues.Enabled = m_NumberBlockOper == 3;
				ctrlTBValue.Enabled = false;
				ctrlBAddValue.Enabled = false;
			}
			else
			{
				ctrlBOperLess.Enabled = visGroup2;
				ctrlBOperMore.Enabled = visGroup2;
				ctrlCBAddElement.DataSource = new BindingSource(m_Elements.Where(el => el.p_IsNumber), null);
				ctrlStandartValues.Enabled = false;
				if (m_NumberBlockOper > 1)
				{
					ctrlTBValue.Enabled = visGroup1;
					ctrlBAddValue.Enabled = visGroup1;
				}
			}

			ctrlBOperAnd.Enabled = visGroup3;
			ctrlBOperOr.Enabled = visGroup3;
		}

		/// <summary>Добавление блока в формулу</summary>
		/// <param name="a_Block">Блок</param>
		/// <returns></returns>
		private bool f_AppendBlock(Cl_Block a_Block)
		{
			if (a_Block == null) return false;

			if (a_Block.p_Object is string)
			{
				string txt = a_Block.p_Object.ToString();
				if (m_Blocks.Count < 2) return false;
				Cl_Element el = m_Blocks[m_Blocks.Count - 2].p_Object as Cl_Element;
				if (el == null || el.p_IsNumber) return false;
				Cl_ElementsParams prm = el.p_NormValues.FirstOrDefault(val => val.p_Value == txt);
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
					if (txt == " > ")
						return new Cl_Block(E_Opers.more);
					else if (txt == " < ")
						return new Cl_Block(E_Opers.less);
					else if (txt == " = ")
						return new Cl_Block(E_Opers.equals);
					else if (txt == " И ")
						return new Cl_Block(E_Opers.and);
				}
				if (a_Text.Length >= 4)
				{
					txt = a_Text.Substring(0, 4);
					if (txt == " != ")
						return new Cl_Block(E_Opers.notEquals);
				}
				if (a_Text.Length >= 5)
				{
					txt = a_Text.Substring(0, 5);
					if (txt == " ИЛИ ")
						return new Cl_Block(E_Opers.or);
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
					int indexStart = 0;
					int lenght = 0;
					if (a_Text != "" && a_Text[0] == '"')
					{
						indexStart = 1;
						lenght = a_Text.IndexOf('"', 1) - indexStart;
					}
					else
					{
						byte[] asdsad = Encoding.ASCII.GetBytes(a_Text);
						lenght = a_Text.IndexOf(" ");
					}

					if (lenght > -1)
						txt = a_Text.Substring(indexStart, lenght);
					else
						txt = a_Text;

					int iVal = 0;
					if (int.TryParse(txt, out iVal))
					{
						return new Cl_Block(iVal);
					}
					if (!string.IsNullOrWhiteSpace(txt))
					{
						return new Cl_Block(txt);
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
			if (m_Blocks.Count > 0)
				m_NumberBlockOper = 4;
			else
				m_NumberBlockOper = 1;
			f_UpdateControls(m_NumberBlockOper);
		}

		/// <summary>Проверка корректности формулы</summary>
		public bool f_Valid()
		{
			if (m_Blocks.Count == 1)
				return false;
			else
			{
				Cl_Block block = m_Blocks.LastOrDefault();
				if (block != null)
					return !block.IsOperand;
				else
					return true;
			}
		}

		private void F_EditorСondition_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.IsDiscard) return;

			if (!f_Valid())
			{
				MessageBox.Show("Формула не корректная!");
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
