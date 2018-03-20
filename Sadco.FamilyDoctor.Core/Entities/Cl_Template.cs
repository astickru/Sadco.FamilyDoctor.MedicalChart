using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace Sadco.FamilyDoctor.Core.Entities
{
    /// <summary>
    /// Класс шаблона
    /// </summary>
    [Table("T_TEMPLATES")]
    public class Cl_Template : I_Version, I_Archive
    {
        /// <summary>ID шаблона</summary>
        [Column("F_ID")]
        [Key]
        public int p_ID { get; set; }

        /// <summary>ID шаблона для всех версий</summary>
        [Column("F_TEMPLATE_ID")]
        public int p_TemplateID { get; set; }

        /// <summary>ID группы шаблонов</summary>
        [Column("F_GROUP_ID")]
        [ForeignKey("p_ParentGroup")]
        public int p_ParentGroupID { get; set; }
        /// <summary>Группа шаблонов</summary>
        public Cl_Group p_ParentGroup { get; set; }

        /// <summary>Системное имя шаблона</summary>
        [Column("F_NAME", TypeName ="varchar")]
        [MaxLength(100)]
        public string p_Name { get; set; }

        /// <summary>Описание шаблона</summary>
        [Column("F_DESC", TypeName = "varchar")]
        [MaxLength(1000)]
        public string p_Description { get; set; }

        /// <summary>Возвращает список элемента шаблона</summary>
        [ForeignKey("p_TemplateID")]
        public ICollection<Cl_TemplatesElements> p_TemplateControls { get; set; }

        /// <summary>Версия шаблона</summary>
        [Column("F_VERSION")]
        public int p_Version { get; set; }

        /// <summary>Флаг нахождения шаблона в архиве</summary>
        [Column("F_ISARHIVE")]
        public bool p_IsArhive { get; set; }

        /// <summary>Системное наименование иконки</summary>
        public string p_IconName { get { return "TEMPLATE_16"; } }
    }
}
