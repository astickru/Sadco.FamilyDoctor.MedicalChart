using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.Core.Formula;
using Sadco.FamilyDoctor.Core.Permision;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Sadco.FamilyDoctor.Core.Facades
{
    /// <summary>
    /// Фасад работы с формулами
    /// </summary>
    public class Cl_FormulaFacade
    {
        private static Cl_FormulaFacade INSTANCE = new Cl_FormulaFacade();
        public static Cl_FormulaFacade f_GetInstance()
        {
            return INSTANCE;
        }

        #region Condition
        /// <summary>Получение первого блока из текста отображения элемента</summary>
        /// <param name="a_Elements">Список элементов</param>
        /// <param name="a_Text">Текст</param>
        /// <returns></returns>
        private Cl_FormulaConditionBlock f_GetFirstConditionBlockFromText(Cl_Element[] a_Elements, string a_Text)
        {
            if (!string.IsNullOrWhiteSpace(a_Text))
            {
                string txt = "";
                if (a_Text.Length >= 3)
                {
                    txt = a_Text.Substring(0, 3);
                    if (txt == " > ")
                        return new Cl_FormulaConditionBlock(Cl_FormulaConditionBlock.E_Opers.more);
                    else if (txt == " < ")
                        return new Cl_FormulaConditionBlock(Cl_FormulaConditionBlock.E_Opers.less);
                    else if (txt == " = ")
                        return new Cl_FormulaConditionBlock(Cl_FormulaConditionBlock.E_Opers.equals);
                    else if (txt == " И ")
                        return new Cl_FormulaConditionBlock(Cl_FormulaConditionBlock.E_Opers.and);
                }
                if (a_Text.Length >= 4)
                {
                    txt = a_Text.Substring(0, 4);
                    if (txt == " != ")
                        return new Cl_FormulaConditionBlock(Cl_FormulaConditionBlock.E_Opers.notEquals);
                }
                if (a_Text.Length >= 5)
                {
                    txt = a_Text.Substring(0, 5);
                    if (txt == " ИЛИ ")
                        return new Cl_FormulaConditionBlock(Cl_FormulaConditionBlock.E_Opers.or);
                }
                if (a_Text.Length > Cl_FormulaConditionBlock.m_OperatorTag.Length)
                {
                    txt = a_Text.Substring(0, Cl_FormulaConditionBlock.m_OperatorTag.Length);
                    if (txt == Cl_FormulaConditionBlock.m_OperatorTag)
                    {
                        int indexEnd = a_Text.IndexOf(" ");
                        if (indexEnd > -1)
                            txt = a_Text.Substring(Cl_FormulaConditionBlock.m_OperatorTag.Length, indexEnd - Cl_FormulaConditionBlock.m_OperatorTag.Length);
                        else
                            txt = a_Text.Replace(Cl_FormulaConditionBlock.m_OperatorTag, "");

                        Cl_Element element = Cl_RecordsFacade.f_GetInstance().f_GetAgeElement(txt);
                        if (element == null)
                            element = Cl_RecordsFacade.f_GetInstance().f_GetGenderElement(txt);
                        if (element == null)
                            element = a_Elements.FirstOrDefault(el => el.p_Tag == txt);
                        if (element != null)
                            return new Cl_FormulaConditionBlock(element);
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
                        return new Cl_FormulaConditionBlock(iVal);
                    }
                    if (!string.IsNullOrWhiteSpace(txt))
                    {
                        return new Cl_FormulaConditionBlock(txt);
                    }
                }
            }
            return null;
        }

        /// <summary>Получение блоков формулы отображения элемента</summary>
        /// <param name="a_Elements">Список элементов</param>
		/// <param name="a_Formula">Формула</param>
        public Cl_FormulaConditionBlock[] f_GetConditionsBlocks(Cl_Element[] a_Elements, string a_Formula)
        {
            string formula = a_Formula;
            var blocks = new List<Cl_FormulaConditionBlock>();
            while (!string.IsNullOrWhiteSpace(formula))
            {
                Cl_FormulaConditionBlock block = f_GetFirstConditionBlockFromText(a_Elements, formula);
                if (block != null)
                {
                    if (block.p_Object is string)
                    {
                        string txt = block.p_Object.ToString();
                        if (blocks.Count < 2) return null;
                        Cl_Element el = blocks[blocks.Count - 2].p_Object as Cl_Element;
                        if (el == null || el.p_IsNumber) return null;
                        if (el.p_Tag == "gender")
                        {
                            Cl_User.E_Sex gender;
                            if (Enum.TryParse(block.p_Object.ToString(), true, out gender))
                            {
                                block.p_Object = gender;
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            Cl_ElementParam prm = el.p_NormValues.FirstOrDefault(val => val.p_Value == txt);
                            if (prm != null)
                            {
                                block.p_Object = prm;
                            }
                            else
                            {
                                prm = el.p_PatValues.FirstOrDefault(val => val.p_Value == txt);
                                if (prm != null)
                                {
                                    block.p_Object = prm;
                                }
                                else
                                {
                                    return null;
                                }
                            }
                        }
                    }
                    blocks.Add(block);
                    string txtBlock = block.f_GetTextFromBlock();
                    formula = formula.Substring(txtBlock.Length);
                }
                else
                {
                    return null;
                }
            }
            return blocks.ToArray();
        }

        /// <summary>Проверка корректности формулы отображения элемента</summary>
        public bool f_Valid(ICollection<Cl_FormulaConditionBlock> a_Blocks)
        {
            if (a_Blocks.Count == 1)
                return false;
            else
            {
                Cl_FormulaConditionBlock block = a_Blocks.LastOrDefault();
                if (block != null)
                    return !block.p_IsOperand;
                else
                    return true;
            }
        }
        #endregion

        #region Mathematical
        /// <summary>Получение первого блока из текста математического вычисления</summary>
        /// <param name="a_Elements">Список элементов</param>
        /// <param name="a_Text">Текст</param>
        /// <returns></returns>
        private Cl_FormulaMathematicalBlock f_GetFirstMathematicalBlockFromText(Cl_Element[] a_Elements, string a_Text)
        {
            if (!string.IsNullOrWhiteSpace(a_Text))
            {
                string txt = "";
                if (a_Text.Length >= 3)
                {
                    txt = a_Text.Substring(0, 3);
                    if (txt == " + ")
                        return new Cl_FormulaMathematicalBlock(Cl_FormulaMathematicalBlock.E_Opers.plus);
                    else if (txt == " - ")
                        return new Cl_FormulaMathematicalBlock(Cl_FormulaMathematicalBlock.E_Opers.minus);
                    else if (txt == " / ")
                        return new Cl_FormulaMathematicalBlock(Cl_FormulaMathematicalBlock.E_Opers.carve);
                    else if (txt == " * ")
                        return new Cl_FormulaMathematicalBlock(Cl_FormulaMathematicalBlock.E_Opers.multiply);
                }
                if (a_Text.Length > Cl_FormulaMathematicalBlock.m_OperatorTag.Length)
                {
                    txt = a_Text.Substring(0, Cl_FormulaMathematicalBlock.m_OperatorTag.Length);
                    if (txt == Cl_FormulaMathematicalBlock.m_OperatorTag)
                    {
                        int indexEnd = a_Text.IndexOf(" ");
                        if (indexEnd > -1)
                            txt = a_Text.Substring(Cl_FormulaMathematicalBlock.m_OperatorTag.Length, indexEnd - Cl_FormulaMathematicalBlock.m_OperatorTag.Length);
                        else
                            txt = a_Text.Replace(Cl_FormulaMathematicalBlock.m_OperatorTag, "");
                        Cl_Element element = a_Elements.FirstOrDefault(el => el.p_Tag == txt);
                        if (element != null)
                            return new Cl_FormulaMathematicalBlock(element);
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
                        return new Cl_FormulaMathematicalBlock(iVal);
                    }
                }
            }
            return null;
        }

        /// <summary>Получение блоков формулы математического вычисления</summary>
        /// <param name="a_Elements">Список элементов</param>
		/// <param name="a_Formula">Формула</param>
        public Cl_FormulaMathematicalBlock[] f_GetMathematicalsBlocks(Cl_Element[] a_Elements, string a_Formula)
        {
            string formula = a_Formula;
            var blocks = new List<Cl_FormulaMathematicalBlock>();
            while (!string.IsNullOrWhiteSpace(formula))
            {
                Cl_FormulaMathematicalBlock block = f_GetFirstMathematicalBlockFromText(a_Elements, formula);
                if (block != null)
                {
                    string text = block.f_GetTextFromBlock();
                    if (string.IsNullOrWhiteSpace(text)) return null;
                    blocks.Add(block);
                    string txtBlock = block.f_GetTextFromBlock();
                    formula = formula.Substring(txtBlock.Length);
                }
                else
                {
                    return null;
                }
            }
            return blocks.ToArray();
        }

        /// <summary>Проверка корректности формулы математической операции</summary>
        public bool f_Valid(ICollection<Cl_FormulaMathematicalBlock> a_Blocks)
        {
            Cl_FormulaMathematicalBlock block = a_Blocks.LastOrDefault();
            if (block != null)
                return !block.p_IsOperand;
            else
                return true;
        }
        #endregion
    }
}
