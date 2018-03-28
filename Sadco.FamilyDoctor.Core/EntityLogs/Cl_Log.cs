using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sadco.FamilyDoctor.Core.EntityLogs
{
	[Table("T_LOGS")]
	public class Cl_Log
	{
		/// <summary>
		/// Ключ
		/// </summary>
		[Key]
		[Column("F_ID")]
		public int p_ID { get; set; }

		/// <summary>
		/// Предыдущий ID лога
		/// </summary>
		[Column("F_PREV_ID")]
		public int p_PrevID { get; set; }

		/// <summary>
		/// ID элемента
		/// </summary>
		[Column("F_ELEMENT_ID")]
		public int p_ElementID { get; set; }

		/// <summary>
		/// Тип элемента
		/// </summary>
		[Column("F_TYPE")]
		public EntityTypes p_EntityType { get; set; }

		/// <summary>
		/// Время, когда было сделано изменение
		/// </summary>
		[Column("F_TIME")]
		public DateTime p_ChangeTime { get; set; }

		/// <summary>
		/// Версия измененного элемента
		/// </summary>
		[Column("F_VERSION")]
		public int p_Version { get; set; }

		/// <summary>
		/// Совершенное действие
		/// </summary>
		[Column("F_EVENT")]
		public string p_Event { get; set; }

		/// <summary>
		/// Имя пользователя совершившего действие
		/// </summary>
		[Column("F_USER")]
		public string p_UserName { get; set; }
	}
}
