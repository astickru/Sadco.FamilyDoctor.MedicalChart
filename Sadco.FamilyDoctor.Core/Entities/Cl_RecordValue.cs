using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sadco.FamilyDoctor.Core.Entities
{
    /// <summary>
    /// Класс сущности значения записи
    /// </summary>
    [Table("T_RECORDSVALUES")]
    public class Cl_RecordValue
    {
        /// <summary>ID значения записи</summary>
        [Key]
        [Column("F_ID")]
        public int p_ID { get; set; }

        // <summary>ID записи</summary>
        [Column("F_RECORD_ID")]
        [ForeignKey("p_Record")]
        public int p_RecordID { get; set; }
        /// <summary>Запись</summary>
        public Cl_Record p_Record { get; set; }

        /// <summary>ID элемента</summary>
        [Column("F_ELEMENT_ID")]
        [ForeignKey("p_Element")]
        public int p_ElementID { get; set; }
        /// <summary>Элемент</summary>
        public Cl_Element p_Element { get; set; }

        /// <summary>ID локации из справочника</summary>
        [Column("F_LOCATION_ID")]
        [ForeignKey("p_LocationCatalog")]
        public int? p_LocationCatalogID { get; set; }
        /// <summary>Локация из справочника</summary>
        public Cl_ElementsParams p_LocationCatalog { get; set; }

        /// <summary>Произвольное локация</summary>
        [Column("F_LOCATION")]
        public string p_LocationUser { get; set; }

        /// <summary>ID значения из справочника</summary>
        [Column("F_VALUE_ID")]
        [ForeignKey("p_ValueCatalog")]
        public int? p_ValueCatalogID { get; set; }
        /// <summary>Значение из справочника</summary>
        public Cl_ElementsParams p_ValueCatalog { get; set; }

        /// <summary>Произвольное значение</summary>
        [Column("F_VALUE")]
        public string p_ValueUser { get; set; }

        /// <summary>ID дополнительного значения из справочника</summary>
        [Column("F_VALUEDOP_ID")]
        [ForeignKey("p_ValueCatalogDop")]
        public int? p_ValueCatalogDopID { get; set; }
        /// <summary>Дополнительное значение из справочника</summary>
        public Cl_ElementsParams p_ValueCatalogDop { get; set; }

        /// <summary>Дополнительное значение</summary>
        [Column("F_VALUEDOP")]
        public string p_ValueUserDop { get; set; }
    }
}
