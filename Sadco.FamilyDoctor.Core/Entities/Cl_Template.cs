using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sadco.FamilyDoctor.Core.Entities {
	[Table("T_TEMPLATES")]
	public class Cl_Template {
		[Key]
		[Column("F_ID")]
		public int p_ID { get; set; }
		[Column("F_GROUP_ID")]
		[ForeignKey("p_GroupTeplates")]
		public int p_GroupTeplatesID { get; set; }
		public Cl_GroupTemplates p_GroupTeplates { get; set; }
		[Column("F_NAME")]
		public string p_Name { get; set; }
		[Column("F_DESC")]
		public string p_Description { get; set; }
	}
}
