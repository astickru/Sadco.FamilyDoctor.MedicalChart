using Sadco.FamilyDoctor.Core.Entities;
using System.Drawing;

namespace Sadco.FamilyDoctor.Core.Formula
{
    /// <summary>
    /// Класс блока условия
    /// </summary>
    public class Cl_FormulaConditionBlock
    {
        /// <summary>
        /// Операциии блока условия
        /// </summary>
        public enum E_Opers
        {
            /// <summary>Больше</summary>
            more,
            /// <summary>Меньше</summary>
            less,
            /// <summary>Равно</summary>
            equals,
            /// <summary>Не равно</summary>
            notEquals,
            /// <summary>И</summary>
            and,
            /// <summary>ИЛИ</summary>
            or
        }

        public Cl_FormulaConditionBlock(object a_Object)
        {
            p_Object = a_Object;
        }

        public object p_Object { get; set; }
        public const string m_OperatorTag = "tag_";

        /// <summary>Является ли блок операндом</summary>
        public bool p_IsOperand {
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
            else if (p_Object is Cl_ElementParam)
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
            else if (p_Object is Cl_ElementParam)
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
}
