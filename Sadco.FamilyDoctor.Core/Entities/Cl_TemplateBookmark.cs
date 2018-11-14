using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Sadco.FamilyDoctor.Core.Entities
{
    /// <summary>
	/// Класс элемента шаблона
	/// </summary>
    [Table("T_TEMPLATESBOOKMARKS")]
    public class Cl_TemplateBookmark : I_Comparable
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

        /// <summary>Заголовок</summary>
        [Column("F_BOOKMARK")]
        public E_Bookmarks p_Bookmark { get; set; }

        /// <summary>Текст заголовка</summary>
        [Column("F_TEXT")]
        public string p_Text { get; set; }

        /// <summary>Индекс позиции заголовка в шаблоне</summary>
        [Column("F_INDEX")]
        public int p_Index { get; set; }

        public bool f_Equals(object a_Value)
        {
            if (a_Value == null || !(a_Value.GetType() == this.GetType()))
                return false;
            var elm = (Cl_TemplateBookmark)a_Value;
            return p_Bookmark == elm.p_Bookmark && p_Text == elm.p_Text;
        }
    }
}
