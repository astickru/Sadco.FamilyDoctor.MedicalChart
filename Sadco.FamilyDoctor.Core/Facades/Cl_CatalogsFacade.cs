using Sadco.FamilyDoctor.Core.Data;
using Sadco.FamilyDoctor.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sadco.FamilyDoctor.Core.Facades
{
    /// <summary>
    /// Фасад работы со справочниками
    /// </summary>
    public class Cl_CatalogsFacade
    {
        private static Cl_CatalogsFacade INSTANCE = new Cl_CatalogsFacade();
        public static Cl_CatalogsFacade f_GetInstance()
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

        /// <summary>Проверяет наличие категории</summary>
        public bool f_HasCategory(string a_CategoryName)
        {
            if (m_IsInit)
            {
                return m_DataContextMegaTemplate.p_Categories.Any(c => c.p_Name == a_CategoryName);
            }
            return false;
        }

        /// <summary>Получение категории</summary>
        public Cl_Category f_GetCategory(string a_CategoryName)
        {
            if (m_IsInit)
            {
                return m_DataContextMegaTemplate.p_Categories.FirstOrDefault(c => c.p_Name == a_CategoryName);
            }
            else return null;
        }

        /// <summary>Добавление новой категории</summary>
        public Cl_Category f_AddCategory(Cl_Category.E_CategoriesTypes a_CategoryType, string a_CategoryName)
        {
            if (m_IsInit)
            {
                var cat = new Cl_Category() { p_Type = a_CategoryType, p_Name = a_CategoryName };
                m_DataContextMegaTemplate.p_Categories.Add(cat);
                m_DataContextMegaTemplate.SaveChanges();
                return cat;
            }
            else return null;
        }
    }
}
