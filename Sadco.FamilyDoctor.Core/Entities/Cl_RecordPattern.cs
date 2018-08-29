using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sadco.FamilyDoctor.Core.Entities
{
    /// <summary>
    /// Класс сущности паттерна записи
    /// </summary>
    [Table("T_RECORDS_PATTERNS")]
    public class Cl_RecordPattern : Cl_RecordBase
    {
        /// <summary>Наименование паттерна записей</summary>
        [Column("F_NAME")]
        public string p_Name { get; set; }

        private List<Cl_RecordPatternValue> m_Values = new List<Cl_RecordPatternValue>();
        /// <summary>Список значений элементов записи</summary>
        [ForeignKey("p_RecordPatternID")]
        public List<Cl_RecordPatternValue> p_Values {
            get { return m_Values; }
            set { m_Values = value; }
        }

        public override IEnumerable<I_RecordValue> f_GetRecordsValues()
        {
            return p_Values;
        }
    }
}
