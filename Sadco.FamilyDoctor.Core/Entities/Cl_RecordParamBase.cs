using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sadco.FamilyDoctor.Core.Entities
{
    /// <summary>Базовый класс для параметра записи</summary>
    public abstract class Cl_RecordParamBase : I_RecordParam, I_Comparable
    {
        /// <summary>ID параметра записи</summary>
        [Key]
        [Column("F_ID")]
        public int p_ID { get; set; }

        /// <summary>ID параметра элемента записи</summary>
        [Column("F_ELPRM_ID")]
        [ForeignKey("p_ElementParam")]
        public int p_ElementParamID { get; set; }
        /// <summary>Параметр элемента записи</summary>
        public Cl_ElementParam p_ElementParam { get; set; }

        /// <summary>Признак принадлежности к дополнительному параметру</summary>
        [Column("F_ISDOP")]
        public bool p_IsDop { get; set; }

        public bool f_Equals(object a_Value)
        {
            if (a_Value == null || !(a_Value.GetType() == this.GetType()))
                return false;
            return p_ElementParam.p_Value == ((Cl_RecordParam)a_Value).p_ElementParam.p_Value;
        }
    }
}
