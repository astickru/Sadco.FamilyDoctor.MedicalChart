using Sadco.FamilyDoctor.Core.EntityLogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sadco.FamilyDoctor.Core.Entities
{
    /// <summary>
    /// Класс шаблона
    /// </summary>
    [Cl_ELogClass(E_EntityTypes.Templates)]
    [Table("T_TEMPLATES")]
    public class Cl_Template : I_Version, I_Delete, I_ELog
    {
        /// <summary>Типы шаблонов</summary>
        public enum E_TemplateType : byte
        {
            /// <summary>Шаблон</summary>
            [Description("Шаблон")]
            Template,
            /// <summary>Блок</summary>
            [Description("Блок")]
            Block,
            /// <summary>Таблица</summary>
            [Description("Таблица")]
            Table
        }

        /// <summary>
        /// Возвращает название типа шаблона
        /// </summary>
        public string p_GetTypeName {
            get {
                return (Attribute.GetCustomAttribute(p_Type.GetType().GetField(p_Type.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description;
            }
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
        [Cl_ELogProperty("Изменилась группа", p_IsCustomDescription = true)]
        public Cl_Group p_ParentGroup { get; set; }

        /// <summary>Типы шаблона</summary>
        [Column("F_TYPE")]
        public E_TemplateType p_Type { get; set; }

        /// <summary>Системное имя шаблона</summary>
        [Column("F_NAME", TypeName = "varchar")]
        [MaxLength(100)]
        [Cl_ELogProperty("Название шаблона")]
        public string p_Name { get; set; }

        /// <summary>Заголовок шаблона</summary>
        [Column("F_TITLE", TypeName = "varchar")]
        [MaxLength(100)]
        [Cl_ELogProperty("Заголовок шаблона")]
        public string p_Title { get; set; }

        /// <summary>ID общей категории</summary>
        [Column("F_CATEGORYTOTAL_ID")]
        [ForeignKey("p_CategoryTotal")]
        public int? p_CategoryTotalID { get; set; }
        private Cl_Category m_CategoryTotal = null;
        /// <summary>Общая категория шаблонов</summary>
        [Cl_ELogProperty("Изменилась общая категория", p_IsCustomDescription = true)]
        public Cl_Category p_CategoryTotal {
            get { return m_CategoryTotal; }
            set {
                m_CategoryTotal = value;
                if (m_CategoryTotal != null)
                    m_CategoryTotal.p_Type = Cl_Category.E_CategoriesTypes.Total;
            }
        }

        /// <summary>ID клинической категории</summary>
        [Column("F_CATEGORYCLINIC_ID")]
        [ForeignKey("p_CategoryClinic")]
        public int? p_CategoryClinicID { get; set; }
        private Cl_Category m_CategoryClinic = null;
        /// <summary>Клиническая категория шаблонов</summary>
        [Cl_ELogProperty("Изменилась клиническая категория", p_IsCustomDescription = true)]
        public Cl_Category p_CategoryClinic {
            get { return m_CategoryClinic; }
            set {
                m_CategoryClinic = value;
                if (m_CategoryClinic != null)
                    m_CategoryClinic.p_Type = Cl_Category.E_CategoriesTypes.Clinic;
            }
        }

        /// <summary>Описание шаблона</summary>
        [Column("F_DESC", TypeName = "varchar")]
        [MaxLength(1000)]
        [Cl_ELogProperty("Описание")]
        public string p_Description { get; set; }

        /// <summary>Возвращает список элементов шаблона</summary>
        [ForeignKey("p_TemplateID")]
        [Cl_ELogProperty("Изменился набор элементов шаблона", p_IsCustomDescription = true, p_IsNewValueOnly = true)]
        public ICollection<Cl_TemplateElement> p_TemplateElements { get; set; }

        /// <summary>Версия шаблона</summary>
        [Column("F_VERSION")]
        public int p_Version { get; set; }

        /// <summary>Конфликт в элементах шаблона</summary>
        [Column("F_ISCONFLICT")]
        public bool p_IsConflict { get; set; }

        /// <summary>Флаг нахождения шаблона в удалении</summary>
        [Column("F_ISDEL")]
        [Cl_ELogProperty("Шаблон удален", p_IsCustomDescription = true, p_IgnoreValue = true)]
        public bool p_IsDelete { get; set; }

        /// <summary>Количество столбцов</summary>
        /// <summary>Версия шаблона</summary>
        [Column("F_COUNTCOLUMN")]
        [Cl_ELogProperty("Количество столбцов")]
        public int p_CountColumn { get; set; } = 2;

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

        /// <summary>Возвращает уникальный ID элемента</summary>
        int I_ELog.p_GetLogEntityID => this.p_TemplateID;

        /// <summary>Проверка наличия элемента</summary>
        private Cl_Element f_HasElement(ICollection<Cl_TemplateElement> a_TemplateElements, Cl_Element a_Element)
        {
            if (a_TemplateElements != null)
            {
                foreach (var te in a_TemplateElements)
                {
                    if (!te.p_ChildElement.p_IsHeader && te.p_ChildElement == a_Element)
                        return a_Element;
                    if (te.p_ChildTemplate != null)
                    {
                        if (f_HasElement(te.p_ChildTemplate.p_TemplateElements, a_Element) != null)
                        {
                            return a_Element;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>Проверка наличия элемента</summary>
        private Cl_Element f_HasElement(ICollection<Cl_TemplateElement> a_TemplateElements, Cl_Template a_Template)
        {
            if (a_TemplateElements != null)
            {
                foreach (var te in a_TemplateElements)
                {
                    if (a_Template.f_HasElement(te.p_ChildElement) != null)
                        return te.p_ChildElement;
                    if (te.p_ChildTemplate != null)
                    {
                        var el = f_HasElement(te.p_ChildTemplate.p_TemplateElements, a_Template);
                        if (el != null)
                        {
                            return el;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>Проверка наличия элемента</summary>
        public Cl_Element f_HasElement(Cl_Element a_Element)
        {
            return f_HasElement(p_TemplateElements, a_Element);
        }

        /// <summary>Проверка наличия элемента</summary>
        public Cl_Element f_HasElement(Cl_Template a_Template)
        {
            return f_HasElement(p_TemplateElements, a_Template);
        }

        public override string ToString()
        {
            return p_Name;
        }
    }
}
