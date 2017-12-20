using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sadco.FamilyDoctor.Core.Entities.Controls {
	[Table("T_CONTROLS")]
	public abstract class Cl_Control {
		[Key]
		[Column("F_ID")]
		public int p_ID { get; set; }
		/// <summary>Обязательность заполнения</summary>
		[Column("F_REQUIRED")]
		public bool p_Required { get; set; }
		/// <summary>Возможность редактирования</summary>
		[Column("F_EDITING")]
		public bool p_Editing { get; set; }
		/// <summary>Видимость безусловная</summary>
		[Column("F_VISIBLE")]
		public bool p_Visible { get; set; }
		///// <summary>Внешняя ссылка</summary>
		//[Column("F_REFERENCE")]
		//public asda p_Reference { get; set; }
		/// <summary>Подсказка по заполнению (отображается всплывающей подсказкой или сообщение по F1)</summary>
		[Column("F_HELP")]
		public string p_Help { get; set; }
		/// <summary>Признак симметричности.
		/// Симметричность подразумевает повторение содержимого блока 2 раза (это используется для описания одинаковых правых и левых органов)
		/// с автоматически добавляемыми заголовками, указанными в его свойствах (напр., «справа» и «слева» или “OD” и “OS”)</summary>
		[Column("F_SYMMETRICAL")]
		public bool p_Symmetrical { get; set; }
		/// <summary>Текст обозначения 1 стороны симметричности</summary>
		[Column("F_SYMMETRYPARAM1")]
		public string p_SymmetryParam1 { get; set; }
		/// <summary>Текст обозначения 2 стороны симметричности</summary>
		[Column("F_SYMMETRYPARAM2")]
		public string p_SymmetryParam2 { get; set; }
		/// <summary>Примечание</summary>
		[Column("F_COMMENT")]
		public string p_Comment { get; set; }
	}
}
