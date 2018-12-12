using Sadco.FamilyDoctor.Core.Data;
using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.Core.Facades;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Sadco.FamilyDoctor.LoaderRecords
{
    enum E_MessageType
    {
        Error,
        Warning,
        Info
    }

    class Cl_ImageFileData
    {
        public string m_FileName;
        public E_RecordFileType m_FileType;
        public byte[] m_FileData;
    }

    class Program
    {
        private static bool m_Loging = false;

        private static E_RecordFileType? f_GetFileType(string a_FileName)
        {
            var extension = Path.GetExtension(a_FileName).ToLower();
            if (extension == ".x")
            {
                extension = Path.GetExtension(a_FileName.Substring(0, a_FileName.Length - 2)).ToLower();
            }
            else if (extension == ".tag")
            {
                extension = Path.GetExtension(a_FileName.Substring(0, a_FileName.Length - 4)).ToLower();
            }
            if (extension == ".htm" || extension == ".html")
            {
                return E_RecordFileType.HTML;
            }
            else if (extension == ".pdf")
            {
                return E_RecordFileType.PDF;
            }
            else if (extension == ".jpg")
            {
                return E_RecordFileType.JPG;
            }
            else if (extension == ".jpeg")
            {
                return E_RecordFileType.JPEG;
            }
            else if (extension == ".jpe")
            {
                return E_RecordFileType.JPE;
            }
            else if (extension == ".jfif")
            {
                return E_RecordFileType.JFIF;
            }
            else if (extension == ".jif")
            {
                return E_RecordFileType.JIF;
            }
            else if (extension == ".png")
            {
                return E_RecordFileType.PNG;
            }
            else if (extension == ".gif")
            {
                return E_RecordFileType.GIF;
            }
            else if (extension == ".xml")
            {
                return E_RecordFileType.XML;
            }
            else
            {
                return null;
            }
        }

        static void Main(string[] args)
        {
            string local = null;
            string path = null;
            string dbConnection = null;
            var medicalCards = new Dictionary<string, Cl_MedicalCard>();

            if (args != null && args.Length >= 4)
            {
                for (var i = 0; i < args.Length; i++)
                {
                    var arg = args[i];
                    if (arg == "-local" && i + 1 < args.Length)
                    {
                        local = args[i + 1];
                    }
                    else if (arg == "-path" && i + 1 < args.Length)
                    {
                        path = args[i + 1];
                    }
                    else if (arg == "-db" && i + 1 < args.Length)
                    {
                        dbConnection = args[i + 1].Replace("Data_Source", "Data Source").Replace("Data_Source", "Data Source").Replace("Initial_Catalog", "Initial Catalog").Replace("Integrated_Security", "Integrated Security");
                    }
                    else if (arg == "-log" && i + 1 < args.Length)
                    {
                        m_Loging = args[i + 1] == "1";
                    }
                }
            }
            if (local != null && path != null && dbConnection != null)
            {
                if (Directory.Exists(path))
                {
                    var db = new Cl_DataContextMegaTemplate(dbConnection);
                    db.f_Init();
                    if (!Cl_CatalogsFacade.f_GetInstance().f_Init(db))
                    {
                        f_WriteLog("Не удалось инициализировать фасад работы со справочниками");
                        Console.ReadKey(true);
                        return;
                    }
                    if (!Cl_MedicalCardsFacade.f_GetInstance().f_Init(db))
                    {
                        f_WriteLog("Не удалось инициализировать фасад работы с мед картами");
                        Console.ReadKey(true);
                        return;
                    }
                    if (!Cl_RecordsFacade.f_GetInstance().f_Init(db, local))
                    {
                        f_WriteLog("Не удалось инициализировать фасад работы с записями");
                        Console.ReadKey(true);
                        return;
                    }
                    DirectoryInfo _dirInfo = new DirectoryInfo(Cl_RecordsFacade.f_GetInstance().f_GetLocalResourcesPath());
                    f_WriteLog(string.Format("Начало формирования записей папки \"{0}\"", path));
                    int iVal = 0;
                    Guid gVal = Guid.Empty;
                    DateTime dtVal = DateTime.MinValue;
                    var dirs = Directory.GetDirectories(path);
                    foreach (var dir in dirs)
                    {
                        var folderName = dir.Substring(dir.LastIndexOf("\\") + 1).Replace(path, "");
                        f_WriteLog(string.Format("Начало формирования записей пациента \"{0}\"", folderName));
                        var vals = folderName.Split(' ');
                        if (vals.Length >= 6 && vals.Length <= 7)
                        {
                            if (vals[5].Length > 0 && vals[5][0] == '#')
                            {
                                if (int.TryParse(vals[0], out iVal))
                                {
                                    var medicalNumber = iVal;
                                    var patientSurName = vals[1];
                                    var patientName = vals[2];
                                    var patientLastName = vals[3];
                                    if (DateTime.TryParse(vals[4], out dtVal))
                                    {
                                        var patientDateBirth = dtVal;
                                        bool validID = false; //ID пациента
                                        int patientID = 0;
                                        Guid patientUID = Guid.Empty;
                                        if (vals.Length == 6)
                                        {
                                            validID = int.TryParse(vals[5].Substring(1), out patientID);
                                        }
                                        else if (vals.Length == 7)
                                        {
                                            validID = Guid.TryParse(vals[6], out patientUID);
                                        }
                                        if (validID)
                                        {
                                            var dirsCliniks = Directory.GetDirectories(dir);
                                            foreach (var dirClinik in dirsCliniks)
                                            {
                                                var clinik = dirClinik.Substring(dirClinik.LastIndexOf("\\") + 1);
                                                var dirsCategories = Directory.GetDirectories(dirClinik);
                                                foreach (var dirCategory in dirsCategories)
                                                {
                                                    var cat = dirCategory.Substring(dirCategory.LastIndexOf("\\") + 1);
                                                    var category = Cl_CatalogsFacade.f_GetInstance().f_GetCategory(cat);
                                                    if (category == null)
                                                    {
                                                        category = Cl_CatalogsFacade.f_GetInstance().f_AddCategory(Cl_Category.E_CategoriesTypes.Total, cat);
                                                    }
                                                    if (category != null)
                                                    {
                                                        var curRecords = new List<Cl_Record>();
                                                        var filesRecords = Directory.GetFiles(dirCategory);
                                                        foreach (var fileRecord in filesRecords)
                                                        {
                                                            var valsRecord = fileRecord.Replace(dirCategory + "\\", "");
                                                            var pathRelativeFile = fileRecord.Replace(path, "");
                                                            var pathRelativeFolder = pathRelativeFile.Replace(valsRecord, "");
                                                            if (DateTime.TryParse(valsRecord.Substring(0, 8), out dtVal))
                                                            {
                                                                var record = new Cl_Record();
                                                                record.p_Version = 1;
                                                                record.p_Type = E_RecordType.FinishedFile;
                                                                record.p_IsAutomatic = true;
                                                                record.p_DateCreate = record.p_DateLastChange = record.p_DateReception = dtVal;

                                                                string patID = patientUID != Guid.Empty ? patientUID.ToString() : patientID.ToString();
                                                                string medicalCardKey = $"{medicalNumber}_{patID}";
                                                                Cl_MedicalCard medicalCard = null;
                                                                if (!medicalCards.TryGetValue(medicalCardKey, out medicalCard))
                                                                {
                                                                    if (patientUID != Guid.Empty)
                                                                        medicalCard = Cl_MedicalCardsFacade.f_GetInstance().f_GetMedicalCard(medicalNumber.ToString(), patientUID);
                                                                    else
                                                                        medicalCard = Cl_MedicalCardsFacade.f_GetInstance().f_GetMedicalCard(medicalNumber.ToString(), patientID);
                                                                    if (medicalCard == null)
                                                                    {
                                                                        medicalCard = new Cl_MedicalCard();
                                                                        medicalCard.p_Number = medicalNumber.ToString();
                                                                        medicalCard.p_DateCreate = record.p_DateCreate;
                                                                        medicalCard.p_PatientID = patientID;
                                                                        if (patientUID != Guid.Empty)
                                                                            medicalCard.p_PatientUID = patientUID;
                                                                        medicalCard.p_PatientSurName = patientSurName;
                                                                        medicalCard.p_PatientName = patientName;
                                                                        medicalCard.p_PatientLastName = patientLastName;
                                                                        medicalCard.p_PatientDateBirth = patientDateBirth;
                                                                        medicalCards.Add(medicalCardKey, medicalCard);
                                                                    }
                                                                }
                                                                record.p_MedicalCard = medicalCard;

                                                                record.p_ClinicName = clinik;
                                                                record.p_CategoryTotalID = category.p_ID;
                                                                record.p_CategoryTotal = category;
                                                                bool isAuthorValid = false;
                                                                var authorStart = valsRecord.IndexOf('[');
                                                                if (authorStart == -1 && valsRecord.LastIndexOf('.') > 9)
                                                                {
                                                                    record.p_Title = valsRecord.Substring(9, valsRecord.LastIndexOf('.') - 9);
                                                                    record.p_DateLastChange = record.p_DateCreate;
                                                                    isAuthorValid = true;
                                                                }
                                                                else if (authorStart > 10)
                                                                {
                                                                    record.p_Title = valsRecord.Substring(9, valsRecord.IndexOf('[') - 10);
                                                                    var authorEnd = valsRecord.IndexOf(']');
                                                                    if (authorEnd > 11 && authorEnd > authorStart + 22)
                                                                    {
                                                                        if (DateTime.TryParse(valsRecord.Substring(authorStart + 1, 19).Replace("-", ":"), out dtVal))
                                                                        {
                                                                            record.p_DateLastChange = dtVal;
                                                                            var authorName = valsRecord.Substring(authorStart + 22, authorEnd - authorStart - 22);
                                                                            var valsAuthorName = authorName.Split(' ');
                                                                            if (valsAuthorName.Length == 4)
                                                                            {
                                                                                record.p_DoctorSurName = valsAuthorName[1];
                                                                                record.p_DoctorName = valsAuthorName[2];
                                                                                record.p_DoctorLastName = valsAuthorName[3];
                                                                                isAuthorValid = true;
                                                                            }
                                                                            else
                                                                            {
                                                                                f_WriteLog(string.Format("Не корректные данные имени автора записи {0}", fileRecord), E_MessageType.Warning);
                                                                                continue;
                                                                            }
                                                                        }
                                                                        else if (valsRecord.IndexOf("_[") > 0 && valsRecord.IndexOf("]-") > 0 && valsRecord.LastIndexOf(".") > valsRecord.IndexOf("]-") + 2)
                                                                        {
                                                                            if (int.TryParse(valsRecord.Substring(valsRecord.IndexOf("]-") + 2, valsRecord.LastIndexOf(".") - valsRecord.IndexOf("]-") - 2), out iVal))
                                                                            {
                                                                                continue;
                                                                            }
                                                                            else
                                                                            {
                                                                                f_WriteLog(string.Format("Не корректное время изменения записи автором {0}", fileRecord), E_MessageType.Warning);
                                                                                continue;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            f_WriteLog(string.Format("Не корректное время изменения записи автором {0}", fileRecord), E_MessageType.Warning);
                                                                            continue;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        f_WriteLog(string.Format("Не корректные данные автора записи {0}", fileRecord), E_MessageType.Warning);
                                                                        continue;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    f_WriteLog(string.Format("Не корректные данные автора записи {0}", fileRecord), E_MessageType.Warning);
                                                                    continue;
                                                                }
                                                                if (isAuthorValid)
                                                                {
                                                                    var attrs = File.GetAttributes(fileRecord);
                                                                    var isPrint = attrs.HasFlag(FileAttributes.ReadOnly);
                                                                    if (isPrint)
                                                                    {
                                                                        record.p_DatePrintDoctor = record.p_DatePrintPatient = DateTime.Now;
                                                                    }

                                                                    var fileName = "";
                                                                    var extension = Path.GetExtension(fileRecord);
                                                                    fileName = fileRecord.Replace(extension, "");
                                                                    if (extension == ".x")
                                                                    {
                                                                        record.p_IsDelete = true;
                                                                        extension = Path.GetExtension(fileRecord.Substring(0, fileRecord.Length - 2));
                                                                        fileName = fileRecord.Replace(extension, "");
                                                                    }
                                                                    else if (extension == ".tag")
                                                                    {
                                                                        extension = Path.GetExtension(fileRecord.Substring(0, fileRecord.Length - 4));
                                                                        fileName = fileRecord.Replace(extension, "");
                                                                    }
                                                                    var recordFileType = f_GetFileType(valsRecord);
                                                                    if (recordFileType != null)
                                                                    {
                                                                        record.p_FileType = (E_RecordFileType)recordFileType;
                                                                    }
                                                                    else
                                                                    {
                                                                        f_WriteLog(string.Format("Неизвестный формат файла записи {0}", fileRecord), E_MessageType.Error);
                                                                        continue;
                                                                    }
                                                                    record.p_FilePath = pathRelativeFile;
                                                                    f_WriteLog(string.Format("Сформирована новая запись {0}", fileRecord), E_MessageType.Info);
                                                                    curRecords.Add(record);
                                                                }
                                                                else
                                                                {
                                                                    f_WriteLog(string.Format("Не корректные данные автора записи {0}", fileRecord), E_MessageType.Warning);
                                                                    continue;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                f_WriteLog(string.Format("Не корректная дата записи {0}", fileRecord), E_MessageType.Warning);
                                                                continue;
                                                            }
                                                        }

                                                        f_WriteLog("Начало сохранения сформированных записей", E_MessageType.Info);
                                                        if (Cl_RecordsFacade.f_GetInstance().f_AddRecords(curRecords))
                                                        {
                                                            f_WriteLog("Конец сохранения сформированных записей", E_MessageType.Info);
                                                            f_WriteLog("Начало копирования папки с файлами", E_MessageType.Info);
                                                            foreach (string _path in Directory.GetFiles(dirCategory, "*.*", SearchOption.AllDirectories))
                                                            {
                                                                try
                                                                {
                                                                    var fileName = Path.GetFileName(_path);
                                                                    var relativePath = _path.Replace(path, "").Replace("\\" + fileName, "");
                                                                    Cl_RecordsFacade.f_GetInstance().f_SaveFileFromSql(relativePath, fileName, File.ReadAllBytes(_path));
                                                                }
                                                                catch (Exception e)
                                                                {
                                                                    f_WriteLog("Не удалось скопировать файл " + _path, E_MessageType.Info);
                                                                }
                                                            }
                                                            f_WriteLog("Конец копирования папки с файлами", E_MessageType.Info);
                                                        }
                                                        else
                                                        {
                                                            f_WriteLog("Не удалось сохраненить сформированные записи", E_MessageType.Info);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        f_WriteLog("Категория записей не найдена", E_MessageType.Error);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            f_WriteLog("Не корректный ID пациента", E_MessageType.Warning);
                                        }
                                    }
                                    else
                                    {
                                        f_WriteLog("Не корректная дата рождения пациента", E_MessageType.Warning);
                                    }
                                }
                                else
                                {
                                    f_WriteLog("Не корректный номер медкарты", E_MessageType.Warning);
                                }
                            }
                            else
                            {
                                f_WriteLog("Не корректное название архива записей", E_MessageType.Warning);
                            }
                        }
                        else
                        {
                            f_WriteLog("Не корректное название архива записей", E_MessageType.Warning);
                        }
                    }
                    //f_WriteLog("Начало сохранения сформированных записей", E_MessageType.Info);
                    //if (Cl_RecordsFacade.f_GetInstance().f_AddRecords(records))
                    //{
                    //    f_WriteLog("Конец сохранения сформированных записей", E_MessageType.Info);



                    //f_WriteLog("Начало копирования папки с файлами", E_MessageType.Info);
                    //foreach (string _path in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
                    //{
                    //    try
                    //    {
                    //        var fileName = Path.GetFileName(_path);
                    //        var relativePath = _path.Replace(path, "").Replace("\\" + fileName, "");
                    //        Cl_RecordsFacade.f_GetInstance().f_SaveFileFromSql(relativePath, fileName, File.ReadAllBytes(_path));
                    //    }
                    //    catch (Exception e)
                    //    {
                    //        f_WriteLog("Не удалось скопировать файл " + _path, E_MessageType.Info);
                    //    }
                    //}
                    //f_WriteLog("Конец копирования папки с файлами", E_MessageType.Info);



                    //}
                    //else
                    //{
                    //    f_WriteLog("Не удалось сохраненить сформированные записи", E_MessageType.Info);
                    //}
                    f_WriteLog(string.Format("Конец формирования записей папки \"{0}\"", path));
                }
                else
                {
                    f_WriteLog("Папка архива записей для загрузки не существует.", E_MessageType.Error);
                }
            }
            else
            {
                f_WriteLog("Не указана папка архива записей для загрузки.", E_MessageType.Error);
            }
            f_FixedLog();
            Console.ReadKey(true);
        }

        private static string m_HTMLText = @"<!DOCTYPE html>
<html>
    <head>
        <meta http-equiv=""X-UA-Compatible"" content=""IE=11"">
        <meta http-equiv=Content-Type content=""text/html; charset=utf-8"">
        <style type=""text/css"">
            body
            {
                padding: 10px 25px;
            }

            .msg
            {
                padding: 10px 5px;
            }

            .error
            {
                color: #b30000;
            }
            
            .warning
            {
                color: #cc7a00;
            }

            .info
            {
                color: black;
            }
        </style>
    </head>
    <body>";

        /// <summary>Запись лога</summary>
        protected static void f_WriteLog(string a_Text, E_MessageType a_MessageType = E_MessageType.Info)
        {
            if (!string.IsNullOrWhiteSpace(a_Text))
            {
                var msgType = a_MessageType == E_MessageType.Info ? "info" : a_MessageType == E_MessageType.Warning ? "warning" : "error";
                Console.WriteLine(string.Format("{0}: {1}", a_MessageType == E_MessageType.Info ? "Инфо" : a_MessageType == E_MessageType.Warning ? "Предупреждение" : "Ошибка", a_Text));
                if (m_Loging)
                {
                    m_HTMLText += string.Format(@"<div class=""msg {0}"">{1}</div>", msgType, a_Text);
                }
            }
        }

        /// <summary>Запись лога в файл</summary>
        protected static void f_FixedLog()
        {
            if (m_Loging)
            {
                m_HTMLText += "</body></html>";
                using (var fileStream = File.CreateText(Directory.GetCurrentDirectory() + "//log.html"))
                {
                    fileStream.WriteLine(m_HTMLText);
                }
            }
        }
    }
}
