using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace Sadco.FamilyDoctor.Core.Entities.Controls
{
	[Table("T_CONTROLS")]
	public class Cl_Control
	{
		private bool m_Required = false;
		private bool m_Editing = true;
		private bool m_Visible = true;
		private bool m_Symmetrical = false;

		[Key]
		[Column("F_ID")]
		public int p_ID { get; set; }
		/// <summary>Индивидуальное имя элемента</summary>
		[Column("F_NAME", TypeName = "varchar")]
		[MaxLength(100)]
		public string p_Name { get; set; }
		/// <summary>Иконка меню</summary>
		[Column("F_ICON", TypeName = "varchar")]
		[MaxLength(50)]
		public string p_IconName { get; set; }
		/// <summary>Обязательность заполнения</summary>
		[Column("F_REQUIRED")]
		public bool p_Required { get { return m_Required; } set { m_Required = value; } }
		/// <summary>Возможность редактирования</summary>
		[Column("F_EDITING")]
		public bool p_Editing { get { return m_Editing; } set { m_Editing = value; } }
		/// <summary>Видимость безусловная</summary>
		[Column("F_VISIBLE")]
		public bool p_Visible { get { return m_Visible; } set { m_Visible = value; } }
		/// <summary>Подсказка по заполнению (отображается всплывающей подсказкой или сообщение по F1)</summary>
		[Column("F_HELP", TypeName = "varchar")]
		[MaxLength(500)]
		public string p_Help { get; set; }
		/// <summary>Признак симметричности.
		/// Симметричность подразумевает повторение содержимого блока 2 раза (это используется для описания одинаковых правых и левых органов)
		/// с автоматически добавляемыми заголовками, указанными в его свойствах (напр., «справа» и «слева» или “OD” и “OS”)</summary>
		[Column("F_SYMMETRICAL")]
		public bool p_Symmetrical { get { return m_Symmetrical; } set { m_Symmetrical = value; } }
		/// <summary>Текст обозначения 1 стороны симметричности</summary>
		[Column("F_SYMMETRYPARAMLEFT", TypeName = "varchar")]
		[MaxLength(50)]
		public string p_SymmetryParamLeft { get; set; }
		/// <summary>Текст обозначения 2 стороны симметричности</summary>
		[Column("F_SYMMETRYPARAMRIGHT", TypeName = "varchar")]
		[MaxLength(50)]
		public string p_SymmetryParamRight { get; set; }
		/// <summary>Примечание</summary>
		[Column("F_COMMENT", TypeName = "varchar")]
		[MaxLength(1000)]
		public string p_Comment { get; set; }

		[Column("F_GROUP_ID")]
		[ForeignKey("p_ParentGroup")]
		public int p_ParentGroupID { get; set; }
		public Cl_GroupsControl p_ParentGroup { get; set; }

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
