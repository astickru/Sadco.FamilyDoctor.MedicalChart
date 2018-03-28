using Sadco.FamilyDoctor.Core.EntityLogs;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Sadco.FamilyDoctor.Core.Entities
{
	/// <summary>
	/// Класс элемента шаблона
	/// </summary>
	[ELogClass(EntityTypes.Elements)]
	[Table("T_ELEMENTS")]
	public class Cl_Element : I_ELog, I_Archive
	{
		/// <summary>Типы элементов шаблона</summary>
		public enum E_ElementsTypes : byte
		{
			/// <summary>Строка в тексте</summary>
			[Description("Строка в тексте")]
			Float,
			/// <summary>Отдельная строка</summary>
			[Description("Отдельная строка")]
			Line,
			/// <summary>Большой текст</summary>
			[Description("Большой текст")]
			Bigbox,
			/// <summary>Рисунок</summary>
			[Description("Рисунок")]
			Image
		}

		/// <summary>Типы элементов шаблона</summary>
		public enum E_TextTypes : byte
		{
			/// <summary>Строка в текстеа</summary>
			[Description("Строка в тексте")]
			Float,
			/// <summary>Отдельная строка</summary>
			[Description("Отдельная строка")]
			Line,
			/// <summary>Большой текст</summary>
			[Description("Большой текст")]
			Bigbox
		}

		private E_ElementsTypes m_Types = E_ElementsTypes.Float;
		private bool m_Required = false;
		private bool m_Editing = true;
		private bool m_Visible = true;
		private bool m_Symmetrical = false;

		/// <summary>Ключ элемента шаблона</summary>
		[Key]
		[Column("F_ID")]
		public int p_ID { get; set; }

		/// <summary>ID элемента шаблона для всех версий</summary>
		[Column("F_ELEMENT_ID")]
		public int p_ElementID { get; set; }

		/// <summary>Системное имя элемента</summary>
		[Column("F_NAME", TypeName = "varchar")]
		[MaxLength(100)]
		[ELogProperty("Название элемента")]
		public string p_Name { get; set; }

		/// <summary>Тэг элемента</summary>
		[Column("F_TAG", TypeName = "varchar")]
		[MaxLength(60)]
		[ELogProperty("Тег элемента")]
		public string p_Tag { get; set; }

        /// <summary>ID обработчик</summary>
        [Column("F_HANDLERID")]
        [MaxLength(100)]
        public string p_HandlerID { get; set; }

        /// <summary>Аргументы обработчика</summary>
        [Column("F_HANDLER_ARGS")]
        [MaxLength(200)]
        public string p_HandlerArguments { get; set; }

        /// <summary>Вид текстового элемента</summary>
		[Column("F_ELEMENT_TYPE")]
		[ELogProperty("Вид текстового элемента")]
		public E_ElementsTypes p_ElementType {
			get { return m_Types; }
			set {
				m_Types = value;
				switch (m_Types) {
					case E_ElementsTypes.Float:
						p_IconName = "FLOAT_16";
						p_IsMulti = false;
						break;
					case E_ElementsTypes.Line:
						p_IconName = "LINE_16";
						p_IsMulti = false;
						break;
					case E_ElementsTypes.Bigbox:
						p_IconName = "BIGBOX_16";
						p_IsMulti = true;
						break;
					case E_ElementsTypes.Image:
						p_IconName = "IMAGE_16";
						p_IsMulti = false;
						break;
					default:
						p_ElementType = E_ElementsTypes.Float;
						p_IconName = "FLOAT_16";
						p_IsMulti = false;
						break;
				}
			}
		}

		/// <summary>Возвращает является ли текстовым элементом</summary>
		public bool f_IsText() {
			return p_ElementType == E_ElementsTypes.Float || p_ElementType == E_ElementsTypes.Line || p_ElementType == E_ElementsTypes.Bigbox;
		}

		/// <summary>Возвращает является ли текстовым элементом</summary>
		public bool f_IsImage() {
			return p_ElementType == E_ElementsTypes.Image;
		}

		/// <summary>Версия элемента шаблона</summary>
		[Column("F_VERSION")]
		public int p_Version { get; set; }

		/// <summary>Системное наименование иконки</summary>
		[NotMapped]
		public string p_IconName { get; set; }

		[NotMapped]
		/// <summary>Флаг многострочности текстового элемента</summary>
		public bool p_IsMulti { get; set; }

		/// <summary>Обязательность заполнения</summary>
		[Column("F_REQUIRED")]
		[ELogProperty("Обязательное поле для заполнения")]
		public bool p_Required { get { return m_Required; } set { m_Required = value; } }

		/// <summary>Возможность редактирования</summary>
		[Column("F_EDITING")]
		[ELogProperty("Редактируемое поле")]
		public bool p_Editing { get { return m_Editing; } set { m_Editing = value; } }

		// <summary>Множественный выбор</summary>
		[Column("F_ISMULTI")]
		[ELogProperty("Множественный выбор")]
		public bool p_IsMultiSelect { get; set; }

		// <summary>Является числовым</summary>
		[Column("F_ISNUMBER")]
		[ELogProperty("Является числовым")]
		public bool p_IsNumber { get; set; }

		// <summary>Точность числа</summary>
		[Column("F_NUMROUND")]
		[ELogProperty("Точность числа")]
		public byte p_NumberRound { get; set; }

		// <summary>Формула числа</summary>
		[Column("F_NUMFORMULA", TypeName = "varchar")]
		[MaxLength(300)]
		[ELogProperty("Формула", IgnoreValue = true)]
		public string p_NumberFormula { get; set; }

		/// <summary>Видимость безусловная</summary>
		[Column("F_VISIBLE")]
		[ELogProperty("Видимое поле")]
		public bool p_Visible { get { return m_Visible; } set { m_Visible = value; } }

		/// <summary>Видимость для пациента</summary>
		[Column("F_VISIBLEPATIENT")]
		[ELogProperty("Видимое поле для пациента")]
		public bool p_VisiblePatient { get; set; }

		/// <summary>Подсказка по заполнению (отображается всплывающей подсказкой или сообщение по F1)</summary>
		[Column("F_HELP", TypeName = "varchar")]
		[MaxLength(500)]
		[ELogProperty("Подсказка")]
		public string p_Help { get; set; }

		/// <summary>Признак симметричности.
		/// Симметричность подразумевает повторение содержимого блока 2 раза (это используется для описания одинаковых правых и левых органов)
		/// с автоматически добавляемыми заголовками, указанными в его свойствах (напр., «справа» и «слева» или “OD” и “OS”)</summary>
		[Column("F_SYMMETRICAL")]
		[ELogProperty("Является симметричным")]
		public bool p_Symmetrical { get { return m_Symmetrical; } set { m_Symmetrical = value; } }

		/// <summary>Текст обозначения 1 стороны симметричности</summary>
		[Column("F_SYMMETRYPARAMLEFT", TypeName = "varchar")]
		[MaxLength(50)]
		[ELogProperty("Симметричность слева")]
		public string p_SymmetryParamLeft { get; set; }

		/// <summary>Текст обозначения 2 стороны симметричности</summary>
		[Column("F_SYMMETRYPARAMRIGHT", TypeName = "varchar")]
		[MaxLength(50)]
		[ELogProperty("Симметричность справа")]
		public string p_SymmetryParamRight { get; set; }

		/// <summary>Значение по умолчанию</summary>
		[Column("F_DEFAULT", TypeName = "varchar")]
		[MaxLength(100)]
		[ELogProperty("Значение по-умолчанию")]
		public string p_Default { get; set; }

		#region Часть
		/// <summary>Часть. Использование префикса</summary>
		[Column("F_ISPARTPRE")]
		[ELogProperty("Префикс")]
		public bool p_IsPartPre { get; set; }

		/// <summary>Часть. Значение префикса</summary>
		[Column("F_PARTPRE", TypeName = "varchar")]
		[MaxLength(100)]
		[ELogProperty("Значение префикса")]
		public string p_PartPre { get; set; }

		/// <summary>Часть. Использование постфикса</summary>
		[Column("F_ISPARTPOST")]
		[ELogProperty("Постфикс")]
		public bool p_IsPartPost { get; set; }

		/// <summary>Часть. Значение постфикса</summary>
		[Column("F_PARTPOST", TypeName = "varchar")]
		[MaxLength(100)]
		[ELogProperty("Значение постфикса")]
		public string p_PartPost { get; set; }

		/// <summary>Часть. Использование локации</summary>
		[Column("F_ISPARTLOCATIONS")]
		[ELogProperty("Локация")]
		public bool p_IsPartLocations { get; set; }

		/// <summary>Часть. Возможность множественного выбора локации</summary>
		[Column("F_ISPARTLOCATIONSMULTI")]
		[ELogProperty("Множественный выбор локации")]
		public bool p_IsPartLocationsMulti { get; set; }

		/// <summary>Часть. Использование нормы</summary>
		[Column("F_ISPARTNORM")]
		[ELogProperty("Норма")]
		public bool p_IsPartNorm { get; set; }

		/// <summary>Часть. Значение нормы</summary>
		[Column("F_PARTNORM")]
		[ELogProperty("Значение нормы")]
		public decimal p_PartNorm { get; set; }

		/// <summary>Часть. Использование нормы диапозон</summary>
		[Column("F_ISPARTNORMRANGE")]
		[ELogProperty("Норма диапазон")]
		public bool p_IsPartNormRange { get; set; }

		private List<Cl_AgeNorm> m_PartAgeNorms = new List<Cl_AgeNorm>();
		/// <summary>Часть. Список возрастных норм</summary>
		[ForeignKey("p_ElementID")]
		public List<Cl_AgeNorm> p_PartAgeNorms {
			get { return m_PartAgeNorms; }
			set { m_PartAgeNorms = value; }
		}
		#endregion

		/// <summary>Возможность ввода не стандартных значений</summary>
		[Column("F_ISCHANGENOTNORM")]
		public bool p_IsChangeNotNormValues { get; set; }

		private List<Cl_ElementsParams> m_ParamsValues = new List<Cl_ElementsParams>();
		/// <summary>Список значений параметров</summary>
		[ForeignKey("p_ElementID")]
		public List<Cl_ElementsParams> p_ParamsValues {
			get { return m_ParamsValues; }
			set { m_ParamsValues = value; }
		}

		/// <summary>Часть. Возможные локации</summary>
		public Cl_ElementsParams[] p_PartLocations {
			get { return m_ParamsValues.Where(p => p.p_TypeParam == Cl_ElementsParams.E_TypeParam.Location).ToArray(); }
		}

		/// <summary>Список стандартных нормальных значений</summary>
		public Cl_ElementsParams[] p_NormValues {
			get { return m_ParamsValues.Where(p => p.p_TypeParam == Cl_ElementsParams.E_TypeParam.NormValues).ToArray(); }
		}

		/// <summary>Список стандартных патологических значений</summary>
		public Cl_ElementsParams[] p_PatValues {
			get { return m_ParamsValues.Where(p => p.p_TypeParam == Cl_ElementsParams.E_TypeParam.PatValues).ToArray(); }
		}

		/// <summary>Условная видимость</summary>
		[Column("F_VISIBILITYFORMULA", TypeName = "varchar")]
		[MaxLength(1000)]
		[ELogProperty("Формула условной видимости", IgnoreValue = true)]
		public string p_VisibilityFormula { get; set; }

		/// <summary>Примечание</summary>
		[Column("F_COMMENT", TypeName = "varchar")]
		[MaxLength(100)]
		[ELogProperty("Примечание")]
		public string p_Comment { get; set; }

		/// <summary>ID группы элементов</summary>
		[Column("F_GROUP_ID")]
		[ForeignKey("p_ParentGroup")]
		public int p_ParentGroupID { get; set; }
		/// <summary>Группа элементов</summary>
		[ELogProperty("Изменилась группа", IsCustomDescription = true)]
		public Cl_Group p_ParentGroup { get; set; }

		/// <summary>Флаг нахождения элемента в архиве</summary>
		[Column("F_ISARHIVE")]
		public bool p_IsArhive { get; set; }

		/// <summary>Добавление значений для поля массива</summary>
		public void f_AddValues(Cl_ElementsParams.E_TypeParam a_TypeParam, string[] a_Values) {
			if (a_Values != null) {
				foreach (string val in a_Values) {
					if (!string.IsNullOrWhiteSpace(val))
						p_ParamsValues.Add(new Cl_ElementsParams() { p_ElementID = p_ID, p_TypeParam = a_TypeParam, p_Value = val });
				}
			}
		}
	}
}
