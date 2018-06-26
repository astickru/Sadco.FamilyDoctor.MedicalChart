using System.ComponentModel.DataAnnotations.Schema;

namespace Sadco.FamilyDoctor.Core.Entities
{
    /// <summary>Класс параметра записи</summary>
    [Table("T_RECORDSPRMS")]
    public class Cl_RecordParam : Cl_RecordParamBase, I_Comparable
    {
        /// <summary>ID параметра записи</summary>
        [Column("F_RECORDVAL_ID")]
        [ForeignKey("p_RecordValue")]
        public int p_RecordValueID { get; set; }
        /// <summary>Значение параметра записи</summary>
        public Cl_RecordValue p_RecordValue { get; set; }
    }
}
