using System.ComponentModel.DataAnnotations.Schema;

namespace Sadco.FamilyDoctor.Core.Entities
{
    /// <summary>Класс параметра паттерна записи</summary>
    [Table("T_RECORDSPATTERNSPRMS")]
    public class Cl_RecordPatternParam : Cl_RecordParamBase, I_Comparable
    {
        /// <summary>ID параметра паттерна записи</summary>
        [Column("F_RECORDPATTERNPRM_ID")]
        [ForeignKey("p_RecordPatternValue")]
        public int p_RecordPatternValueID { get; set; }
        /// <summary>Значение параметра паттерна записи</summary>
        public Cl_RecordPatternValue p_RecordPatternValue { get; set; }
    }
}
