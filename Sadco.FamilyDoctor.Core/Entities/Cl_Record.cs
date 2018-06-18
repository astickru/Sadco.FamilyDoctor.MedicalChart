using Sadco.FamilyDoctor.Core.EntityLogs;
using Sadco.FamilyDoctor.Core.Permision;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.RegularExpressions;

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

        /// <summary>Дата первой печати для доктора</summary>
        [Column("F_DATEPRINTDOCTOR")]
        public DateTime? p_DatePrintDoctor { get; set; }

        /// <summary>Дата первой печати для пациента</summary>
        [Column("F_DATEPRINTPATIENT")]
        public DateTime? p_DatePrintPatient { get; set; }

        /// <summary>Флаг печати для доктора</summary>
        public bool p_IsPrintDoctor {
            get {
                return p_DatePrintDoctor != null;
            }
        }

        /// <summary>Флаг печати для пациента</summary>
        public bool p_IsPrintPatient {
            get {
                return p_DatePrintPatient != null;
            }
        }

        /// <summary>Флаг автомата</summary>
        [Column("F_ISAUTIMATIC")]
        public bool p_IsAutimatic { get; set; }

        /// <summary>Дата синхронизации с БМК</summary>
        [Column("F_DATESYNCBMK")]
        public DateTime? p_DateSyncBMK { get; set; }

        /// <summary>Флаг синхронизации с БМК</summary>
        public bool p_IsSyncBMK {
            get {
                return p_DateSyncBMK != null;
            }
        }

        /// <summary>Тип записи</summary>
        [Column("F_TYPE")]
        public E_RecordType p_Type { get; set; }

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
        public string p_HTMLDoctor { get; set; }

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
        public string p_DoctorFIO { get { return f_GetDoctorInitials(); } }
        /// <summary>Возвращает инициалы пользователя</summary>
        public string f_GetDoctorInitials()
        {
            return string.Format("{0} {1} {2}", p_DoctorSurName, string.IsNullOrWhiteSpace(p_DoctorName) ? "" : p_DoctorName[0].ToString() + ".", string.IsNullOrWhiteSpace(p_DoctorLastName) ? "" : p_DoctorLastName[0].ToString() + ".");
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
        public void f_SetDoctor(Cl_User a_User)
        {
            p_ClinikName = a_User.p_ClinikName;
            p_DoctorID = a_User.p_UserID;
            p_DoctorSurName = a_User.p_UserSurName;
            p_DoctorName = a_User.p_UserName;
            p_DoctorLastName = a_User.p_UserLastName;
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
        public string f_GetHTMLDoctor()
        {
            return f_GetHTML(true);
        }

        /// <summary>Получение HTML текста записи</summary>
        private string f_GetHTML(bool a_IsDoctor)
        {
            var template = Properties.Resources.ResourceManager.GetObject("template_report").ToString();
            template = Regex.Replace(template, "<fd\\.document\\.location>.*?<\\/fd\\.document\\.location>", string.Format("<fd.document.location>{0}</fd.document.location>", p_ClinikName));
            template = Regex.Replace(template, "<fd\\.document\\.patient>.*?<\\/fd\\.document\\.patient>", string.Format("<fd.document.patient>{0} {1} {2} {3} {4} # {5}</fd.document.patient>",
                p_RecordID, p_PatientSurName, p_PatientName, p_PatientLastName, p_PatientDateBirth.ToString("dd.MM.yyyy"), p_ID));
            template = Regex.Replace(template, "<fd\\.document\\.date>.*?<\\/fd\\.document\\.date>", string.Format("<fd.document.date>{0}</fd.document.date>", p_DateCreate.ToString("dd.MM.yyyy")));
            template = Regex.Replace(template, "<fd\\.document\\.time>.*?<\\/fd\\.document\\.time>", string.Format("<fd.document.time>{0}</fd.document.time>", p_DateCreate.ToString("hh:mm")));
            template = Regex.Replace(template, "<fd\\.document\\.title>.*?<\\/fd\\.document\\.title>", string.Format("<fd.document.title>{0}</fd.document.title>", p_Title));
            string htmlContent = "";
            string htmlTabling = null;
            string htmlFloating = null;
            byte age = f_GetPatientAge();
            
            void f_EndTabling()
            {
                if (htmlTabling != null)
                {
                    htmlTabling += "</tbody></table>";
                    htmlContent += htmlTabling;
                    htmlTabling = null;
                }
            }

            void f_EndFloating()
            {
                if (htmlFloating != null)
                {
                    if (htmlFloating.Length > 0)
                    {
                        if (htmlFloating[htmlFloating.Length - 1] != '.')
                            htmlFloating += ".";
                        htmlFloating += "</p>";
                        htmlContent += htmlFloating;
                    }
                    htmlFloating = null;
                }
            }

            foreach (var value in p_Values)
            {
                var te = value.f_GetTemplateElement();
                if (te != null && te.p_Template != null)
                {
                    decimal? min = 0;
                    decimal? max = 0;
                    var partNorm = value.p_Element.f_GetPartNormValue(p_PatientSex, age, out min, out max);
                    string htmlBlock = "";
                    if (a_IsDoctor)
                        htmlBlock = value.f_GetHTMLDoctor(this, te.p_Template.p_Type == Cl_Template.E_TemplateType.Table, min, max);
                    else
                        htmlBlock = value.f_GetHTMLPatient(this, te.p_Template.p_Type == Cl_Template.E_TemplateType.Table, min, max);
                    if (!string.IsNullOrWhiteSpace(htmlBlock))
                    {
                        if (te.p_Template.p_Type == Cl_Template.E_TemplateType.Table)
                        {
                            f_EndFloating();
                            if (htmlTabling == null)
                                htmlTabling = "<table border=\"1\" cellpadding=\"4\" bordercolor=\"black\" width=\"100%\" style=\"border-collapse:collapse;font-family:Verdana;font-size:11px;\"><thead><tr><td>Показатель</td><td>Значение</td><td>Ед. изм.</td><td>Нормa</td></tr></thead><tbody>";
                            htmlTabling += htmlBlock;
                        }
                        else if (value.p_Element.p_ElementType == Cl_Element.E_ElementsTypes.Float)
                        {
                            f_EndTabling();
                            if (htmlFloating == null)
                                htmlFloating = "<p>" + htmlBlock;
                            else
                                htmlFloating += " " + htmlBlock;
                        }
                        else
                        {
                            f_EndTabling();
                            f_EndFloating();
                            if (a_IsDoctor)
                                htmlContent += htmlBlock;
                            else
                                htmlContent += htmlBlock;
                        }
                    }
                }
            }
            f_EndTabling();
            f_EndFloating();
            template = Regex.Replace(template, "<fd\\.document\\.content>.*?<\\/fd\\.document\\.content>", string.Format("<fd.document.content>{0}</fd.document.content>", htmlContent));
            template = Regex.Replace(template, "<fd\\.document\\.doctor>.*?<\\/fd\\.document\\.doctor>", string.Format("<fd.document.doctor>{0}</fd.document.doctor>", p_DoctorFIO));
            return template;
        }

        /// <summary>Получение конечного текста записи для специалиста</summary>
        public string f_GetDocumentTextDoctor(string a_AppStartupPath)
        {
            return f_GetDocumentText(a_AppStartupPath, true);
        }

        /// <summary>Получение конечного текста записи для пациента</summary>
        public string f_GetDocumentTextPatient(string a_AppStartupPath)
        {
            return f_GetDocumentText(a_AppStartupPath, false);
        }

        /// <summary>Получение конечного текста записи</summary>
        private string f_GetDocumentText(string a_AppStartupPath, bool a_IsDoctor)
        {
            if (a_IsDoctor && p_HTMLDoctor != null)
            {
                var res = p_HTMLDoctor.Replace("class=\"record_title_img\" src=\"", "class=\"record_title_img\" src=\"file:///" + a_AppStartupPath + "/");
                res = p_HTMLDoctor.Replace("class=record_title_img src=", "class=record_title_img src=file:///" + a_AppStartupPath + "/");
                return res;
            }
            else if (!a_IsDoctor && p_HTMLPatient != null)
            {
                var res = p_HTMLPatient.Replace("class=\"record_title_img\" src=\"", "class=\"record_title_img\" src=\"file:///" + a_AppStartupPath + "/");
                res = p_HTMLPatient.Replace("class=record_title_img src=", "class=record_title_img src=file:///" + a_AppStartupPath + "/");
                return res;
            }
            else
            {
                if (p_Type == Cl_Record.E_RecordType.FinishedFile)
                {
                    if (p_FileType == Cl_Record.E_RecordFileType.HTML)
                    {
                        return Encoding.UTF8.GetString(p_FileBytes).Replace("src=\"", "src=\"file:///" + a_AppStartupPath + "/");
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
