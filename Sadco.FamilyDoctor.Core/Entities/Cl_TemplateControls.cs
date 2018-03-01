using Sadco.FamilyDoctor.Core.Entities.Controls;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sadco.FamilyDoctor.Core.Entities
{
	[Table("T_TEMPLATECONTROLS")]
	public class Cl_TemplateControl
	{
		[Key]
		[Column("F_ID")]
		public int p_ID { get; set; }

		[Column("F_CONTROL_ID")]
		public int p_ControlID { get; set; }

		[Column("F_TEMPLATE_ID")]
		[ForeignKey("p_Template")]
		public int p_TemplateID { get; set; }
		public Cl_Template p_Template { get; set; }

		[Column("F_CONTROL_TYPE", TypeName = "varchar")]
		public string p_ControlType { get; set; }

		[Column("F_POSX")]
		public int p_PositionX { get; set; }
		[Column("F_POSY")]
		public int p_PositionY { get; set; }
		[Column("F_WIDTH")]
		public int p_Width { get; set; }
		[Column("F_HEIGHT")]
		public int p_Height { get; set; }

		private I_BaseControl m_Control = null;
		public I_BaseControl p_Control {
			get {
				if (m_Control != null) return m_Control;

				if (!Cl_App.m_DataContext.f_GetAvailableControls().ContainsKey(p_ControlType)) return null;

				return m_Control = Cl_App.m_DataContext.f_GetControlByType(p_ControlID, p_ControlType);
			}
			set {
				m_Control = value;
				p_ControlType = value.GetType().Name;
				p_ControlID = value.p_ID;
			}
		}
	}
}
