using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace Sadco.FamilyDoctor.Core.Entities
{
	[Table("T_TEMPLATES")]
	public class Cl_Template : I_Control
	{
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

		[ForeignKey("p_TemplateID")]
		public ICollection<Cl_TemplateControl> p_TemplateControls { get; set; }

		public string p_IconName { get { return "template"; } }

		public Bitmap p_Icon {
			get {
				try {
					object obj = Properties.Resources.ResourceManager.GetObject(p_IconName);
					return ((Bitmap)obj);
				} catch {
					return null;
				}
			}
		}
	}
}
