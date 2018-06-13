using Sadco.FamilyDoctor.Core.EntityLogs;
using Sadco.FamilyDoctor.Core.Permision;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sadco.FamilyDoctor.Core.Entities
{
    /// <summary>
    /// Класс сущности записи
    /// </summary>
    [Cl_ELogClass(E_EntityTypes.Records)]
    [Table("T_RECORDS")]
    public class Cl_Record : I_Version, I_Delete, I_ELog
    {
        /// <summary>
        /// Типы записей
        /// </summary>
        public enum E_RecordType : byte
        {
            /// <summary>По шаблону</summary>
            ByTemplate,
            /// <summary>Готовый файл</summary>
            FinishedFile
        }

        /// <summary>
        /// Типы вложенных файлов записей
        /// </summary>
        public enum E_RecordFileType : byte
        {
            HTML,
            PDF,
            JPG,
            JPEG,
            JPE,
            JFIF,
            JIF,
            PNG,
            GIF,
            XML
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
        [Cl_ELogProperty("Запись удалена", p_IsCustomDescription = true, p_IgnoreValue = true)]
        public bool p_IsDelete { get; set; }

        /// <summary>Возвращает уникальный ID записи</summary>
        int I_ELog.p_GetLogEntityID => this.p_RecordID;

        /// <summary>Время формирования записи</summary>
        [Column("F_DATEFORMING")]
        public DateTime p_DateForming { get; set; }

        /// <summary>Время создания записи</summary>
        [Column("F_DATECREATE")]
        public DateTime p_DateCreate { get; set; }

        /// <summary>Время последнего изменения записи</summary>
        [Column("F_DATELASTCHANGE")]
        public DateTime p_DateLastChange { get; set; }

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

        /// <summary>Тип записи</summary>
        [Column("F_TYPE")]
        public E_RecordType p_Type { get; set; }

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

        /// <summary>ID пациента</summary>
        [Column("F_PATIENT_UID")]
        public Guid? p_PatientUID { get; set; }

        /// <summary>Пол пациента</summary>
        [Column("F_GENDER")]
        public Cl_User.E_Sex p_PatientSex { get; set; }

        /// <summary>Имя пациента</summary>
        [Column("F_PATIENT_NAME")]
        public string p_PatientName { get; set; }

        /// <summary>Фамиля пациента</summary>
        [Column("F_PATIENT_SURNAME")]
        public string p_PatientSurName { get; set; }

        /// <summary>Отчество пациента</summary>
        [Column("F_PATIENT_LASTNAME")]
        public string p_PatientLastName { get; set; }

        /// <summary>Дата рождения пациента</summary>
        [Column("F_PATIENT_DATEBIRTH")]
        public DateTime p_PatientDateBirth { get; set; }

        /// <summary>Название клиники</summary>
        [Column("F_CLINIKNAME")]
        public string p_ClinikName { get; set; }

        /// <summary>Заголовок записи</summary>
        [Column("F_TITLE")]
        public string p_Title { get; set; }

        /// <summary>HTML текст записи для клиента</summary>
        [Column("F_HTMLPATIENT")]
        public string p_HTMLPatient { get; set; }

        /// <summary>HTML текст записи для пользователя</summary>
        [Column("F_HTMLUSER")]
        public string p_HTMLUser { get; set; }

        /// <summary>Тип файла</summary>
        [Column("F_FILETYPE")]
        public E_RecordFileType p_FileType { get; set; }

        /// <summary>Данные файла</summary>
        [Column("F_FILE")]
        public byte[] p_FileBytes { get; set; }

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

        private List<Cl_RecordValue> m_Values = new List<Cl_RecordValue>();
        /// <summary>Список значений элементов записи</summary>
        [ForeignKey("p_RecordID")]
        [Cl_ELogProperty(p_IsComputedLog = true)]
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

        /// <summary>Возвращает возраст пациента</summary>
        public byte f_GetPatientAge()
        {
            byte age = 0;
            if (p_PatientDateBirth != null)
            {
                DateTime dateNow = DateTime.Now;
                byte year = (byte)(dateNow.Year - p_PatientDateBirth.Year);
                if (dateNow.Month < p_PatientDateBirth.Month ||
                    (dateNow.Month == p_PatientDateBirth.Month && dateNow.Day < p_PatientDateBirth.Day)) year--;
                return year;
            }
            return age;
        }

        /// <summary>Установка пользователя</summary>
        public void f_SetUser(Cl_User a_User)
        {
            p_ClinikName = a_User.p_ClinikName;
            p_UserID = a_User.p_UserID;
            p_UserSurName = a_User.p_UserSurName;
            p_UserName = a_User.p_UserName;
            p_UserLastName = a_User.p_UserLastName;
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

        /// <summary>Установка шаблона</summary>
        public void f_SetTemplate(Cl_Template a_Template)
        {
            p_Template = a_Template;
            p_Title = a_Template.p_Title;
            p_CategoryTotalID = a_Template.p_CategoryTotalID;
            p_CategoryTotal = a_Template.p_CategoryTotal;
            p_CategoryClinikID = a_Template.p_CategoryClinikID;
            p_CategoryClinik = a_Template.p_CategoryClinik;
        }

        /// <summary>Получение HTML текста записи для пациента</summary>
        public string f_GetHTMLPatient()
        {
            return f_GetHTML(false);
        }

        /// <summary>Получение HTML текста записи для пользователя</summary>
        public string f_GetHTMLUser()
        {
            return f_GetHTML(true);
        }

        /// <summary>Получение HTML текста запис</summary>
        private string f_GetHTML(bool a_IsUser)
        {
            string html = @"
<!DOCTYPE html>
<html>
    <head>
        <meta http-equiv=""X-UA-Compatible"" content=""IE=11"">
         <style type=""text/css"">
            body
            {
                padding: 10px 25px;
            }

            .record_title
            {
                text-align: center;
                padding-bottom: 10px;
                border-bottom: dashed 1px black;
            }

                .record_title img
                {
                    width: 450px;
                }

            .record_name
            {
                text-align: right;
                font: bold 14px arial;
                padding: 10px 0;
            }

            .record_info
            {
                font: bold 14px arial;
            }

            .record_date
            {
                font: bold 14px arial;
                padding: 10px 0;
            }

            .record_values
            {
                display: inline-table;
            }

            .record_value
            {
                display: table-row;
            }

            .record_value_name, .record_value_val
            {
                display: table-cell;
                padding: 5px 0;
            }

            .record_value_name
            {
                padding-right: 5px;
            }
        </style>
    </head>
    <body>
        <div class=""record_title"">
            <img src=""Images/title.jpg"" />
            <div>(495)775-75-66 | www.familydoctor.ru | company@familydoctor.ru</div>
        </div>
        <div class=""record_name"">";
            html += p_ClinikName;
            html += @"</div><div class=""record_info"">";
            html += string.Format("№ {0} {1} {2} {3} {4} # {5}", p_RecordID, p_PatientSurName, p_PatientName, p_PatientLastName, p_PatientDateBirth.ToString("dd.MM.yyyy"), p_ID);
            html += @"</div><div class=""record_date"">";
            html += p_DateCreate.ToString("dd.MM.yyyy");
            html += @"</div><div class=""record_values"">";
            foreach (var value in p_Values)
            {
                if (a_IsUser)
                    html += value.f_GetHTMLUser();
                else
                    html += value.f_GetHTMLPatient();
            }
            html += "</div>";
            html += "</body></html>";
            return html;
        }

        /// <summary>Получение конечного текста записи</summary>
        public string f_GetDocumentText(string a_AppStartupPath)
        {
            if (p_HTMLUser != null)
            {
                return p_HTMLUser.Replace("src=\"", "src=\"file:///" + a_AppStartupPath + "/");
            }
            else
            {
                if (p_Type == Cl_Record.E_RecordType.FinishedFile)
                {
                    if (p_FileType == Cl_Record.E_RecordFileType.HTML)
                    {
                        return Encoding.UTF8.GetString(p_FileBytes).Replace(@"\\family-doctor.local\fd$\FD.med\Images\Logo.jpg", "file:///" + a_AppStartupPath + "/Images/title.jpg");
                    }
                    else if (p_FileType == Cl_Record.E_RecordFileType.JFIF || p_FileType == Cl_Record.E_RecordFileType.JIF || p_FileType == Cl_Record.E_RecordFileType.JPE ||
                        p_FileType == Cl_Record.E_RecordFileType.JPEG || p_FileType == Cl_Record.E_RecordFileType.JPG || p_FileType == Cl_Record.E_RecordFileType.PNG)
                    {
                        return string.Format(@"<img src=""data:image/{0};base64,{1}"" />", Enum.GetName(typeof(Cl_Record.E_RecordFileType), p_FileType).ToLower(), Convert.ToBase64String(p_FileBytes));
                    }
                }
                else
                {
                    return null;
                }
            }
            return null;
        }
    }
}
