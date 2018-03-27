using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sadco.FamilyDoctor.Core.EntityLogs
{
	public enum EntityTypes : int
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
		Templates
	}

	[AttributeUsage(AttributeTargets.Class)]
	public class ELogClassAttribute : Attribute
	{
		public ELogClassAttribute(EntityTypes type) {
			this.EntityType = type;
		}

		/// <summary>
		/// Тип исследуемого объекта
		/// </summary>
		public EntityTypes EntityType { get; set; }
	}
}
