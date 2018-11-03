using FD.dat.mon.stb.lib;
using Sadco.FamilyDoctor.Core.EntityLogs;
using Sadco.FamilyDoctor.Core.Facades;
using Sadco.FamilyDoctor.Core.Permision;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Sadco.FamilyDoctor.Core.Entities
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

    /// <summary>
    /// Класс сущности записи
    /// </summary>
    [Cl_ELogClass(E_EntityTypes.Records)]
    [Table("T_RECORDS")]
    public class Cl_Record : Cl_RecordBase, I_Record, I_Version, I_Delete, I_ELog
    {
        public class Cl_EventArgs : EventArgs
        {
            public Cl_Record p_Record { get; set; }
        }

        [NotMapped]
        public Cl_Record p_ParentRecord { get; set; }

        /// <summary>ID записи для всех версий</summary>
        [Column("F_RECORD_ID")]
        [Description("ID записи для всех версий")]
        public int p_RecordID { get; set; }

        /// <summary>Версия записи</summary>
        [Column("F_VERSION")]
        [Description("Версия записи")]
        public int p_Version { get; set; }

        /// <summary>Заголовок записи</summary>
        [Column("F_TITLE")]
        public string p_Title { get; set; }

        /// <summary>Флаг нахождения записи в удалении</summary>
        [Column("F_ISDEL")]
        [Description("Флаг нахождения записи в удалении")]
        [Cl_ELogProperty("Запись удалена", p_IsCustomDescription = true, p_IgnoreValue = true)]
        public bool p_IsDelete { get; set; }

        /// <summary>Возвращает уникальный ID записи</summary>
        int I_ELog.p_GetLogEntityID => this.p_RecordID;

        /// <summary>Время приема</summary>
        [Column("F_DATERECEPTION")]
        [Description("Время приема")]
        public DateTime p_DateReception { get; set; }

        /// <summary>Время создания записи</summary>
        [Column("F_DATECREATE")]
        [Description("Время создания записи")]
        public DateTime p_DateCreate { get; set; }

        /// <summary>Время последнего изменения записи</summary>
        [Column("F_DATELASTCHANGE")]
        [Description("Время последнего изменения записи")]
        public DateTime p_DateLastChange { get; set; }

        /// <summary>ID медицинской карты</summary>
        [Column("F_MEDICALCARD_ID")]
        [Description("ID медицинской карты")]
        public int? p_MedicalCardID { get; set; }

        /// <summary>Mедицинская карта</summary>
        [Description("Mедицинская карта")]
        [ForeignKey("p_MedicalCardID")]
        public Cl_MedicalCard p_MedicalCard { get; set; }

        public string p_MedicalCardNumber { get { return p_MedicalCard != null ? p_MedicalCard.p_Number : ""; } }

        /// <summary>Дата первой печати для доктора</summary>
        [Column("F_DATEPRINTDOCTOR")]
        [Description("Дата первой печати для доктора")]
        public DateTime? p_DatePrintDoctor { get; set; }

        /// <summary>Дата первой печати для пациента</summary>
        [Column("F_DATEPRINTPATIENT")]
        [Description("Дата первой печати для пациента")]
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
        [Column("F_ISAUTOMATIC")]
        [Description("Флаг автомата")]
        public bool p_IsAutomatic { get; set; }

        /// <summary>Дата синхронизации с БМК</summary>
        [Column("F_DATESYNCBMK")]
        [Description("Дата синхронизации с БМК")]
        [Cl_ELogProperty("Cинхронизации с БМК")]
        public DateTime? p_DateSyncBMK { get; set; }

        /// <summary>Флаг синхронизации с БМК</summary>
        public bool p_IsSyncBMK {
            get {
                return p_DateSyncBMK != null;
            }
        }

        /// <summary>Тип записи</summary>
        [Column("F_TYPE")]
        [Description("Тип записи")]
        public E_RecordType p_Type { get; set; }

        /// <summary>MKB1</summary>
        [Column("F_MKB1")]
        [MaxLength(50)]
        [Cl_ELogProperty(p_Description = "MKB1")]
        public string p_MKB1 { get; set; }
        /// <summary>MKB1</summary>
        [Column("F_MKB2")]
        [MaxLength(50)]
        [Cl_ELogProperty(p_Description = "MKB2")]
        public string p_MKB2 { get; set; }
        /// <summary>MKB1</summary>
        [Column("F_MKB3")]
        [MaxLength(50)]
        [Cl_ELogProperty(p_Description = "MKB3")]
        public string p_MKB3 { get; set; }
        /// <summary>MKB1</summary>
        [Column("F_MKB4")]
        [MaxLength(50)]
        [Cl_ELogProperty(p_Description = "MKB4")]
        public string p_MKB4 { get; set; }

        /// <summary>HTML текст записи для клиента</summary>
        [Column("F_HTMLPATIENT")]
        [Description("HTML текст записи для клиента")]
        public string p_HTMLPatient { get; set; }

        /// <summary>HTML текст записи для пользователя</summary>
        [Column("F_HTMLUSER")]
        [Description("HTML текст записи для пользователя")]
        public string p_HTMLDoctor { get; set; }

        /// <summary>Тип файла</summary>
        [Column("F_FILETYPE")]
        [Description("Тип файла")]
        public E_RecordFileType p_FileType { get; set; }

        /// <summary>Путь к файлу</summary>
        [Column("F_PATHFILE")]
        [Description("Путь к файлу")]
        [Cl_ELogProperty(p_Description = "Путь к файлу", p_IgnoreValue = true)]
        public string p_FilePath { get; set; }

        /// <summary>Бинарные данные файла</summary>
        [NotMapped]
        [Description("Данные файла")]
        [Cl_ELogProperty(p_Description = "Данные файла", p_IgnoreValue = true)]
        public byte[] p_FileBytes { get; set; }

        private List<Cl_RecordValue> m_Values = new List<Cl_RecordValue>();
        /// <summary>Список значений элементов записи</summary>
        [ForeignKey("p_RecordID")]
        [Description("Список значений элементов записи")]
        [Cl_ELogProperty(p_IsComputedLog = true)]
        public List<Cl_RecordValue> p_Values {
            get { return m_Values; }
            set { m_Values = value; }
        }

        public override void f_SetTemplate(Cl_Template a_Template)
        {
            base.f_SetTemplate(a_Template);
            if (a_Template != null)
            {
                p_Title = a_Template.p_Title;
            }
        }

        public override IEnumerable<I_RecordValue> f_GetRecordsValues()
        {
            return p_Values;
        }

        /// <summary>Проверка наличия всех необходимых значений</summary>
        public bool f_IsValid()
        {
            return (!string.IsNullOrWhiteSpace(p_Title) && !string.IsNullOrWhiteSpace(p_ClinicName)
                 && p_DateCreate != null && p_DateLastChange != null && p_DateReception != null
                 && !string.IsNullOrWhiteSpace(p_DoctorSurName) && !string.IsNullOrWhiteSpace(p_DoctorName) && !string.IsNullOrWhiteSpace(p_DoctorLastName)
                 && p_MedicalCard != null);
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
            template = Regex.Replace(template, "<fd\\.document\\.location>.*?<\\/fd\\.document\\.location>", string.Format("<fd.document.location>{0}</fd.document.location>", p_ClinicName));
            template = Regex.Replace(template, "<fd\\.document\\.patient>.*?<\\/fd\\.document\\.patient>", string.Format("<fd.document.patient>{0} {1} {2} {3} {4} {5} ({6}) # {7}</fd.document.patient>",
                p_RecordID, p_MedicalCard.p_PatientSurName, p_MedicalCard.p_PatientName, p_MedicalCard.p_PatientLastName,
                p_MedicalCard.p_PatientSex == Cl_User.E_Sex.Man ? "М" : "Ж", p_MedicalCard.p_PatientDateBirth.ToString("dd.MM.yyyy"), p_MedicalCard.f_GetPatientAgeByMonthText(p_DateCreate), p_ID));
            template = Regex.Replace(template, "<fd\\.document\\.date>.*?<\\/fd\\.document\\.date>", string.Format("<fd.document.date>{0}</fd.document.date>", p_DateReception.ToString("dd.MM.yyyy")));
            template = Regex.Replace(template, "<fd\\.document\\.time>.*?<\\/fd\\.document\\.time>", string.Format("<fd.document.time>{0}</fd.document.time>", p_DateReception.ToString("HH:mm")));
            template = Regex.Replace(template, "<fd\\.document\\.title>.*?<\\/fd\\.document\\.title>", string.Format("<fd.document.title>{0}</fd.document.title>", p_Title));
            string htmlContent = "";

            if (p_Type == E_RecordType.FinishedFile)
            {
                htmlContent += $"<div><img src=\"{Cl_RecordsFacade.f_GetInstance().f_GetLocalResourcesRelativeFilePath(this)}\" /></div>";
            }
            else
            {
                string htmlTabling = null;
                string htmlFloating = null;
                byte age = p_MedicalCard.f_GetPatientAgeByYear(p_DateCreate);

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
                    var te = Cl_RecordsFacade.f_GetInstance().f_GetTemplateElement(value);
                    if (te != null && te.p_Template != null)
                    {
                        decimal? min = 0;
                        decimal? max = 0;
                        var partNorm = value.p_Element.f_GetPartNormValue(p_MedicalCard.p_PatientSex, age, out min, out max);
                        string htmlBlock = "";
                        if (a_IsDoctor)
                            htmlBlock = Cl_RecordsFacade.f_GetInstance().f_GetHTMLDoctor(this, value, te.p_Template.p_Type == Cl_Template.E_TemplateType.Table, min, max);
                        else
                            htmlBlock = Cl_RecordsFacade.f_GetInstance().f_GetHTMLPatient(this, value, te.p_Template.p_Type == Cl_Template.E_TemplateType.Table, min, max);
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
            }

            htmlContent += $"<p>MKБ: {p_MKB1} - {p_MKB2} - {p_MKB3} - {p_MKB4}</p>";
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
            if (p_Type == E_RecordType.FinishedFile)
            {
                p_FileBytes = Cl_RecordsFacade.f_GetInstance().f_GetFileFromSql(this);
                if (p_FileBytes != null)
                {
                    if (p_FileType == E_RecordFileType.HTML)
                    {
                        return Encoding.UTF8.GetString(p_FileBytes).Replace("src=\"", "src=\"file:///" + a_AppStartupPath + "/");
                    }
                    else if (p_FileType == E_RecordFileType.PDF)
                    {
                        return Encoding.UTF8.GetString(p_FileBytes);
                    }
                    else if (p_FileType == E_RecordFileType.JFIF || p_FileType == E_RecordFileType.JIF || p_FileType == E_RecordFileType.JPE ||
                        p_FileType == E_RecordFileType.JPEG || p_FileType == E_RecordFileType.JPG || p_FileType == E_RecordFileType.PNG || p_FileType == E_RecordFileType.GIF)
                    {
                        return string.Format(@"<img src=""data:image/{0};base64,{1}"" />", Enum.GetName(typeof(E_RecordFileType), p_FileType).ToLower(), Convert.ToBase64String(p_FileBytes));
                    }
                }
                else
                {
                    MonitoringStub.Warning("Файл отсутствует");
                }
            }
            else if (a_IsDoctor && p_HTMLDoctor != null)
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
            return null;
        }
    }
}
