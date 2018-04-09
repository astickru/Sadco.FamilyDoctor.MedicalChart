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
        /// <summary>Типы шаблонов</summary>
        public enum E_TemplateType : byte
        {
            /// <summary>Шаблон</summary>
            Template,
            /// <summary>Блок</summary>
            Block,
            /// <summary>Таблица</summary>
            Table
        }

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

        /// <summary>Типы шаблона</summary>
        [Column("F_TYPE")]
        public E_TemplateType p_Type { get; set; }

        /// <summary>Системное имя шаблона</summary>
        [Column("F_NAME", TypeName = "varchar")]
        [MaxLength(100)]
        public string p_Name { get; set; }

        /// <summary>Описание шаблона</summary>
        [Column("F_DESC", TypeName = "varchar")]
        [MaxLength(1000)]
        public string p_Description { get; set; }

        /// <summary>Возвращает список элементов шаблона</summary>
        [ForeignKey("p_TemplateID")]
        public ICollection<Cl_TemplateElement> p_TemplateElements { get; set; }

        /// <summary>Версия шаблона</summary>
        [Column("F_VERSION")]
        public int p_Version { get; set; }

        /// <summary>Конфликт в элементах шаблона</summary>
        [Column("F_ISCONFLICT")]
        public bool p_IsConflict { get; set; }

        /// <summary>Флаг нахождения шаблона в архиве</summary>
        [Column("F_ISARHIVE")]
        public bool p_IsArhive { get; set; }

        /// <summary>Системное наименование иконки</summary>
        public string p_IconName {
            get {
                if (p_Type == E_TemplateType.Template)
                    return "TEMPLATE_16";
                else if (p_Type == E_TemplateType.Block)
                    return "BLOCK_16";
                else if (p_Type == E_TemplateType.Table)
                    return "TABLE_16";
                return "TEMPLATE_16";
            }
        }

        /// <summary>Проверка наличия элемента</summary>
        private bool f_HasElement(ICollection<Cl_TemplateElement> a_TemplateElements, Cl_Element a_Element)
        {
            if (a_TemplateElements != null)
            {
                foreach (var te in a_TemplateElements)
                {
                    if (te.p_ChildElement == a_Element)
                        return true;
                    if (te.p_ChildTemplate != null)
                    {
                        if (f_HasElement(te.p_ChildTemplate.p_TemplateElements, a_Element))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>Проверка наличия элемента</summary>
        private bool f_HasElement(ICollection<Cl_TemplateElement> a_TemplateElements, Cl_Template a_Template)
        {
            if (a_TemplateElements != null)
            {
                foreach (var te in a_TemplateElements)
                {
                    if (a_Template.f_HasElement(te.p_ChildElement))
                        return true;
                    if (te.p_ChildTemplate != null)
                    {
                        if (f_HasElement(te.p_ChildTemplate.p_TemplateElements, a_Template))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>Проверка наличия элемента</summary>
        public bool f_HasElement(Cl_Element a_Element)
        {
            return f_HasElement(p_TemplateElements, a_Element);
        }

        /// <summary>Проверка наличия элемента</summary>
        public bool f_HasElement(Cl_Template a_Template)
        {
            return f_HasElement(p_TemplateElements, a_Template);
        }
    }
}
