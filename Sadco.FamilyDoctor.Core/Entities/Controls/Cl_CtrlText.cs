using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sadco.FamilyDoctor.Core.Entities.Controls
{
	[Table("T_CONTROLS_TEXT")]
	[Description("Текстовый элемент")]
	public class Cl_CtrlText : I_Control
	{
		[Key]
		[Column("F_ID")]
		public int p_ID { get; set; }

		/// <summary>Текст-шаблон</summary>
		[Column("F_TEXT", TypeName = "varchar")]
		[MaxLength(8000)]
		public string p_Text { get; set; }

		[Column("F_CONTROL_ID")]
		public int p_BaseControlID { get; set; }
		public Cl_Control p_BaseControl { get; set; }

		public Cl_CtrlText() {
			p_BaseControl = new Cl_Control();
			p_BaseControl.p_Image = "label";
		}
	}
}
