using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Sadco.FamilyDoctor.Core.Entities
{
    /// <summary>
    /// Класс возрастной нормы текстового элемента шаблона
    /// </summary>
    [Table("T_AGENORMS")]
    public class Cl_AgeNorm
    {
        /// <summary>Ключ возрастной нормы</summary>
        [Key]
        [Column("F_ID")]
        public int p_ID { get; set; }

        /// <summary>ID элемента</summary>
        [Column("F_ELEMENT_ID")]
        [ForeignKey("p_Element")]
        public int p_ElementID { get; set; }
        /// <summary>Элемент</summary>
        public Cl_Element p_Element { get; set; }

        /// <summary>Возраст от</summary>
        [Column("F_AGEFROM")]
        public byte p_AgeFrom { get; set; }

        /// <summary>Возраст до</summary>
        [Column("F_AGETO")]
        public byte p_AgeTo { get; set; }

        /// <summary>Муж мин</summary>
        [Column("F_MALEMIN")]
        public decimal p_MaleMin { get; set; }

        /// <summary>Муж макс</summary>
        [Column("F_MALEMAX")]
        public decimal p_MaleMax { get; set; }

        /// <summary>Жен мин</summary>
        [Column("F_FEMALEMIN")]
        public decimal p_FemaleMin { get; set; }

        /// <summary>Жен макс</summary>
        [Column("F_FEMALEMAX")]
        public decimal p_FemaleMax { get; set; }
    }
}
