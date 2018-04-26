using Sadco.FamilyDoctor.Core.EntityLogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Sadco.FamilyDoctor.Core.Entities
{
    /// <summary>
    /// Класс элемента шаблона
    /// </summary>
    [Cl_ELogClass(E_EntityTypes.Elements)]
    [Table("T_ELEMENTS")]
    public class Cl_Element : I_Version, I_Delete, I_ELog
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

        /// <summary>
        /// Возвращает название элемента шаблона
        /// </summary>
        public string p_GetElementName {
            get {
                return (Attribute.GetCustomAttribute(p_ElementType.GetType().GetField(p_ElementType.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description;
            }
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
        private string m_IconImage = "";
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
        [Cl_ELogProperty("Название элемента")]
        public string p_Name { get; set; }

        /// <summary>Тэг элемента</summary>
        [Column("F_TAG", TypeName = "varchar")]
        [MaxLength(60)]
        [Cl_ELogProperty("Тег элемента")]
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
        [Cl_ELogProperty("Вид текстового элемента")]
        public E_ElementsTypes p_ElementType {
            get { return m_Types; }
            set {
                m_Types = value;
                switch (m_Types)
                {
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
        public bool f_IsText()
        {
            return p_ElementType == E_ElementsTypes.Float || p_ElementType == E_ElementsTypes.Line || p_ElementType == E_ElementsTypes.Bigbox;
        }

        /// <summary>Возвращает является ли текстовым элементом</summary>
        public bool f_IsImage()
        {
            return p_ElementType == E_ElementsTypes.Image;
        }

        /// <summary>Версия элемента шаблона</summary>
        [Column("F_VERSION")]
        public int p_Version { get; set; }

        /// <summary>Системное наименование иконки</summary>
        [NotMapped]
        public string p_IconName {
            get {
                return m_IconImage + (p_IsDelete ? "_DEL" : "");
            }
            set {
                m_IconImage = value;
            }
        }

        [NotMapped]
        /// <summary>Флаг многострочности текстового элемента</summary>
        public bool p_IsMulti { get; set; }

        /// <summary>Обязательность заполнения</summary>
        [Column("F_REQUIRED")]
        [Cl_ELogProperty("Обязательное поле для заполнения")]
        public bool p_Required { get { return m_Required; } set { m_Required = value; } }

        /// <summary>Возможность редактирования</summary>
        [Column("F_EDITING")]
        [Cl_ELogProperty("Редактируемое поле")]
        public bool p_Editing { get { return m_Editing; } set { m_Editing = value; } }

        // <summary>Множественный выбор</summary>
        [Column("F_ISMULTI")]
        [Cl_ELogProperty("Множественный выбор")]
        public bool p_IsMultiSelect { get; set; }

        // <summary>Является числовым</summary>
        [Column("F_ISNUMBER")]
        [Cl_ELogProperty("Является числовым")]
        public bool p_IsNumber { get; set; }

        // <summary>Точность числа</summary>
        [Column("F_NUMROUND")]
        [Cl_ELogProperty("Точность числа")]
        public byte p_NumberRound { get; set; }

        // <summary>Формула числа</summary>
        [Column("F_NUMFORMULA", TypeName = "varchar")]
        [MaxLength(300)]
        [Cl_ELogProperty("Формула", p_IgnoreValue = true)]
        public string p_NumberFormula { get; set; }

        /// <summary>Видимость безусловная</summary>
        [Column("F_VISIBLE")]
        [Cl_ELogProperty("Видимое поле")]
        public bool p_Visible { get { return m_Visible; } set { m_Visible = value; } }

        /// <summary>Видимость для пациента</summary>
        [Column("F_VISIBLEPATIENT")]
        [Cl_ELogProperty("Видимое поле для пациента")]
        public bool p_VisiblePatient { get; set; }

        /// <summary>Подсказка по заполнению (отображается всплывающей подсказкой или сообщение по F1)</summary>
        [Column("F_HELP", TypeName = "varchar")]
        [MaxLength(500)]
        [Cl_ELogProperty("Подсказка")]
        public string p_Help { get; set; }

        /// <summary>Признак симметричности.
        /// Симметричность подразумевает повторение содержимого блока 2 раза (это используется для описания одинаковых правых и левых органов)
        /// с автоматически добавляемыми заголовками, указанными в его свойствах (напр., «справа» и «слева» или “OD” и “OS”)</summary>
        [Column("F_SYMMETRICAL")]
        [Cl_ELogProperty("Является симметричным")]
        public bool p_Symmetrical { get { return m_Symmetrical; } set { m_Symmetrical = value; } }

        /// <summary>Текст обозначения 1 стороны симметричности</summary>
        [Column("F_SYMMETRYPARAMLEFT", TypeName = "varchar")]
        [MaxLength(50)]
        [Cl_ELogProperty("Симметричность слева")]
        public string p_SymmetryParamLeft { get; set; }

        /// <summary>Текст обозначения 2 стороны симметричности</summary>
        [Column("F_SYMMETRYPARAMRIGHT", TypeName = "varchar")]
        [MaxLength(50)]
        [Cl_ELogProperty("Симметричность справа")]
        public string p_SymmetryParamRight { get; set; }

        /// <summary>ID значения по умолчанию</summary>
        [Column("F_DEFAULT_ID")]
        [ForeignKey("p_Default")]
        public int? p_DefaultID { get; set; }
        /// <summary>Значение по умолчанию</summary>
        [Cl_ELogProperty("Значение по-умолчанию", p_IsCustomDescription = true)]
        public Cl_ElementParam p_Default { get; set; }

        #region Часть
        /// <summary>Часть. Использование префикса</summary>
        [Column("F_ISPARTPRE")]
        [Cl_ELogProperty("Префикс")]
        public bool p_IsPartPre { get; set; }

        /// <summary>Часть. Значение префикса</summary>
        [Column("F_PARTPRE", TypeName = "varchar")]
        [MaxLength(100)]
        [Cl_ELogProperty("Значение префикса")]
        public string p_PartPre { get; set; }

        /// <summary>Часть. Использование постфикса</summary>
        [Column("F_ISPARTPOST")]
        [Cl_ELogProperty("Постфикс")]
        public bool p_IsPartPost { get; set; }

        /// <summary>Часть. Значение постфикса</summary>
        [Column("F_PARTPOST", TypeName = "varchar")]
        [MaxLength(100)]
        [Cl_ELogProperty("Значение постфикса")]
        public string p_PartPost { get; set; }

        /// <summary>Часть. Использование локации</summary>
        [Column("F_ISPARTLOCATIONS")]
        [Cl_ELogProperty("Локация")]
        public bool p_IsPartLocations { get; set; }

        /// <summary>Часть. Возможность множественного выбора локации</summary>
        [Column("F_ISPARTLOCATIONSMULTI")]
        [Cl_ELogProperty("Множественный выбор локации")]
        public bool p_IsPartLocationsMulti { get; set; }

        /// <summary>Часть. Использование нормы</summary>
        [Column("F_ISPARTNORM")]
        [Cl_ELogProperty("Норма")]
        public bool p_IsPartNorm { get; set; }

        /// <summary>Часть. Значение нормы</summary>
        [Column("F_PARTNORM")]
        [Cl_ELogProperty("Значение нормы")]
        public decimal p_PartNorm { get; set; }

        /// <summary>Часть. Использование нормы диапозон</summary>
        [Column("F_ISPARTNORMRANGE")]
        [Cl_ELogProperty("Норма диапазон")]
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
        [Cl_ELogProperty("Ввод не стандартных значений")]
        public bool p_IsChangeNotNormValues { get; set; }

        private List<Cl_ElementParam> m_ParamsValues = new List<Cl_ElementParam>();
        /// <summary>Список значений параметров</summary>
        [ForeignKey("p_ElementID")]
        public List<Cl_ElementParam> p_ParamsValues {
            get { return m_ParamsValues; }
            set { m_ParamsValues = value; }
        }

        /// <summary>Часть. Возможные локации</summary>
        [Cl_ELogProperty("Изменился набор значений для поля \"Локация\"", p_IsCustomDescription = true, p_IsNewValueOnly = true)]
        public Cl_ElementParam[] p_PartLocations {
            get { return m_ParamsValues.Where(p => p.p_TypeParam == Cl_ElementParam.E_TypeParam.Location).ToArray(); }
        }

        /// <summary>Список стандартных нормальных значений</summary>
        [Cl_ELogProperty("Изменился набор значений для поля \"Нормальные значения\"", p_IsCustomDescription = true, p_IsNewValueOnly = true)]
        public Cl_ElementParam[] p_NormValues {
            get { return m_ParamsValues.Where(p => p.p_TypeParam == Cl_ElementParam.E_TypeParam.NormValues).ToArray(); }
        }

        /// <summary>Список стандартных патологических значений</summary>
        [Cl_ELogProperty("Изменился набор значений для поля \"Паталогические значения\"", p_IsCustomDescription = true, p_IsNewValueOnly = true)]
        public Cl_ElementParam[] p_PatValues {
            get { return m_ParamsValues.Where(p => p.p_TypeParam == Cl_ElementParam.E_TypeParam.PatValues).ToArray(); }
        }

        /// <summary>Условная видимость</summary>
        [Column("F_VISIBILITYFORMULA", TypeName = "varchar")]
        [MaxLength(1000)]
        [Cl_ELogProperty("Формула условной видимости", p_IgnoreValue = true)]
        public string p_VisibilityFormula { get; set; }

        /// <summary>Примечание</summary>
        [Column("F_COMMENT", TypeName = "varchar")]
        [MaxLength(100)]
        [Cl_ELogProperty("Примечание")]
        public string p_Comment { get; set; }

        /// <summary>ID группы элементов</summary>
        [Column("F_GROUP_ID")]
        [ForeignKey("p_ParentGroup")]
        public int p_ParentGroupID { get; set; }
        /// <summary>Группа элементов</summary>
        [Cl_ELogProperty("Изменилась группа", p_IsCustomDescription = true)]
        public Cl_Group p_ParentGroup { get; set; }

        /// <summary>Флаг нахождения элемента в удалении</summary>
        [Column("F_ISDEL")]
        [Cl_ELogProperty("Элемент удален", p_IsCustomDescription = true, p_IgnoreValue = true)]
        public bool p_IsDelete { get; set; }

        /// <summary>Данные рисунка</summary>
        [Column("F_IMAGE")]
        [Cl_ELogProperty("Изменился рисунок", p_IsCustomDescription = true, p_IgnoreValue = true)]
        public byte[] p_ImageBytes { get; set; }
        [NotMapped]
        /// <summary>Рисунок</summary>
        public Image p_Image {
            get {
                if (p_ImageBytes != null && p_ImageBytes.Length > 0)
                {
                    MemoryStream ms = new MemoryStream(p_ImageBytes);
                    return Image.FromStream(ms);
                }
                return null;
            }
            set {
                if (value != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        value.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        p_ImageBytes = ms.ToArray();
                    }
                }
                else
                {
                    p_ImageBytes = null;
                }
            }
        }

        /// <summary>Добавление значений для поля массива</summary>
        public void f_AddValues(Cl_ElementParam.E_TypeParam a_TypeParam, string[] a_Values)
        {
            if (a_Values != null)
            {
                foreach (string val in a_Values)
                {
                    if (!string.IsNullOrWhiteSpace(val))
                        p_ParamsValues.Add(new Cl_ElementParam() { p_ElementID = p_ID, p_TypeParam = a_TypeParam, p_Value = val });
                }
            }
        }

        /// <summary>Возвращает уникальный ID элемента</summary>
        int I_ELog.p_GetLogEntityID => this.p_ElementID;

        /// <summary>Является ли значение элемента текстом пользователя</summary>
        public bool p_IsTextUser {
            get {
                return p_IsChangeNotNormValues || ((p_NormValues == null || p_NormValues.Length == 0) && (p_NormValues == null || p_NormValues.Length == 0));
            }
        }

        /// <summary>Является ли значение элемента из справочника</summary>
        public bool p_IsTextFromCatalog {
            get {
                return !p_IsTextUser;
            }
        }

        /// <summary>Является ли значение элемента текстом</summary>
        public bool p_IsText {
            get {
                return p_ElementType == E_ElementsTypes.Float || p_ElementType == E_ElementsTypes.Line || p_ElementType == E_ElementsTypes.Bigbox;
            }
        }

        /// <summary>Является ли значение элемента текстом</summary>
        public bool p_IsImage {
            get {
                return p_ElementType == E_ElementsTypes.Image;
            }
        }
    }
}
