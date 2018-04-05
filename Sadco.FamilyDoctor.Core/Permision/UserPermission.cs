using Sadco.FamilyDoctor.Core.Permision.Enums;
using System;

namespace Sadco.FamilyDoctor.Core.Permision
{
	public class UserPermission
	{
		private string m_Role;

		/// <summary>
		/// Текущая роль
		/// </summary>
		public Roles Role { get; }
		/// <summary>
		/// Уровень доступа
		/// </summary>
		public AccessLevels AccessLevel { get; }
		/// <summary>
		/// Не имеет доступа
		/// </summary>
		public bool IsNoAccess { get { return AccessLevel == AccessLevels.None; } }
		/// <summary>
		/// Доступ ко всем записям (редактирование)
		/// </summary>
		public bool IsEditAllRecords { get { return (AccessLevel & AccessLevels.EditAllRecords) == AccessLevels.EditAllRecords; } }
		/// <summary>
		/// Доступ к записям в роли ассистента (редактирование)
		/// </summary>
		public bool IsEditAssistantRecords { get { return (AccessLevel & AccessLevels.EditAssistantRecords) == AccessLevels.EditAssistantRecords; } }
		/// <summary>
		/// Доступ только к своим записям (редактирование)
		/// </summary>
		public bool IsEditSelfRecords { get { return (AccessLevel & AccessLevels.EditSelfRecords) == AccessLevels.EditSelfRecords; } }
		/// <summary>
		/// Доступ на чтение всех записей
		/// </summary>
		public bool IsReadAllRecords { get { return (AccessLevel & AccessLevels.ReadAllRecords) == AccessLevels.ReadAllRecords; } }
		/// <summary>
		/// Доступ на чтение, только выбранных записей
		/// </summary>
		public bool IsReadSelectedRecords { get { return (AccessLevel & AccessLevels.ReadSelectedRecords) == AccessLevels.ReadSelectedRecords; } }
		/// <summary>
		/// Доступ к системе оценок
		/// </summary>
		public bool IsEditAllRatings { get { return (AccessLevel & AccessLevels.EditAllRatings) == AccessLevels.EditAllRatings; } }
		/// <summary>
		/// Доступ к редактору шаблона
		/// </summary>
		public bool IsEditTemplates { get { return (AccessLevel & AccessLevels.EditTemplates) == AccessLevels.EditTemplates; } }
		/// <summary>
		/// Доступ к мегашаблону
		/// </summary>
		public bool IsEditMegaTemplates { get { return (AccessLevel & AccessLevels.EditMegaTemplates) == AccessLevels.EditMegaTemplates; } }

		public UserPermission(string role)
		{
			this.m_Role = role;
			Role = f_GetRole<Roles>(this.m_Role);
			AccessLevel = f_GetAccessLevel(Role);
		}

		private T f_GetRole<T>(string roleName)
		{
			return (T)Enum.Parse(typeof(T), roleName);
		}

		private AccessLevels f_GetAccessLevel(Roles role)
		{
			return (Attribute.GetCustomAttribute(role.GetType().GetField(role.ToString()), typeof(AccessLevelAttribute)) as AccessLevelAttribute).AccessLevel;
		}
	}
}
