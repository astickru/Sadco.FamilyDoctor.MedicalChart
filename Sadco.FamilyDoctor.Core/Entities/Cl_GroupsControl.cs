using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sadco.FamilyDoctor.Core.Entities
{
	[Table("T_GROUPS_CONTROL")]
	public class Cl_GroupsControl {

		public Cl_GroupsControl() {
			p_SubGroups = new List<Cl_GroupsControl>();
		}

		[Key]
		[Column("F_ID")]
		public int p_ID { get; set; }
		[Column("F_NAME", TypeName = "varchar")]
		[MaxLength(100)]
		public string p_Name { get; set; }

		[Column("F_PARENT_ID")]
		public int? p_ParentID { get; set; }
		public Cl_GroupsControl p_Parent { get; set; }

		[ForeignKey("p_ParentID")]
		public virtual ICollection<Cl_GroupsControl> p_SubGroups { get; set; }

		private string f_GetParentName(Cl_GroupsControl a_ParentGroup) {
			string name = a_ParentGroup.p_Name;
			if (a_ParentGroup.p_Parent != null) {
				name = f_GetParentName(a_ParentGroup.p_Parent) + "/" + name;
			}
			return name;
		}

		public string f_GetFullName () {
			if (p_Parent != null) {
				return f_GetParentName(p_Parent) + "/" + p_Name;
			}
			return p_Name;
		}
	}
}
