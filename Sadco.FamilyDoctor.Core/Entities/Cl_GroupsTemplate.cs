using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sadco.FamilyDoctor.Core.Entities
{
	[Table("T_GROUPS_TEMPLATES")]
	public class Cl_GroupsTemplate : I_Group
	{
		public Cl_GroupsTemplate() {
			p_SubGroups = new List<Cl_GroupsTemplate>();
		}

		[Key]
		[Column("F_ID")]
		public int p_ID { get; set; }

		[Column("F_NAME", TypeName = "varchar")]
		[MaxLength(100)]
		public string p_Name { get; set; }

		[Column("F_PARENT_ID")]
		public int? p_ParentID { get; set; }
		public Cl_GroupsTemplate p_Parent { get; set; }

		[ForeignKey("p_ParentID")]
		public virtual ICollection<Cl_GroupsTemplate> p_SubGroups { get; set; }

		[ForeignKey("p_ID")]
		public virtual ICollection<Cl_Template> cl_Templates { get; set; }

		public string f_GetParentName<T>(T parent) where T : I_Group {
			Cl_GroupsTemplate parentObj = parent as Cl_GroupsTemplate;

			string name = parent.p_Name;
			if (parentObj.p_Parent != null) {
				name = f_GetParentName(parentObj.p_Parent) + "/" + name;
			}
			return name;
		}

		public string f_GetFullName() {
			if (p_Parent != null) {
				return f_GetParentName(p_Parent) + "/" + p_Name;
			}
			return p_Name;
		}
	}
}
