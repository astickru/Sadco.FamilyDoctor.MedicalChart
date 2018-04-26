using System;

namespace Sadco.FamilyDoctor.Core.EntityLogs
{
	[AttributeUsage(AttributeTargets.Property)]
	public class Cl_ELogPropertyAttribute : Attribute
	{
		/// <summary>
		/// Инициализация
		/// </summary>
		public Cl_ELogPropertyAttribute() : this("") { }

		/// <summary>
		/// Инициализация
		/// </summary>
		/// <param name="description">Название изменившегося поля</param>
		public Cl_ELogPropertyAttribute(string description) {
			this.p_Description = description;
		}

		/// <summary>
		/// Описание поля
		/// </summary>
		public string p_Description { get; set; }

		/// <summary>
		/// Указывает, что используется индивидуальное описание события
		/// </summary>
		public bool p_IsCustomDescription { get; set; }

		/// <summary>
		/// Игнорирование значения
		/// </summary>
		public bool p_IgnoreValue { get; set; }

        /// <summary>
        /// Сохранять в истории только новое значение, старое игнорируется
        /// </summary>
        public bool p_IsNewValueOnly { get; set; }

        /// <summary>
        /// Текст лога является вычисляемым
        /// </summary>
        public bool p_IsComputedLog { get; set; }
    }
}
