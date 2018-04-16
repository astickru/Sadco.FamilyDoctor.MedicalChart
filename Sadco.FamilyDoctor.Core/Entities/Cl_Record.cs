using Sadco.FamilyDoctor.Core.EntityLogs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sadco.FamilyDoctor.Core.Entities
{
    /// <summary>
    /// Класс сущности записи
    /// </summary>
    [Table("T_RECORDS")]
    public class Cl_Record : I_Version, I_Delete, I_ELog
    {
        public enum E_Sex : byte
        {
            /// <summary>Мужчина</summary>
            Man,
            /// <summary>Женьщина</summary>
            Female
        }

        /// <summary>Ключ записи</summary>
        [Key]
        [Column("F_ID")]
        public int p_ID { get; set; }

        /// <summary>ID записи для всех версий</summary>
        [Column("F_RECORD_ID")]
        public int p_RecordID { get; set; }

        /// <summary>Версия записи</summary>
        [Column("F_VERSION")]
        public int p_Version { get; set; }

        /// <summary>Флаг нахождения записи в удалении</summary>
        [Column("F_ISDEL")]
        [ELogProperty("Запись удалена", IsCustomDescription = true, IgnoreValue = true)]
        public bool p_IsDelete { get; set; }

        /// <summary>Возвращает уникальный ID записи</summary>
        int I_ELog.p_GetLogEntityID => this.p_RecordID;

        /// <summary>Пол</summary>
        [Column("F_SEX")]
        public E_Sex p_Sex { get; set; }

        /// <summary>Дата рождения</summary>
        [Column("F_DATEBIRTH", TypeName = "Date")]
        public DateTime p_DateBirth { get; set; }

        /// <summary>Время формирования записи</summary>
        [Column("F_DATEFORMING")]
        public DateTime p_DateForming { get; set; }

        /// <summary>Время создания записи</summary>
        [Column("F_DATECREATE")]
        public DateTime p_DateCreate { get; set; }

        /// <summary>ID медицинской карты</summary>
        [Column("F_CARD_ID")]
        public int p_MedicalCardID { get; set; }

        /// <summary>Флаг архив</summary>
        [Column("F_ISARCHIVE")]
        public bool p_IsArchive { get; set; }

        /// <summary>Флаг печати</summary>
        [Column("F_ISPRINT")]
        public bool p_IsPrint { get; set; }

        /// <summary>Флаг автомата</summary>
        [Column("F_ISAUTIMATIC")]
        public bool p_IsAutimatic { get; set; }

        /// <summary>ID пользователя</summary>
        [Column("F_USER_ID")]
        public int p_UserID { get; set; }

        /// <summary>Имя пользователя</summary>
        [Column("F_USER_NAME")]
        public string p_UserName { get; set; }

        /// <summary>Фамиля пользователя</summary>
        [Column("F_USER_SURNAME")]
        public string p_UserSurName { get; set; }

        /// <summary>Отчество пользователя</summary>
        [Column("F_USER_LASTNAME")]
        public string p_UserLastName { get; set; }

        /// <summary>ID пациента</summary>
        [Column("F_PATIENT_ID")]
        public int p_PatientID { get; set; }

        /// <summary>Имя пациента</summary>
        [Column("F_PATIENT_NAME")]
        public string p_PatientName { get; set; }

        /// <summary>Фамиля пациента</summary>
        [Column("F_PATIENT_SURNAME")]
        public string p_PatientSurName { get; set; }

        /// <summary>Отчество пациента</summary>
        [Column("F_PATIENT_LASTNAME")]
        public string p_PatientLastName { get; set; }

        /// <summary>ID шаблона</summary>
        [Column("F_TEMPLATE_ID")]
        [ForeignKey("p_Template")]
        public int p_TemplateID { get; set; }
        /// <summary>Шаблон</summary>
        public Cl_Template p_Template { get; set; }

        private List<Cl_RecordValue> m_Values = new List<Cl_RecordValue>();
        /// <summary>Список значений элементов записи</summary>
        [ForeignKey("p_RecordID")]
        public List<Cl_RecordValue> p_Values {
            get { return m_Values; }
            set { m_Values = value; }
        }

        /// <summary>Инициалы пользователя</summary>
        [NotMapped]
        public string p_UserFIO { get { return f_GetUserInitials(); } }
        /// <summary>Возвращает инициалы пользователя</summary>
        public string f_GetUserInitials()
        {
            return string.Format("{0} {1} {2}", p_UserSurName, string.IsNullOrWhiteSpace(p_UserName) ? "" : p_UserName[0].ToString() + ".", string.IsNullOrWhiteSpace(p_UserLastName) ? "" : p_UserLastName[0].ToString() + ".");
        }

        /// <summary>Инициалы пациента</summary>
        [NotMapped]
        public string p_PatientFIO { get { return f_GetPatientInitials(); } }
        /// <summary>Возвращает инициалы пациента</summary>
        public string f_GetPatientInitials()
        {
            return string.Format("{0} {1} {2}", p_PatientSurName, string.IsNullOrWhiteSpace(p_PatientName) ? "" : p_PatientName[0].ToString() + ".", string.IsNullOrWhiteSpace(p_PatientLastName) ? "" : p_PatientLastName[0].ToString() + ".");
        }
    }
}
