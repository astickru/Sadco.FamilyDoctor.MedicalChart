using Sadco.FamilyDoctor.Core.EntityLogs;
using Sadco.FamilyDoctor.Core.Facades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Sadco.FamilyDoctor.Core.Entities
{
    /// <summary>
    /// Класс сущности значения записи
    /// </summary>
    [Table("T_RECORDSVALUES")]
    public class Cl_RecordValue : Cl_RecordValueBase
    {
        // <summary>ID записи</summary>
        [Column("F_RECORD_ID")]
        [ForeignKey("p_Record")]
        public int p_RecordID { get; set; }
        /// <summary>Запись</summary>
        public Cl_Record p_Record { get; set; }

        private List<Cl_RecordParam> m_Params = new List<Cl_RecordParam>();
        /// <summary>Список параметров элементов</summary>
        [ForeignKey("p_ElementID")]
        public List<Cl_RecordParam> p_Params {
            get { return m_Params; }
            set { m_Params = value; }
        }

        /// <summary>Получение записи</summary>
        public override I_Record f_GetRecord()
        {
            return p_Record;
        }

        public override IEnumerable<I_RecordParam> f_GetParams()
        {
            return m_Params;
        }
    }
}
