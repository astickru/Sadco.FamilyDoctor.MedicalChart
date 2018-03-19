using Sadco.FamilyDoctor.Core.Controls;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sadco.FamilyDoctor.Core.Entities
{
    /// <summary>
    /// Класс сущности группы шаблонов
    /// </summary>
    [Table("T_GROUPS_TEMPLATES")]
	public class Cl_GroupTemplate : I_Group, I_Archive
    {
		public Cl_GroupTemplate() {
			p_SubGroups = new List<Cl_GroupTemplate>();
		}

        /// <summary>ID группы</summary>
		[Column("F_ID")]
        [Key]
		public int p_ID { get; set; }

        /// <summary>Название группы</summary>
		[Column("F_NAME", TypeName = "varchar")]
        [MaxLength(100)]
        public string p_Name { get; set; }

        /// <summary>ID родительской группы</summary>
		[Column("F_PARENT_ID")]
		public int? p_ParentID { get; set; }
        /// <summary>Родительская группа</summary>
		public Cl_GroupTemplate p_Parent { get; set; }

        /// <summary>Возвращает список вложенных групп</summary>
		[ForeignKey("p_ParentID")]
        public virtual ICollection<Cl_GroupTemplate> p_SubGroups { get; set; }

        /// <summary>Возвращает список шаблонов</summary>
        [ForeignKey("p_ID")]
        public virtual ICollection<Cl_Template> cl_Templates { get; set; }

        /// <summary>Флаг нахождения группы шаблонов в архиве</summary>
        [Column("F_ISARHIVE")]
        public bool p_IsArhive { get; set; }

        /// <summary>Возвращает имя родительской группы</summary>
        public string f_GetParentName<T>(T parent) where T : I_Group {
			Cl_GroupTemplate parentObj = parent as Cl_GroupTemplate;

			string name = parent.p_Name;
			if (parentObj.p_Parent != null) {
				name = f_GetParentName(parentObj.p_Parent) + "/" + name;
			}
			return name;
		}

        /// <summary>Возвращает полный путь родительской группы</summary>
		public string f_GetFullName() {
			if (p_Parent != null) {
				return f_GetParentName(p_Parent) + "/" + p_Name;
			}
			return p_Name;
		}
	}
}
