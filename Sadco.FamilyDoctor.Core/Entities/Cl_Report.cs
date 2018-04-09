using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Sadco.FamilyDoctor.Core.Entities
{
    /// <summary>
    /// Класс записи
    /// </summary>
    //[Table("T_RECORD")]
    public class Cl_Record
    {
        /// <summary>Ключ записи</summary>
        [Key]
        [Column("F_ID")]
        public int p_ID { get; set; }

        /// <summary>ID шаблона</summary>
        [Column("F_TEMPLATE_ID")]
        [ForeignKey("p_Template")]
        public int p_TemplateID { get; set; }
        /// <summary>Шаблон</summary>
        public Cl_Template p_Template { get; set; }


    }
}
