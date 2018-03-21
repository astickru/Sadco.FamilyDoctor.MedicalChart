using Sadco.FamilyDoctor.Core.Controls;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sadco.FamilyDoctor.Core.Entities
{
    /// <summary>
    /// Класс сущности групп
    /// </summary>
    [Table("T_GROUPS")]
    public class Cl_Group : I_Archive
    {
        /// <summary>
        /// Тип групп
        /// </summary>
        public enum E_Type : byte
        {
            Elements,
            Templates
        }

        public Cl_Group()
        {
            p_SubGroups = new List<Cl_Group>();
        }

        /// <summary>ID группы</summary>
        [Key]
        [Column("F_ID")]
        public int p_ID { get; set; }

        /// <summary>Тип группы</summary>
		[Column("F_TYPE")]
        public E_Type p_Type { get; set; }

        /// <summary>Название группы</summary>
		[Column("F_NAME", TypeName = "varchar")]
        [MaxLength(100)]
        public string p_Name { get; set; }

        /// <summary>ID родительской группы</summary>
		[Column("F_PARENT_ID")]
        [ForeignKey("p_Parent")]
        public int? p_ParentID { get; set; }
        /// <summary>Родительская группа</summary>
        public Cl_Group p_Parent { get; set; }

        /// <summary>Возвращает список вложенных групп</summary>
        [ForeignKey("p_ParentID")]
        public virtual ICollection<Cl_Group> p_SubGroups { get; set; }

        /// <summary>Флаг нахождения группы элементов в архиве</summary>
        [Column("F_ISARHIVE")]
        public bool p_IsArhive { get; set; }

        /// <summary>Возвращает имя родительской группы</summary>
        public string f_GetParentName(Cl_Group parent)
        {
            string name = parent.p_Name;
            if (parent.p_Parent != null)
            {
                name = f_GetParentName(parent.p_Parent) + "/" + name;
            }
            return name;
        }

        /// <summary>Возвращает полный путь родительской группы</summary>
		public string f_GetFullName()
        {
            if (p_Parent != null)
            {
                return f_GetParentName(p_Parent) + "/" + p_Name;
            }
            return p_Name;
        }
    }
}
