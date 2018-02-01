using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sadco.FamilyDoctor.Core.Entities.Controls
{
	[Table("T_CONTROLS_COMBOBOX")]
	[Description("Элемент с выпадающим списком")]
	public class Cl_CtrlComboBox : I_Control
	{
		private List<string> m_Elements = new List<string>();

		[Key]
		[Column("F_ID")]
		public int p_ID { get; set; }

		/// <summary>Текст-шаблон</summary>
		[Column("F_TEXT", TypeName = "varchar")]
		[MaxLength(8000)]
		public string p_Text {
			get {
				return Newtonsoft.Json.JsonConvert.SerializeObject(m_Elements);
			}
			set {
				if (!string.IsNullOrEmpty(value))
					p_Elements = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(value);
			}
		}

		public List<string> p_Elements {
			get { return m_Elements; }
			set { m_Elements = value; }
		}

		[Column("F_CONTROL_ID")]
		public int p_BaseControlID { get; set; }
		public Cl_Control p_BaseControl { get; set; }

		public Cl_CtrlComboBox() {
			p_BaseControl = new Cl_Control();
			p_BaseControl.p_Image = "combo_box";
		}
	}
}