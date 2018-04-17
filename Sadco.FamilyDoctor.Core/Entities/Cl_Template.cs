﻿using Sadco.FamilyDoctor.Core.EntityLogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Sadco.FamilyDoctor.Core.Entities
{
    /// <summary>
    /// Класс шаблона
    /// </summary>
    [ELogClass(EntityTypes.Templates)]
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
        /// Возвращает название элемента шаблона
        /// </summary>
        public string p_GetElementName {
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
        [ELogProperty("Изменилась группа", IsCustomDescription = true)]
        public Cl_Group p_ParentGroup { get; set; }

        /// <summary>Типы шаблона</summary>
        [Column("F_TYPE")]
        public E_TemplateType p_Type { get; set; }

        /// <summary>Системное имя шаблона</summary>
        [Column("F_NAME", TypeName = "varchar")]
        [MaxLength(100)]
        [ELogProperty("Название шаблона")]
        public string p_Name { get; set; }

        /// <summary>Описание шаблона</summary>
        [Column("F_DESC", TypeName = "varchar")]
        [MaxLength(1000)]
        [ELogProperty("Описание")]
        public string p_Description { get; set; }

        /// <summary>Возвращает список элементов шаблона</summary>
        [ForeignKey("p_TemplateID")]
        [ELogProperty("Изменился набор элементов шаблона", IsCustomDescription = true, IsNewValueOnly = true)]
        public ICollection<Cl_TemplateElement> p_TemplateElements { get; set; }

        /// <summary>Версия шаблона</summary>
        [Column("F_VERSION")]
        public int p_Version { get; set; }

        /// <summary>Конфликт в элементах шаблона</summary>
        [Column("F_ISCONFLICT")]
        public bool p_IsConflict { get; set; }

        /// <summary>Флаг нахождения шаблона в удалении</summary>
        [Column("F_ISDEL")]
        [ELogProperty("Шаблон удален", IsCustomDescription = true, IgnoreValue = true)]
        public bool p_IsDelete { get; set; }

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

        /// <summary>Рекурсивная загрузка списка элементов шаблона</summary>
        private void f_RecursiveLoadTE(Cl_Template a_Template)
        {
            if (a_Template != null)
            {
                var tes = Cl_App.m_DataContext.Entry(a_Template).Collection(d => d.p_TemplateElements);
                if (!tes.IsLoaded) tes.Load();
                if (a_Template.p_TemplateElements != null)
                {
                    foreach (var te in a_Template.p_TemplateElements)
                    {
                        Cl_App.m_DataContext.Entry(te).Reference(d => d.p_ChildTemplate).Load();
                        f_RecursiveLoadTE(te.p_ChildTemplate);
                        Cl_App.m_DataContext.Entry(te).Reference(d => d.p_ChildElement).Query().Include(p => p.p_ParamsValues).Load();
                    }
                }
            }
        }
        /// <summary>Загрузка полного списка элементов шаблона</summary>
        public void f_LoadTemplatesElements()
        {
            var elements = Cl_App.m_DataContext.p_TemplatesElements.Include(te => te.p_ChildElement).Include(te => te.p_ChildElement.p_ParamsValues).Include(te => te.p_ChildTemplate)
               .Where(t => t.p_TemplateID == p_ID).OrderBy(t => t.p_Index).ToArray();
            foreach (var el in elements)
            {
                f_RecursiveLoadTE(el.p_ChildTemplate);
            }
        }
    }
}
