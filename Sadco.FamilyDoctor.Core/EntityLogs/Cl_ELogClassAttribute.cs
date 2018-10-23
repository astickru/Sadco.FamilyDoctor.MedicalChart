using System;

namespace Sadco.FamilyDoctor.Core.EntityLogs
{
    public enum E_EntityTypes : int
    {
        /// <summary>
        /// Группа
        /// </summary>
        Groups = 1,
        /// <summary>
        /// Элемент
        /// </summary>
        Elements = 2,
        /// <summary>
        /// Шаблон
        /// </summary>
        Templates = 3,
        /// <summary>
        /// Записи врача
        /// </summary>
        Records = 4,
        /// <summary>
        /// Паттерны записей врача
        /// </summary>
        RecordsPatterns = 5,
        /// <summary>
        /// Оценка записей
        /// </summary>
        Rating = 6,
        /// <summary>
        /// События приложения
        /// </summary>
        AppEvents = 100,
        /// <summary>
        /// События UI форм
        /// </summary>
        UIEvents = 110,
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class Cl_ELogClassAttribute : Attribute
    {
        public Cl_ELogClassAttribute(E_EntityTypes type)
        {
            this.p_EntityType = type;
        }

        /// <summary>
        /// Тип исследуемого объекта
        /// </summary>
        public E_EntityTypes p_EntityType { get; set; }
    }
}
