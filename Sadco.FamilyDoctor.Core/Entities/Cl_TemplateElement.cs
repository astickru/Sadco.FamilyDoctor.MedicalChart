using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Sadco.FamilyDoctor.Core.Entities
{
    /// <summary>
	/// Класс элемента шаблона
	/// </summary>
    [Table("T_TEMPLATESELEMENTS")]
    public class Cl_TemplateElement
    {
        [Column("F_ID")]
        [Key]
        public int p_ID { get; set; }

        /// <summary>ID шаблона</summary>
        [Column("F_TEMPLATE_ID")]
        [ForeignKey("p_Template")]
        public int p_TemplateID { get; set; }
        /// <summary>Шаблон</summary>
        public Cl_Template p_Template { get; set; }

        /// <summary>ID элемента</summary>
        [Column("F_CHILDELEMENT_ID")]
        [ForeignKey("p_ChildElement")]
        public int? p_ChildElementID { get; set; }
        /// <summary>Элемент</summary>
        public Cl_Element p_ChildElement { get; set; }

        /// <summary>ID элемента</summary>
        [Column("F_CHILDTEMPLATE_ID")]
        [ForeignKey("p_ChildTemplate")]
        public int? p_ChildTemplateID { get; set; }
        /// <summary>Элемент</summary>
        public Cl_Template p_ChildTemplate { get; set; }

        /// <summary>Индекс позиции элемента в шаблоне</summary>
        [Column("F_INDEX")]
        public int p_Index { get; set; }
    }
}
