using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Sadco.FamilyDoctor.Core.Entities
{
    /// <summary>
    /// Класс сущности значения паттерна записи
    /// </summary>
    [Table("T_RECORDSPATTERNSVALUES")]
    public class Cl_RecordPatternValue : Cl_RecordValueBase
    {
        // <summary>ID паттерна записи</summary>
        [Column("F_RECORDPATTERN_ID")]
        [ForeignKey("p_RecordPattern")]
        public int p_RecordPatternID { get; set; }
        /// <summary>Паттерн записи</summary>
        public Cl_RecordPattern p_RecordPattern { get; set; }

        private List<Cl_RecordPatternParam> m_Params = new List<Cl_RecordPatternParam>();
        /// <summary>Список параметров элементов</summary>
        [ForeignKey("p_ElementID")]
        public List<Cl_RecordPatternParam> p_Params {
            get { return m_Params; }
            set { m_Params = value; }
        }

        /// <summary>Локации</summary>
        public Cl_RecordPatternParam[] p_PartLocations {
            get { return p_Params.Where(p => p.p_ElementParam.p_TypeParam == Cl_ElementParam.E_TypeParam.Location).ToArray(); }
        }

        /// <summary>Значения из справочника</summary>
        public Cl_RecordPatternParam[] p_ValuesCatalog {
            get { return p_Params.Where(p => !p.p_IsDop && (p.p_ElementParam.p_TypeParam == Cl_ElementParam.E_TypeParam.NormValues || p.p_ElementParam.p_TypeParam == Cl_ElementParam.E_TypeParam.PatValues)).ToArray(); }
        }

        /// <summary>Дополнительные значения из справочника</summary>
        public Cl_RecordPatternParam[] p_ValuesDopCatalog {
            get { return p_Params.Where(p => p.p_IsDop && (p.p_ElementParam.p_TypeParam == Cl_ElementParam.E_TypeParam.NormValues || p.p_ElementParam.p_TypeParam == Cl_ElementParam.E_TypeParam.PatValues)).ToArray(); }
        }
    }
}
