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
        private void f_RecursiveLoadTE(Cl_Template a_Template, bool a_Reload)
        {
            if (a_Template != null)
            {
                var tes = m_DataContextMegaTemplate.Entry(a_Template).Collection(d => d.p_TemplateElements);
                if (!tes.IsLoaded) tes.Load();
                if (a_Reload || a_Template.p_TemplateElements != null)
                {
                    foreach (var te in a_Template.p_TemplateElements)
                    {
                        m_DataContextMegaTemplate.Entry(te).Reference(d => d.p_ChildTemplate).Load();
                        f_RecursiveLoadTE(te.p_ChildTemplate, a_Reload);
                        m_DataContextMegaTemplate.Entry(te).Reference(d => d.p_ChildElement).Query().Include(p => p.p_ParamsValues).Include(p => p.p_PartAgeNorms).Load();
                    }
                }
            }
        }
        /// <summary>Загрузка полного списка элементов шаблона</summary>
        public void f_LoadTemplatesElements(Cl_Template a_Template, bool a_Reload = false)
        {
            if (a_Template != null)
            {
                if (a_Reload || a_Template.p_TemplateElements == null)
                {
                    var elements = m_DataContextMegaTemplate.p_TemplatesElements.Include(te => te.p_ChildElement).Include(te => te.p_ChildElement.p_ParamsValues).Include(te => te.p_ChildElement.p_PartAgeNorms).Include(te => te.p_ChildTemplate)
                       .Where(t => t.p_TemplateID == a_Template.p_ID).OrderBy(t => t.p_Index).ToArray();
                    foreach (var el in elements)
                    {
                        f_RecursiveLoadTE(el.p_ChildTemplate, a_Reload);
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

        /// <summary>Возвращает последнюю версию переданного элемента</summary>
        /// <param name="a_Element">Элемент шаблона</param>
        internal Cl_Element f_GetLastVersionElement(Cl_Element a_Element)
        {
            if (a_Element == null) return null;
            var el = m_DataContextMegaTemplate.p_Elements.Where(e => e.p_ElementID == a_Element.p_ElementID).OrderByDescending(d => d.p_Version).FirstOrDefault();
            if (a_Element.p_ID != el.p_ID)
                return el;
            return a_Element;
        }

        /// <summary>Возвращает последнюю версию переданного шаблона</summary>
        /// <param name="a_Template">Шаблон</param>
        internal Cl_Template f_GetLastVersionTemplate(Cl_Template a_Template)
        {
            if (a_Template == null) return null;
            var templ = m_DataContextMegaTemplate.p_Templates.Where(e => e.p_TemplateID == a_Template.p_TemplateID).OrderByDescending(d => d.p_Version).FirstOrDefault();
            if (a_Template.p_ID != templ.p_ID)
                return templ;
            return a_Template;
        }

        /// <summary>Возвращает актуальный шаблон</summary>
        /// <param name="a_Template">Шаблон</param>
        private Cl_Template f_GetActualTemplate(Cl_Template a_Template)
        {
            if (a_Template == null) return null;
            var template = f_GetLastVersionTemplate(a_Template);
            if (template != null) f_LoadTemplatesElements(template);
            if (template.p_ID != a_Template.p_ID && f_IsActualElementsOnTemplate(template))
            {
                return template;
            }
            if (template.p_TemplateElements == null || template.p_TemplateElements.Count == 0)
                return a_Template;
            Cl_Template newTemplate = null;
            var oldTemplateElements = new List<Cl_TemplateElement>();
            var newTemplateElements = new List<Cl_TemplateElement>();
            foreach (var item in a_Template.p_TemplateElements)
            {
                void initNewTemplate()
                {
                    if (newTemplate == null)
                    {
                        newTemplate = new Cl_Template();
                        newTemplate.p_TemplateID = item.p_Template.p_TemplateID;
                        newTemplate.p_Title = item.p_Template.p_Title;
                        newTemplate.p_CategoryTotalID = item.p_Template.p_CategoryTotalID;
                        newTemplate.p_CategoryTotal = item.p_Template.p_CategoryTotal;
                        newTemplate.p_CategoryClinicID = item.p_Template.p_CategoryClinicID;
                        newTemplate.p_CategoryClinic = item.p_Template.p_CategoryClinic;
                        newTemplate.p_Type = item.p_Template.p_Type;
                        newTemplate.p_Name = item.p_Template.p_Name;
                        newTemplate.p_Version = item.p_Template.p_Version + 1;
                        newTemplate.p_ParentGroup = item.p_Template.p_ParentGroup;
                        newTemplate.p_Description = item.p_Template.p_Description;
                        newTemplate.p_ParentGroupID = item.p_Template.p_ParentGroupID;
                    }
                }

                if (item.p_ChildElement != null)
                {
                    var el = f_GetLastVersionElement(item.p_ChildElement);
                    if (el.p_ID != item.p_ChildElement.p_ID)
                    {
                        initNewTemplate();
                        var tplEl = new Cl_TemplateElement();
                        tplEl.p_TemplateID = newTemplate.p_ID;
                        tplEl.p_Template = newTemplate;
                        tplEl.p_ChildElementID = el.p_ID;
                        tplEl.p_ChildElement = el;
                        tplEl.p_Index = item.p_Index;
                        tplEl.p_Value = item.p_Value;
                        newTemplateElements.Add(tplEl);
                    }
                    else
                        oldTemplateElements.Add(item);
                }
                else if (item.p_ChildTemplate != null)
                {
                    var tmpl = f_GetActualTemplate(item.p_ChildTemplate);
                    if (tmpl.p_ID != item.p_ChildTemplate.p_ID)
                    {
                        initNewTemplate();
                        var tplEl = new Cl_TemplateElement();
                        tplEl.p_TemplateID = newTemplate.p_ID;
                        tplEl.p_Template = newTemplate;
                        tplEl.p_ChildTemplateID = tmpl.p_ID;
                        tplEl.p_ChildTemplate = tmpl;
                        tplEl.p_Index = item.p_Index;
                        tplEl.p_Value = item.p_Value;
                        newTemplateElements.Add(tplEl);
                    }
                    else
                        oldTemplateElements.Add(item);
                }
            }
            if (newTemplate != null)
            {
                foreach (var te in oldTemplateElements)
                {
                    var tplEl = new Cl_TemplateElement();
                    tplEl.p_TemplateID = newTemplate.p_ID;
                    tplEl.p_Template = newTemplate;
                    tplEl.p_ChildElementID = te.p_ChildElementID;
                    tplEl.p_ChildElement = te.p_ChildElement;
                    tplEl.p_ChildTemplateID = te.p_ChildTemplateID;
                    tplEl.p_ChildTemplate = te.p_ChildTemplate;
                    tplEl.p_Index = te.p_Index;
                    tplEl.p_Value = te.p_Value;
                    newTemplateElements.Add(tplEl);
                }
                newTemplate.p_TemplateElements = newTemplateElements.OrderBy(te => te.p_Index).ToArray();

                return newTemplate;
            }
            else
                return template;
        }

        internal bool f_IsActualElement(Cl_Element element)
        {
            if (element == null) return false;
            Cl_Element actElement = this.f_GetLastVersionElement(element);
            return actElement.p_ID == element.p_ID;
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

            Cl_Template actTemplate = this.f_GetLastVersionTemplate(template);
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
                {
                    if (!this.f_IsActualElementsOnTemplate(item.p_ChildTemplate))
                        return false;
                }
            }

            return defNewTemplate == false && defNewElements == false;
        }

        /// <summary>
        /// Проверка наличия элемента в коллекции элементов
        /// </summary>
        /// <param name="a_Items">Коллекции элементов</param>
        /// <param name="a_Element">Элемент</param>
        public Cl_Element f_HasElement(IEnumerable<I_Element> a_Items, Cl_Element a_Element)
        {
            foreach (var item in a_Items)
            {
                if (item is Ctrl_Element)
                {
                    var el = (Ctrl_Element)item;
                    if (!el.p_Element.p_IsHeader && (el.p_Element.Equals(a_Element) || el.p_Element.p_ElementID == a_Element.p_ElementID))
                        return a_Element;
                }
                else if (item is Ctrl_Template)
                {
                    var tpl = (Ctrl_Template)item;
                    if (tpl.p_Template.f_HasElement(a_Element) != null)
                        return a_Element;
                }
            }
            return null;
        }

        /// <summary>
        /// Проверка наличия элемента в коллекции элементов
        /// </summary>
        /// <param name="a_Items">Коллекции элементов</param>
        /// <param name="a_Element">Шаблон</param>
        public Cl_Element f_HasElement(IEnumerable<I_Element> a_Items, Cl_Template a_Template)
        {
            foreach (var item in a_Items)
            {
                if (item is Ctrl_Element)
                {
                    var el = (Ctrl_Element)item;
                    if (a_Template.f_HasElement(el.p_Element) != null)
                        return el.p_Element;
                }
                else if (item is Ctrl_Template)
                {
                    var tpl = (Ctrl_Template)item;
                    var el = a_Template.f_HasElement(tpl.p_Template);
                    if (el != null)
                        return el;
                }
            }
            return null;
        }

        /// <summary>Сохранение шаблона</summary>
        /// <param name="curTemplate">Сохраняемый шаблон</param>
        /// <param name="elements">Новый список элементов в сохраняемом шаблоне</param>
        /// <param name="isUpSave">Флаг необходимости актуализации версий элментов</param>
        /// <param name="m_Log">Объект логгера</param>
        /// <returns></returns>
        public Cl_Template f_SaveTemplate(Cl_Template curTemplate, I_Element[] elements, bool isUpSave, Cl_EntityLog m_Log = null)
        {
            if (elements.Length > 0)
            {
                if (elements.Any(el => el.f_IsTab()) && !elements[0].f_IsTab())
                {
                    MonitoringStub.Warning("При наличии хотя бы одного элемента «Вкладка» такой элемент должен идти первым.");
                    return null;
                }

                var elHeaders = new List<I_Element>();
                foreach (var element in elements)
                {
                    if (element is Ctrl_Template)
                    {
                        var templ = (Ctrl_Template)element;
                        elHeaders.AddRange(templ.f_GetElements().Where(el => el.f_IsHeader()));
                    }
                    if (element.f_IsHeader())
                    {
                        elHeaders.Add(element);
                    }
                }
                if (elHeaders.Count > 1)
                {
                    int prevHeaderLevel = elHeaders[0].f_GetHeaderLevel();
                    for (int i = 1; i < elHeaders.Count(); i++)
                    {
                        var elHeader = elHeaders[i];
                        var headerLevel = elHeader.f_GetHeaderLevel();
                        if (prevHeaderLevel < headerLevel - 1)
                        {
                            MonitoringStub.Warning("Каждый следующий заголовок может быть либо одним или несколькими уровнями выше, либо одним уровнем ниже.");
                            return null;
                        }
                        prevHeaderLevel = headerLevel;
                    }
                }
            }
            if (isUpSave)
            {
                foreach (I_Element item in elements)
                {
                    if (item is Ctrl_Template)
                    {
                        var block = (Ctrl_Template)item;
                        var templ = f_GetLastVersionTemplate(block.p_Template);
                        if (templ.p_ID != block.p_Template.p_ID)
                        {
                            var els = elements.Where(e => e.p_ElementID != block.p_Template.p_TemplateID);
                            var el = f_HasElement(els, templ);
                            if (el != null)
                            {
                                MonitoringStub.Warning($"В последней версии '{templ.p_Version}' шаблона '{templ.p_Name}' имеется элемент '{el.p_Name}'!");
                                return curTemplate;
                            }
                        }
                    }
                }
            }
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
                        newTemplate.p_ParentGroup = curTemplate.p_ParentGroup;
                        newTemplate.p_Description = curTemplate.p_Description;
                        newTemplate.p_ParentGroupID = curTemplate.p_ParentGroupID;

                        m_DataContextMegaTemplate.p_Templates.Add(newTemplate);
                    }

                    m_DataContextMegaTemplate.SaveChanges();

                    foreach (I_Element item in elements)
                    {
                        var tplEl = new Cl_TemplateElement();
                        tplEl.p_TemplateID = newTemplate.p_ID;
                        tplEl.p_Template = newTemplate;
                        if (item is Ctrl_Element)
                        {
                            var block = (Ctrl_Element)item;
                            if (isUpSave)
                            {
                                var el = f_GetLastVersionElement(block.p_Element);
                                if (el.p_ID != block.p_Element.p_ID)
                                {
                                    tplEl.p_ChildElementID = el.p_ID;
                                    tplEl.p_ChildElement = el;
                                }
                                else
                                {
                                    tplEl.p_ChildElementID = block.p_ID;
                                    tplEl.p_ChildElement = block.p_Element;
                                }
                            }
                            else
                            {
                                tplEl.p_ChildElementID = block.p_ID;
                                tplEl.p_ChildElement = block.p_Element;
                            }
                            if (block.f_IsHeader())
                            {
                                tplEl.p_Value = block.p_Value;
                            }
                        }
                        else if (item is Ctrl_Template)
                        {
                            var block = (Ctrl_Template)item;
                            if (isUpSave)
                            {
                                //var templ = f_GetLastVersionTemplate(block.p_Template);
                                var templ = f_GetActualTemplate(block.p_Template);

                                if (templ.p_ID != block.p_Template.p_ID)
                                {
                                    tplEl.p_ChildTemplateID = templ.p_ID;
                                    tplEl.p_ChildTemplate = templ;
                                }
                                else
                                {
                                    tplEl.p_ChildTemplateID = block.p_ID;
                                    tplEl.p_ChildTemplate = block.p_Template;
                                }
                            }
                            else
                            {
                                tplEl.p_ChildTemplateID = block.p_ID;
                                tplEl.p_ChildTemplate = block.p_Template;
                            }
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
    }
}
