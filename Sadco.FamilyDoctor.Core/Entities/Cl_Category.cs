using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sadco.FamilyDoctor.Core.Entities
{

    /// <summary>
    /// Класс сущности категории
    /// </summary>
    [Table("T_CATEGORIES")]
	public class Cl_Category : I_Comparable
    {
        /// <summary>Типы элементов шаблона</summary>
        public enum E_CategoriesTypes : byte
        {
            /// <summary>Общая категория</summary>
            Total,
            /// <summary>Клиническая категория</summary>
            Clinik
        }

        /// <summary>Ключ категории</summary>
        [Key]
		[Column("F_ID")]
		public int p_ID { get; set; }

        /// <summary>Название категории</summary>
        [Column("F_NAME", TypeName = "varchar")]
        [MaxLength(100)]
        public string p_Name { get; set; }

        /// <summary>Тип категории</summary>
        [Column("F_Type")]
        public E_CategoriesTypes p_Type { get; set; }

        /// <summary>Метод сравнения</summary>
        public bool f_Equals(object a_Value)
        {
            if (a_Value == null || !(a_Value.GetType() == this.GetType()))
                return false;
            return p_ID == ((Cl_Category)a_Value).p_ID;
        }

        public override string ToString()
        {
            return p_Name;
        }
    }
}
