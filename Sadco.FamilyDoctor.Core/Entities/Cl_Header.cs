using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sadco.FamilyDoctor.Core.Entities
{
    /// <summary>
    /// Класс элемента заголовка
    /// </summary>
    public class Cl_Header
    {
        /// <summary>Элемент заголовка</summary>
        public Cl_Element p_Element { get; set; }

        /// <summary>Возвращает уровень заголовка</summary>
        public int p_HeaderLevel {
            get {
                return p_Element?.p_IsHeader == true ? int.Parse(p_Element.p_Name.Substring(10)) : 0;
            }
        }

        /// <summary>Вид заголовка</summary>
        public E_Headers p_Type {
            get {
                return (E_Headers)p_HeaderLevel;
            }
        }

        /// <summary>Текст заголовка</summary>
        public string p_Text { get; set; }

        public override string ToString()
        {
            if (p_Type == E_Headers.Header_1)
                return "H1. " + p_Text;
            else if (p_Type == E_Headers.Header_2)
                return "H2. " + p_Text;
            else if (p_Type == E_Headers.Header_3)
                return "H3. " + p_Text;
            else if (p_Type == E_Headers.Header_4)
                return "H4. " + p_Text;
            else if (p_Type == E_Headers.Header_5)
                return "H5. " + p_Text;
            else if (p_Type == E_Headers.Header_6)
                return "H6. " + p_Text;
            else
                return "H1. " + p_Text;
        }
    }
}
