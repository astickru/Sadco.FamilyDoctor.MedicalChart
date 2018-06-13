using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sadco.FamilyDoctor.Core.EntityLogs
{
	[Table("T_RATINGS")]
	public class Cl_Rating
    {
		/// <summary>
		/// Ключ
		/// </summary>
		[Key]
		[Column("F_ID")]
		public int p_ID { get; set; }

		/// <summary>
		/// ID записи
		/// </summary>
		[Column("F_RECORD_ID")]
		public int p_RecordID { get; set; }

		/// <summary>
		/// Время, когда было сделано изменение
		/// </summary>
		[Column("F_TIME")]
		public DateTime p_Time { get; set; }

		/// <summary>
		/// Значение оценки
		/// </summary>
		[Column("F_VALUE")]
		public int p_Value { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        [Column("F_COMMENT")]
        public string p_Comment { get; set; }

        /// <summary>ID пользователя</summary>
        [Column("F_USER_ID")]
        public int p_UserID { get; set; }

        /// <summary>Имя пользователя</summary>
        [Column("F_USER_NAME")]
        public string p_UserName { get; set; }
    }
}
