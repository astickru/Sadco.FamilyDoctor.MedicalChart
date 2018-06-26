using Sadco.FamilyDoctor.Core.EntityLogs;
using Sadco.FamilyDoctor.Core.Permision;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sadco.FamilyDoctor.Core.Entities
{
    /// <summary>
    /// Абстрактный класс для записи
    /// </summary>
    public abstract class Cl_RecordBase
    {
        /// <summary>Ключ записи</summary>
        [Key]
        [Column("F_ID")]
        public int p_ID { get; set; }

        /// <summary>Заголовок записи</summary>
        [Column("F_TITLE")]
        public string p_Title { get; set; }

        /// <summary>Название клиники</summary>
        [Column("F_CLINIKNAME")]
        public string p_ClinikName { get; set; }

        /// <summary>ID пользователя</summary>
        [Column("F_USER_ID")]
        public int p_DoctorID { get; set; }

        /// <summary>Имя пользователя</summary>
        [Column("F_USER_NAME")]
        public string p_DoctorName { get; set; }

        /// <summary>Фамиля пользователя</summary>
        [Column("F_USER_SURNAME")]
        public string p_DoctorSurName { get; set; }

        /// <summary>Отчество пользователя</summary>
        [Column("F_USER_LASTNAME")]
        public string p_DoctorLastName { get; set; }

        /// <summary>Инициалы пользователя</summary>
        [NotMapped]
        public string p_DoctorFIO { get { return f_GetDoctorInitials(); } }
        /// <summary>Возвращает инициалы пользователя</summary>
        public string f_GetDoctorInitials()
        {
            return string.Format("{0} {1} {2}", p_DoctorSurName, string.IsNullOrWhiteSpace(p_DoctorName) ? "" : p_DoctorName[0].ToString() + ".", string.IsNullOrWhiteSpace(p_DoctorLastName) ? "" : p_DoctorLastName[0].ToString() + ".");
        }

        /// <summary>Установка пользователя</summary>
        public void f_SetDoctor(Cl_User a_User)
        {
            p_ClinikName = a_User.p_ClinikName;
            p_DoctorID = a_User.p_UserID;
            p_DoctorSurName = a_User.p_UserSurName;
            p_DoctorName = a_User.p_UserName;
            p_DoctorLastName = a_User.p_UserLastName;
        }

        /// <summary>ID общей категории</summary>
        [Column("F_CATEGORYTOTAL_ID")]
        [ForeignKey("p_CategoryTotal")]
        public int? p_CategoryTotalID { get; set; }
        private Cl_Category m_CategoryTotal = null;
        /// <summary>Общая категория шаблонов</summary>
        public Cl_Category p_CategoryTotal {
            get { return m_CategoryTotal; }
            set {
                m_CategoryTotal = value;
                if (m_CategoryTotal != null)
                    m_CategoryTotal.p_Type = Cl_Category.E_CategoriesTypes.Total;
            }
        }

        /// <summary>ID клинической категории</summary>
        [Column("F_CATEGORYCLINIK_ID")]
        [ForeignKey("p_CategoryClinik")]
        public int? p_CategoryClinikID { get; set; }
        private Cl_Category m_CategoryClinik = null;
        /// <summary>Клиническая категория шаблонов</summary>
        public Cl_Category p_CategoryClinik {
            get { return m_CategoryClinik; }
            set {
                m_CategoryClinik = value;
                if (m_CategoryClinik != null)
                    m_CategoryClinik.p_Type = Cl_Category.E_CategoriesTypes.Clinik;
            }
        }

        /// <summary>ID шаблона</summary>
        [Column("F_TEMPLATE_ID")]
        [ForeignKey("p_Template")]
        public int? p_TemplateID { get; set; }
        /// <summary>Шаблон</summary>
        public Cl_Template p_Template { get; set; }

        /// <summary>Установка шаблона</summary>
        public void f_SetTemplate(Cl_Template a_Template)
        {
            p_Template = a_Template;
            if (a_Template != null)
            {
                p_Title = a_Template.p_Title;
                p_CategoryTotalID = a_Template.p_CategoryTotalID;
                p_CategoryTotal = a_Template.p_CategoryTotal;
                p_CategoryClinikID = a_Template.p_CategoryClinikID;
                p_CategoryClinik = a_Template.p_CategoryClinik;
            }
        }
    }
}
