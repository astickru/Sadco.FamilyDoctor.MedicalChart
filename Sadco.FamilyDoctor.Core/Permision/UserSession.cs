using System;

namespace Sadco.FamilyDoctor.Core.Permision
{
	public class UserSession
	{
		private UserSession() { }

		/// <summary>
		/// ID пользователя
		/// </summary>
		public static int ID { get; set; }

		/// <summary>
		/// Имя пользователя
		/// </summary>
		public static string Name { get; set; }

		/// <summary>
		/// Уровень доступа пользователя
		/// </summary>
		public static UserPermission Permission { get; set; }

		/// <summary>
		/// ID пациента
		/// </summary>
		public static int PatientID { get; set; }

		/// <summary>
		/// Имя пациента
		/// </summary>
		public static string PatientName { get; set; }

		/// <summary>
		/// День рождения пациента
		/// </summary>
		public static DateTime PatientBirthday { get; set; }

		/// <summary>
		/// Начало выбранного диапазона даты
		/// </summary>
		public static DateTime TimeIntervalStart { get; set; }

		/// <summary>
		/// Конец выбранного диапазона даты
		/// </summary>
		public static DateTime TimeIntervalEnd { get; set; }
	}
}
