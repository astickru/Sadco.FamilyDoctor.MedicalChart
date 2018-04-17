using System;

namespace Sadco.FamilyDoctor.Core.Permision
{
    /// <summary>
    /// Уровень предоставляемого доступа
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
	public class Cl_AccessLevelAttribute : Attribute
	{
		public Cl_AccessLevelAttribute(E_AccessLevels level)
		{
			AccessLevel = level;
		}

        /// <summary>Текущий уровень доступа</summary>
		public E_AccessLevels AccessLevel { get; set; }
    }
}
