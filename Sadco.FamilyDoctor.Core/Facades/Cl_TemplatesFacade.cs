using Sadco.FamilyDoctor.Core.Data;
using Sadco.FamilyDoctor.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        /// <summary>Получение полного списка элемента в шаблоне</summary>
        public Cl_Element[] f_GetElements(Cl_Template a_Template)
        {
            var elements = new List<Cl_Element>();
            if (a_Template == null) return elements.ToArray();
            if (a_Template.p_TemplateElements == null)
            {
                a_Template.f_LoadTemplatesElements();
            }
            foreach (var te in a_Template.p_TemplateElements)
            {
                if (te.p_ChildElement != null) elements.Add(te.p_ChildElement);
                elements.AddRange(f_GetElements(te.p_ChildTemplate));
            }
            return elements.ToArray();
        }
    }
}
