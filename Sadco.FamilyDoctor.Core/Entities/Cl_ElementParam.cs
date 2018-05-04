using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sadco.FamilyDoctor.Core.Entities
{
    /// <summary>Класс параметров текстовых элементов шаблона</summary>
    [Table("T_ELEMENTSPRMS")]
    public class Cl_ElementParam : I_Comparable
    {
        /// <summary>Тип параметра</summary>
        public enum E_TypeParam : byte
        {
            /// <summary>Локация</summary>
            [Description("Локация")]
            Location,
            /// <summary>Нормальные значения</summary>
            [Description("Нормальные значения")]
            NormValues,
            /// <summary>Патологические значения</summary>
            [Description("Патологические значения")]
            PatValues
        }

        /// <summary>ID параметров элементов шаблона</summary>
        [Key]
        [Column("F_ID")]
        public int p_ID { get; set; }

        /// <summary>ID элемента</summary>
        [Column("F_ELEMENT_ID")]
        [ForeignKey("p_Element")]
        public int p_ElementID { get; set; }
        /// <summary>Элемент</summary>
        public Cl_Element p_Element { get; set; }

        /// <summary>Тип параметров элементов шаблона</summary>
        [Column("F_TYPEPARAM")]
        public E_TypeParam p_TypeParam { get; set; }

        /// <summary>Значение параметров элементов шаблона</summary>
        [Column("F_VALUE")]
        public string p_Value { get; set; }

        public override string ToString()
        {
            return p_Value;
        }

        /// <summary>Метод сравнения</summary>
        public bool f_Equals(object a_Value)
        {
            if (a_Value == null || !(a_Value is Cl_ElementParam))
                return false;
            return p_Value == ((Cl_ElementParam)a_Value).p_Value;
        }
    }
}
