using Sadco.FamilyDoctor.Core.EntityLogs;
using Sadco.FamilyDoctor.Core.Permision;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sadco.FamilyDoctor.Core.Entities
{
    /// <summary>
    /// Класс сущности медкарты
    /// </summary>
    [Table("T_MEDICALCARD")]
    public class Cl_MedicalCard : I_Delete
    {
        /// <summary>Ключ медкарты</summary>
        [Key]
        [Column("F_ID")]
        public int p_ID { get; set; }

        /// <summary>Номер медкарты</summary>
        [Column("F_NUMBER", TypeName = "varchar")]
        [MaxLength(50)]
        [Description("Номер медкарты")]
        public string p_Number { get; set; }

        /// <summary>Время создания медкарты</summary>
        [Column("F_DATECREATE")]
        [Description("Время создания медкарты")]
        public DateTime p_DateCreate { get; set; }

        /// <summary>Время архивирования медкарты</summary>
        [Column("F_DATEARCHIVE")]
        [Description("Время архивирования медкарты")]
        public DateTime? p_DateArchive { get; set; }

        /// <summary>Флаг архив</summary>
        [Description("Флаг архив")]
        public bool p_IsArchive { get { return p_DateArchive != null; } }

        /// <summary>Время удаления медкарты</summary>
        [Column("F_DATEDELETE")]
        [Description("Время удаления медкарты")]
        public DateTime? p_DateDelete { get; set; }

        /// <summary>Флаг нахождения медкарты в удалении</summary>
        [Description("Флаг нахождения медкарты в удалении")]
        public bool p_IsDelete { get { return p_DateDelete != null; } }

        /// <summary>Время объединения медкарты</summary>
        [Column("F_DATEMERGE")]
        [Description("Время объединения медкарты")]
        public DateTime? p_DateMerge { get; set; }

        /// <summary>ID пациента</summary>
        [Column("F_PATIENT_ID")]
        [Description("ID пациента")]
        public int p_PatientID { get; set; }

        /// <summary>GUID пациента</summary>
        [Column("F_PATIENT_UID")]
        [Description("GUID пациента")]
        public Guid? p_PatientUID { get; set; }

        /// <summary>Пол пациента</summary>
        [Column("F_GENDER")]
        [Description("Пол пациента")]
        public Cl_User.E_Sex p_PatientSex { get; set; }

        /// <summary>Имя пациента</summary>
        [Column("F_PATIENT_NAME")]
        [Description("Имя пациента")]
        public string p_PatientName { get; set; }

        /// <summary>Фамиля пациента</summary>
        [Column("F_PATIENT_SURNAME")]
        [Description("Фамиля пациента")]
        public string p_PatientSurName { get; set; }

        /// <summary>Отчество пациента</summary>
        [Column("F_PATIENT_LASTNAME")]
        [Description("Отчество пациента")]
        public string p_PatientLastName { get; set; }

        /// <summary>Дата рождения пациента</summary>
        [Column("F_PATIENT_DATEBIRTH")]
        [Description("Дата рождения пациента")]
        public DateTime p_PatientDateBirth { get; set; }

        /// <summary>Комментарий</summary>
        [Column("F_COMMENT")]
        [Description("Комментарий")]
        public string p_Comment { get; set; }

        /// <summary>Инициалы пациента</summary>
        [NotMapped]
        public string p_PatientFIO { get { return f_GetPatientInitials(); } }
        /// <summary>Возвращает инициалы пациента</summary>
        public string f_GetPatientInitials()
        {
            return string.Format("{0} {1} {2}", p_PatientSurName, string.IsNullOrWhiteSpace(p_PatientName) ? "" : p_PatientName[0].ToString() + ".", string.IsNullOrWhiteSpace(p_PatientLastName) ? "" : p_PatientLastName[0].ToString() + ".");
        }

        /// <summary>Дата первой печати обложки</summary>
        [Column("F_DATEPRINTTITLE")]
        [Description("Дата первой печати обложки")]
        public DateTime? p_DatePrintTitle { get; set; }

        /// <summary>Флаг печати обложки</summary>
        public bool p_IsPrintTitle {
            get {
                return p_DatePrintTitle != null;
            }
        }

        /// <summary>Возвращает возраст пациента</summary>
        public byte f_GetPatientAgeByYear(DateTime date)
        {
            byte age = 0;
            if (p_PatientDateBirth != null)
            {
                byte year = (byte)(date.Year - p_PatientDateBirth.Year);
                if (date.Month < p_PatientDateBirth.Month ||
                    (date.Month == p_PatientDateBirth.Month && date.Day < p_PatientDateBirth.Day)) year--;
                return year;
            }
            return age;
        }

        /// <summary>Возвращает возраст пациента</summary>
        public int f_GetPatientAgeByMonth(DateTime date)
        {
            int age = 0;
            if (p_PatientDateBirth != null)
            {
                int day = (date.Day - p_PatientDateBirth.Day);
                int month = (date.Month - p_PatientDateBirth.Month);
                int year = (date.Year - p_PatientDateBirth.Year);
                if (month < 0)
                {
                    year--;
                    month = 12 + month;
                }
                if (day < 0)
                {
                    month--;
                }
                return year * 12 + month;
            }
            return age;
        }

        /// <summary>Возвращает возраст пациента</summary>
        public string f_GetPatientAgeByMonthText(DateTime date)
        {
            var months = f_GetPatientAgeByMonth(date);
            return $"{months / 12} лет, {months % 12} мес.";
        }

        /// <summary>Установка пациента</summary>
        public void f_SetPatient(Cl_User a_User)
        {
            p_PatientID = a_User.p_UserID;
            p_PatientSurName = a_User.p_UserSurName;
            p_PatientName = a_User.p_UserName;
            p_PatientLastName = a_User.p_UserLastName;
            p_PatientSex = a_User.p_Sex;
            p_PatientDateBirth = a_User.p_DateBirth;
        }
    }
}
