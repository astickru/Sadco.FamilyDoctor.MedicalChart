using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Sadco.FamilyDoctor.Core.Entities
{
    [Table("T_TEMPLATESELEMENTS")]
    public class Cl_TemplatesElements
    {
        [Column("F_ID")]
        [Key]
        public int p_ID { get; set; }

        [Column("F_CONTROL_ID")]
        public int p_ControlID { get; set; }

        [Column("F_TEMPLATE_ID")]
        [ForeignKey("p_Template")]
        public int p_TemplateID { get; set; }
        public Cl_Template p_Template { get; set; }

        [Column("F_CONTROL_TYPE", TypeName = "varchar")]
        public string p_ControlType { get; set; }

        [Column("F_POSX")]
        public int p_PositionX { get; set; }

        [Column("F_POSY")]
        public int p_PositionY { get; set; }

        [Column("F_WIDTH")]
        public int p_Width { get; set; }

        [Column("F_HEIGHT")]
        public int p_Height { get; set; }

        private Cl_Element m_Element = null;
        public Cl_Element p_Element {
            get {
                if (m_Element != null) return m_Element;
                if (!Cl_App.m_DataContext.f_GetAvailableControls().ContainsKey(p_ControlType)) return null;
                return m_Element = Cl_App.m_DataContext.p_Elements.FirstOrDefault(e => e.p_ID == p_ControlID);
            }
            set {
                m_Element = value;
                p_ControlType = value.GetType().Name;
                p_ControlID = value.p_ID;
            }
        }
    }
}
