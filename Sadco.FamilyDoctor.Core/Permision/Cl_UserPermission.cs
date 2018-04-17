using System;

namespace Sadco.FamilyDoctor.Core.Permision
{
    /// <summary>
    /// Класс доступа пользователя
    /// </summary>
    public class Cl_UserPermission
	{
        public Cl_UserPermission(string role)
        {
            m_Role = role;
            p_Role = f_GetRole<E_Roles>(m_Role);
            p_AccessLevel = f_GetAccessLevel(p_Role);
        }

        /// <summary>Текущая роль</summary>
		public E_Roles p_Role { get; }
		
        /// <summary>Уровень доступа</summary>
		public E_AccessLevels p_AccessLevel { get; }
		
        /// <summary>Не имеет доступа</summary>
		public bool p_IsNoAccess { get { return p_AccessLevel == E_AccessLevels.None; } }
		
        /// <summary>Доступ ко всем записям (редактирование)</summary>
		public bool p_IsEditAllRecords { get { return (p_AccessLevel & E_AccessLevels.EditAllRecords) == E_AccessLevels.EditAllRecords; } }
		
        /// <summary>Доступ к записям в роли ассистента (редактирование)</summary>
		public bool p_IsEditAssistantRecords { get { return (p_AccessLevel & E_AccessLevels.EditAssistantRecords) == E_AccessLevels.EditAssistantRecords; } }
		
        /// <summary>Доступ только к своим записям (редактирование)</summary>
		public bool p_IsEditSelfRecords { get { return (p_AccessLevel & E_AccessLevels.EditSelfRecords) == E_AccessLevels.EditSelfRecords; } }
		
        /// <summary>Доступ на чтение всех записей</summary>
		public bool p_IsReadAllRecords { get { return (p_AccessLevel & E_AccessLevels.ReadAllRecords) == E_AccessLevels.ReadAllRecords; } }
		
        /// <summary>Доступ на чтение, только выбранных записей</summary>
		public bool p_IsReadSelectedRecords { get { return (p_AccessLevel & E_AccessLevels.ReadSelectedRecords) == E_AccessLevels.ReadSelectedRecords; } }
		
        /// <summary>Доступ к системе оценок</summary>
		public bool p_IsEditAllRatings { get { return (p_AccessLevel & E_AccessLevels.EditAllRatings) == E_AccessLevels.EditAllRatings; } }
		
        /// <summary>Доступ к редактору шаблона</summary>
		public bool p_IsEditTemplates { get { return (p_AccessLevel & E_AccessLevels.EditTemplates) == E_AccessLevels.EditTemplates; } }
		
        /// <summary>Доступ к мегашаблону</summary>
		public bool p_IsEditMegaTemplates { get { return (p_AccessLevel & E_AccessLevels.EditMegaTemplates) == E_AccessLevels.EditMegaTemplates; } }

        /// <summary>Флаг отображения удаленных элементов</summary>
        public bool p_IsShowDeleted { get { return (p_AccessLevel & E_AccessLevels.IsShowDeleted) == E_AccessLevels.IsShowDeleted; } }

        private string m_Role;

        private T f_GetRole<T>(string roleName)
		{
			return (T)Enum.Parse(typeof(T), roleName);
		}

		private E_AccessLevels f_GetAccessLevel(E_Roles role)
		{
			return (Attribute.GetCustomAttribute(role.GetType().GetField(role.ToString()), typeof(Cl_AccessLevelAttribute)) as Cl_AccessLevelAttribute).AccessLevel;
		}
	}
}
