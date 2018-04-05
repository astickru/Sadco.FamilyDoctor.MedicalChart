using Sadco.FamilyDoctor.Core.Permision.Enums;
using System;

namespace Sadco.FamilyDoctor.Core.Permision
{
	/// <summary>
	/// Уровень предоставляемого доступа
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	public class AccessLevelAttribute : Attribute
	{
		/// <summary>
		/// Текущий уровень доступа
		/// </summary>
		public AccessLevels AccessLevel { get; set; }

		public AccessLevelAttribute(AccessLevels level)
		{
			AccessLevel = level;
		}
	}
}
