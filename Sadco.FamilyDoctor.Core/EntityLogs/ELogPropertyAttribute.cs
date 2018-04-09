using System;

namespace Sadco.FamilyDoctor.Core.EntityLogs
{
	[AttributeUsage(AttributeTargets.Property)]
	public class ELogPropertyAttribute : Attribute
	{
		/// <summary>
		/// Инициализация
		/// </summary>
		public ELogPropertyAttribute() : this("") { }

		/// <summary>
		/// Инициализация
		/// </summary>
		/// <param name="description">Название изменившегося поля</param>
		public ELogPropertyAttribute(string description) {
			this.Description = description;
		}

		/// <summary>
		/// Описание поля
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Указывает, что используется индивидуальное описание события
		/// </summary>
		public bool IsCustomDescription { get; set; }

		/// <summary>
		/// Игнорирование значения
		/// </summary>
		public bool IgnoreValue { get; set; }

        /// <summary>
        /// Сохранять в истории только новое значение, старое игнорируется
        /// </summary>
        public bool IsNewValueOnly { get; set; }
    }
}
