using Sadco.FamilyDoctor.Core.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Sadco.FamilyDoctor.Core.Formula
{
    /// <summary>
    /// Класс блока математического вычисления
    /// </summary>
    public class Cl_FormulaMathematicalBlock
    {
        /// <summary>
        /// Операциии блока условия
        /// </summary>
        public enum E_Opers
        {
            plus,
            minus,
            carve,
            multiply
        }

        public Cl_FormulaMathematicalBlock(object a_Object)
        {
            p_Object = a_Object;
        }

        public object p_Object { get; set; }
        public const string m_OperatorTag = "tag_";

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
}
