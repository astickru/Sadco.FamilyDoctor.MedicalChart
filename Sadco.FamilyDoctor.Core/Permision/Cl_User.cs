using System;
using System.ComponentModel;

namespace Sadco.FamilyDoctor.Core.Permision
{
    /// <summary>
    /// Класс пользователя
    /// </summary>
    public class Cl_User
    {
        public enum E_Sex : byte
        {
            /// <summary>Мужской</summary>
            [Description("Мужской")]
            Man,
            /// <summary>Женский</summary>
            [Description("Женский")]
            Female,
            /// <summary>Нет данных</summary>
            [Description("Нет данных")]
            None
        }

        /// <summary>Название клиники</summary>
        public string p_ClinicName { get; set; }

        /// <summary>ID пользователя</summary>
        public int p_UserID { get; set; }

        /// <summary>UID пользователя</summary>
        public Guid? p_UserUID { get; set; }

        /// <summary>Имя пользователя</summary>
        public string p_UserName { get; set; }

        /// <summary>Фамилия пользователя</summary>
        public string p_UserSurName { get; set; }

        /// <summary>Отчество пользователя</summary>
        public string p_UserLastName { get; set; }

        /// <summary>Пол</summary>
        public E_Sex p_Sex { get; set; }

        /// <summary>Дата рождения</summary>
        public DateTime p_DateBirth { get; set; }

        /// <summary>Уровень доступа пользователя</summary>
		public Cl_UserPermission p_Permission { get; set; }

        /// <summary>Руководитель</summary>
        public Cl_User p_ParentUser { get; set; }

        /// <summary>Инициалы пользователя</summary>
        public string p_FIO { get { return f_GetInitials(); } }
        /// <summary>Возвращает инициалы пользователя</summary>
        public string f_GetInitials()
        {
            return string.Format("{0} {1} {2}", p_UserSurName, string.IsNullOrWhiteSpace(p_UserName) ? "" : p_UserName[0].ToString() + ".", string.IsNullOrWhiteSpace(p_UserLastName) ? "" : p_UserLastName[0].ToString() + ".");
        }
    }
}
