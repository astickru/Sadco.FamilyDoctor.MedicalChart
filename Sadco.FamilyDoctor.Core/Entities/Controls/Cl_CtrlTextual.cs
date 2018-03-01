using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace Sadco.FamilyDoctor.Core.Entities.Controls
{
	public enum ValueTypes : int
	{
		Single,
		Multi
	}

	public enum TextControlTypes : int
	{
		[Description("Текстовое поле")]
		Text = 1,
		[Description("Галочка выбора")]
		CheckBox,
		[Description("Выпадающий список")]
		ComboBox,
		[Description("Список")]
		List
	}

	[Table("T_CONTROLS_TEXTUAL")]
	[Description("Текстовый элемент")]
	public class Cl_CtrlTextual : I_BaseControl
	{
		private TextControlTypes m_Types = TextControlTypes.Text;
		private string m_Text = "";
		private List<string> m_Elements = new List<string>();

		[Key]
		[Column("F_ID")]
		public int p_ID { get; set; }

		[Column("F_CONTROL_TYPE")]
		public TextControlTypes p_ControlType {
			get { return m_Types; }
			set {
				m_Types = value;

				switch (m_Types) {
					case TextControlTypes.Text:
						p_BaseControl.p_IconName = "label";
						m_ValueType = ValueTypes.Single;
						break;
					case TextControlTypes.CheckBox:
						p_BaseControl.p_IconName = "check_box";
						m_ValueType = ValueTypes.Single;
						break;
					case TextControlTypes.ComboBox:
						p_BaseControl.p_IconName = "combo_box";
						m_ValueType = ValueTypes.Multi;
						break;
					case TextControlTypes.List:
						p_BaseControl.p_IconName = "list_box";
						m_ValueType = ValueTypes.Multi;
						break;
					default:
						p_ControlType = TextControlTypes.Text;
						break;
				}
			}
		}

		//[NotMapped]
		//[Column("F_VALUETYPE")]
		private ValueTypes m_ValueType = ValueTypes.Single;
		public ValueTypes f_ValueType() {
			return m_ValueType;
		}

		/// <summary>Текст-шаблон</summary>
		[Column("F_TEXT", TypeName = "varchar")]
		[MaxLength(8000)]
		public string p_Text {
			get {
				if (f_ValueType() == ValueTypes.Single)
					return m_Text;
				else
					return Newtonsoft.Json.JsonConvert.SerializeObject(m_Elements);
			}
			set {
				if (f_ValueType() == ValueTypes.Single)
					m_Text = value;
				else
					p_Elements = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(value);
			}
		}

		[Column("F_CONTROL_ID")]
		//[ForeignKey("p_BaseControl")]
		public int p_BaseControlID { get; set; }
		public Cl_Control p_BaseControl { get; set; }

		public List<string> p_Elements {
			get { return m_Elements; }
			set { m_Elements = value; }
		}

		public string p_IconName { get { return this.p_BaseControl.p_IconName; } }
		public Bitmap p_Icon { get { return this.p_BaseControl.p_Icon; } }

		public Cl_CtrlTextual() {
			p_BaseControl = new Cl_Control();
			p_ControlType = TextControlTypes.Text;
		}
	}
}