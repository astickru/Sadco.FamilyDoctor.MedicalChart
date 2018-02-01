using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sadco.FamilyDoctor.Core.Entities {
	[Table("T_TEMPLATES")]
	public class Cl_Template {
		[Key]
		[Column("F_ID")]
		public int p_ID { get; set; }

		[Column("F_GROUP_ID")]
		[ForeignKey("p_ParentGroup")]
		public int p_ParentGroupID { get; set; }
		public Cl_GroupsTemplate p_ParentGroup { get; set; }

		[Column("F_NAME", TypeName = "varchar")]
		[MaxLength(100)]
		public string p_Name { get; set; }
		[Column("F_DESC", TypeName = "varchar")]
		[MaxLength(1000)]
		public string p_Description { get; set; }
	}
}
