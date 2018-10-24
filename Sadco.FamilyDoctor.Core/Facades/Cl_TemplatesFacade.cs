using FD.dat.mon.stb.lib;
using Sadco.FamilyDoctor.Core.Controls.DesignerPanel;
using Sadco.FamilyDoctor.Core.Data;
using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.Core.EntityLogs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Sadco.FamilyDoctor.Core.Facades
{
    /// <summary>
    /// Фасад работы с шаблонами
    /// </summary>
    public class Cl_TemplatesFacade
    {
        private static Cl_TemplatesFacade INSTANCE = new Cl_TemplatesFacade();
        public static Cl_TemplatesFacade f_GetInstance()
        {
            return INSTANCE;
        }

        private bool m_IsInit = false;
        private Cl_DataContextMegaTemplate m_DataContextMegaTemplate = null;

        /// <summary>Инициализация фасада</summary>
        public bool f_Init(Cl_DataContextMegaTemplate a_DataContextMegaTemplate)
        {
            m_DataContextMegaTemplate = a_DataContextMegaTemplate;
            m_IsInit = m_DataContextMegaTemplate != null;
            return m_IsInit;
        }

        /// <summary>Рекурсивная загрузка списка элементов шаблона</summary>
        private void f_RecursiveLoadTE(Cl_Template a_Template)
        {
            if (a_Template != null)
            {
                var tes = m_DataContextMegaTemplate.Entry(a_Template).Collection(d => d.p_TemplateElements);
                if (!tes.IsLoaded) tes.Load();
                if (a_Template.p_TemplateElements != null)
                {
                    foreach (var te in a_Template.p_TemplateElements)
                    {
                        m_DataContextMegaTemplate.Entry(te).Reference(d => d.p_ChildTemplate).Load();
                        f_RecursiveLoadTE(te.p_ChildTemplate);
                        m_DataContextMegaTemplate.Entry(te).Reference(d => d.p_ChildElement).Query().Include(p => p.p_ParamsValues).Include(p => p.p_PartAgeNorms).Load();
                    }
                }
            }
        }
        /// <summary>Загрузка полного списка элементов шаблона</summary>
        public void f_LoadTemplatesElements(Cl_Template a_Template)
        {
            if (a_Template != null)
            {
                if (a_Template.p_TemplateElements == null)
                {
                    var elements = m_DataContextMegaTemplate.p_TemplatesElements.Include(te => te.p_ChildElement).Include(te => te.p_ChildElement.p_ParamsValues).Include(te => te.p_ChildElement.p_PartAgeNorms).Include(te => te.p_ChildTemplate)
                       .Where(t => t.p_TemplateID == a_Template.p_ID).OrderBy(t => t.p_Index).ToArray();
                    foreach (var el in elements)
                    {
                        f_RecursiveLoadTE(el.p_ChildTemplate);
                    }
                }
            }
        }

        /// <summary>Получение полного списка элемента в шаблоне</summary>
        public Cl_Element[] f_GetElements(Cl_Template a_Template)
        {
            var elements = new List<Cl_Element>();
            if (a_Template == null) return elements.ToArray();
            if (a_Template.p_TemplateElements == null)
            {
                f_LoadTemplatesElements(a_Template);
            }
            foreach (var te in a_Template.p_TemplateElements)
            {
                if (te.p_ChildElement != null) elements.Add(te.p_ChildElement);
                elements.AddRange(f_GetElements(te.p_ChildTemplate));
            }
            return elements.ToArray();
        }

        /// <summary>
        /// Возвращает последнюю версию переданного элемента
        /// </summary>
        /// <param name="p_ChildElement">Элемент шаблона</param>
        /// <returns></returns>
        internal Cl_Element f_GetActualElement(Cl_Element p_ChildElement)
        {
            if (p_ChildElement == null) return null;

            Cl_Element els = m_DataContextMegaTemplate.p_Elements
            .Where(e => e.p_ElementID == p_ChildElement.p_ElementID).OrderByDescending(d => d.p_Version).FirstOrDefault();

            if (p_ChildElement.p_ID != els.p_ID)
                return els;

            return p_ChildElement;
        }
        internal bool f_IsActualElement(Cl_Element element)
        {
            if (element == null) return false;

            Cl_Element actElement = this.f_GetActualElement(element);
            return actElement.p_ID == element.p_ID;
        }

        /// <summary>
        /// Возвращает последнюю версию переданного шаблона
        /// </summary>
        /// <param name="p_ChildTemplate">Шаблон</param>
        /// <returns></returns>
        internal Cl_Template f_GetActualTemplate(Cl_Template p_ChildTemplate)
        {
            if (p_ChildTemplate == null) return null;
            Cl_Template tmpl = m_DataContextMegaTemplate.p_Templates.Where(e => e.p_TemplateID == p_ChildTemplate.p_TemplateID).OrderByDescending(d => d.p_Version).FirstOrDefault();
            if (tmpl != null && p_ChildTemplate.p_ID != tmpl.p_ID)
                return tmpl;
            return p_ChildTemplate;
        }

        /// <summary>
        /// Возвращает шаблон
        /// </summary>
        /// <param name="a_TemplateID">ID шаблона</param>
        /// <returns></returns>
        public Cl_Template f_GetTemplate(int a_TemplateID)
        {
            Cl_Template tmpl = m_DataContextMegaTemplate.p_Templates.FirstOrDefault(t => t.p_ID == a_TemplateID);
            if (tmpl != null) f_LoadTemplatesElements(tmpl);
            return tmpl;
        }

        /// <summary>
        /// Возвращает шаблон
        /// </summary>
        /// <param name="a_TemplateName">Название шаблона</param>
        /// <returns></returns>
        public Cl_Template f_GetTemplateByName(string a_TemplateName)
        {
            Cl_Template tmpl = m_DataContextMegaTemplate.p_Templates.FirstOrDefault(t => t.p_Name == a_TemplateName);
            if (tmpl != null) f_LoadTemplatesElements(tmpl);
            return tmpl;
        }

        internal bool f_IsActualElementsOnTemplate(Cl_Template template)
        {
            bool defNewElements = false;
            bool defNewTemplate = false;

            if (template == null) return false;

            Cl_Template actTemplate = this.f_GetActualTemplate(template);
            defNewTemplate = actTemplate.p_ID != template.p_ID;

            if (template.p_TemplateElements == null || template.p_TemplateElements.Count == 0)
                return true;

            foreach (Cl_TemplateElement item in template.p_TemplateElements)
            {
                if (item.p_ChildElement != null)
                {
                    if (this.f_IsActualElement(item.p_ChildElement) == false)
                    {
                        defNewElements = true;
                        break;
                    }
                }

                if (item.p_ChildTemplate != null)
                    return this.f_IsActualElementsOnTemplate(item.p_ChildTemplate);
            }

            return defNewTemplate == false && defNewElements == false;
        }


        /// <summary>
        /// Сохранение шаблона
        /// </summary>
        /// <param name="curTemplate">Сохраняемый шаблон</param>
        /// <param name="items">Новый список элементов в сохраняемом шаблоне</param>
        /// <param name="m_Log">Объект логгера</param>
        /// <returns></returns>
        public Cl_Template f_SaveTemplate(Cl_Template curTemplate, I_Element[] elements, Cl_EntityLog m_Log = null)
        {
            using (var transaction = m_DataContextMegaTemplate.Database.BeginTransaction())
            {
                try
                {
                    Cl_Template newTemplate = null;
                    if (curTemplate.p_Version == 0)
                    {
                        newTemplate = curTemplate;
                        newTemplate.p_Version = 1;
                    }
                    else
                    {
                        newTemplate = new Cl_Template();
                        newTemplate.p_TemplateID = curTemplate.p_TemplateID;
                        newTemplate.p_Title = curTemplate.p_Title;
                        newTemplate.p_CategoryTotalID = curTemplate.p_CategoryTotalID;
                        newTemplate.p_CategoryTotal = curTemplate.p_CategoryTotal;
                        newTemplate.p_CategoryClinicID = curTemplate.p_CategoryClinicID;
                        newTemplate.p_CategoryClinic = curTemplate.p_CategoryClinic;
                        newTemplate.p_Type = curTemplate.p_Type;
                        newTemplate.p_Name = curTemplate.p_Name;
                        newTemplate.p_Version = curTemplate.p_Version + 1;
                        newTemplate.p_ParentGroupID = curTemplate.p_ParentGroupID;
                        newTemplate.p_ParentGroup = curTemplate.p_ParentGroup;
                        newTemplate.p_Description = curTemplate.p_Description;

                        m_DataContextMegaTemplate.p_Templates.Add(newTemplate);
                    }

                    m_DataContextMegaTemplate.SaveChanges();

                    foreach (I_Element item in elements)
                    {
                        Cl_TemplateElement tplEl = new Cl_TemplateElement();
                        tplEl.p_TemplateID = newTemplate.p_ID;
                        tplEl.p_Template = newTemplate;
                        if (item is Ctrl_Element)
                        {
                            Ctrl_Element block = (Ctrl_Element)item;
                            tplEl.p_ChildElementID = block.p_ID;
                            tplEl.p_ChildElement = block.p_Element;
                        }
                        else if (item is Ctrl_Template)
                        {
                            Ctrl_Template block = (Ctrl_Template)item;
                            tplEl.p_ChildTemplateID = block.p_ID;
                            tplEl.p_ChildTemplate = block.p_Template;
                        }

                        tplEl.p_Index = Array.IndexOf(elements, item) + 1;

                        m_DataContextMegaTemplate.p_TemplatesElements.Add(tplEl);
                    }

                    m_DataContextMegaTemplate.SaveChanges();

                    if (m_Log != null && m_Log.f_IsChanged(newTemplate) == false)
                    {
                        if (newTemplate.Equals(curTemplate) && newTemplate.p_Version == 1)
                        {
                            newTemplate.p_Version = 0;
                        }

                        MonitoringStub.Message("Шаблон не изменялся!");
                        transaction.Rollback();
                    }
                    else
                    {
                        m_Log.f_SaveEntity(newTemplate);
                        transaction.Commit();

                        return newTemplate;
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MonitoringStub.Error("Error_Editor", "При сохранении изменений произошла ошибка", ex, null, null);
                }

                return curTemplate;
            }
        }

        public Cl_Template f_UpSaveTemplate(Cl_Template curTemplate, I_Element[] elements, Cl_EntityLog m_Log)
        {
            Cl_Template newTemplate = curTemplate;

            if (this.f_IsActualElementsOnTemplate(curTemplate))
                return f_SaveTemplate(curTemplate, elements, m_Log);

            using (var transaction = m_DataContextMegaTemplate.Database.BeginTransaction())
            {
                try
                {
                    newTemplate = f_UpdateTemplate(newTemplate);

                    m_Log.f_SaveEntity(newTemplate);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MonitoringStub.Error("Error_Editor", "При сохранении изменений произошла ошибка", ex, null, null);
                }
            }

            return newTemplate;
        }

        private Cl_Template f_UpdateTemplate(Cl_Template template)
        {
            Cl_Template defNewTemplate = this.f_GetActualTemplate(template);
            if (defNewTemplate.p_Version > template.p_Version)
                return defNewTemplate;

            if (template.p_TemplateElements == null || template.p_TemplateElements.Count == 0)
                return template;

            Cl_Template newTemplate = new Cl_Template();
            newTemplate.p_TemplateID = template.p_TemplateID;
            newTemplate.p_Title = template.p_Title;
            newTemplate.p_CategoryTotalID = template.p_CategoryTotalID;
            newTemplate.p_CategoryTotal = template.p_CategoryTotal;
            newTemplate.p_CategoryClinicID = template.p_CategoryClinicID;
            newTemplate.p_CategoryClinic = template.p_CategoryClinic;
            newTemplate.p_Type = template.p_Type;
            newTemplate.p_Name = template.p_Name;
            newTemplate.p_Version = template.p_Version + 1;
            newTemplate.p_ParentGroupID = template.p_ParentGroupID;
            newTemplate.p_ParentGroup = template.p_ParentGroup;
            newTemplate.p_Description = template.p_Description;

            m_DataContextMegaTemplate.p_Templates.Add(newTemplate);
            m_DataContextMegaTemplate.SaveChanges();

            int i = 0;
            foreach (Cl_TemplateElement item in template.p_TemplateElements)
            {
                Cl_TemplateElement tplEl = new Cl_TemplateElement();
                tplEl.p_Template = newTemplate;

                if (item.p_ChildElement != null)
                {
                    tplEl.p_ChildElement = this.f_GetActualElement(item.p_ChildElement);
                }
                else if (item.p_ChildTemplate != null)
                {
                    tplEl.p_ChildTemplate = this.f_UpdateTemplate(item.p_ChildTemplate);

                    if (this.f_IsActualElementsOnTemplate(item.p_ChildTemplate) == false)
                    {
                        tplEl.p_ChildTemplate = this.f_UpdateTemplate(tplEl.p_ChildTemplate);
                    }
                }

                tplEl.p_Index = i = ++i;
                m_DataContextMegaTemplate.p_TemplatesElements.Add(tplEl);
            }

            m_DataContextMegaTemplate.SaveChanges();

            return newTemplate;
        }
    }
}
