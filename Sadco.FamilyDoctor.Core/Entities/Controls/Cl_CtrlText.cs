using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sadco.FamilyDoctor.Core.Entities.Controls {
	[Table("T_CONTROLS_TEXTS")]
	public class Cl_CtrlText {
		[Key]
		[Column("F_ID")]
		public int p_ID { get; set; }
		/// <summary>Текст-шаблон</summary>
		[Column("F_TEXT")]
		public string p_Text { get; set; }
		///// <summary>Текст-шаблон</summary>
		//[Column("F_TEXT")]
		//public string p_Text { get; set; }
	}
}
