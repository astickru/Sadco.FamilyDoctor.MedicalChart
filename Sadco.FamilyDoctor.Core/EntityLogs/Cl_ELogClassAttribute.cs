using System;

namespace Sadco.FamilyDoctor.Core.EntityLogs
{
    public enum E_EntityTypes : int
    {
        /// <summary>
        /// Группа
        /// </summary>
        Groups,
        /// <summary>
        /// Элемент
        /// </summary>
        Elements,
        /// <summary>
        /// Шаблон
        /// </summary>
        Templates,
        /// <summary>
        /// Записи врача
        /// </summary>
        Records
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
