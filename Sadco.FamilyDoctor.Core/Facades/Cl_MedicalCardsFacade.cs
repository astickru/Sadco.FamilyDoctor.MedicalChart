using FD.dat.mon.stb.lib;
using Sadco.FamilyDoctor.Core.Data;
using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.Core.Permision;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Sadco.FamilyDoctor.Core.Facades
{
    /// <summary>
    /// Фасад работы с медкартами
    /// </summary>
    public class Cl_MedicalCardsFacade
    {
        private static Cl_MedicalCardsFacade INSTANCE = new Cl_MedicalCardsFacade();
        public static Cl_MedicalCardsFacade f_GetInstance()
        {
            return INSTANCE;
        }

        private bool m_IsInit = false;
        private Cl_DataContextMegaTemplate m_DataContextMegaTemplate = null;

        /// <summary>Инициализация фасада</summary>
        public bool f_Init(Cl_DataContextMegaTemplate a_DataContextMegaTemplate)
        {
            m_DataContextMegaTemplate = a_DataContextMegaTemplate;
            m_IsInit = m_DataContextMegaTemplate != null;
            return m_IsInit;
        }

        /// <summary>Создание новой медкарты</summary>
        /// <param name="a_Number">Номер медкарта</param>
        /// <param name="a_PatientID">ID пациента</param>
        /// <param name="a_PatientSex">Пол пациента</param>
        /// <param name="a_PatientSurName">Фамиля пациента</param>
        /// <param name="a_PatientName">Имя пациента</param>
        /// <param name="a_PatientLastName">Отчество пациента</param>
        /// <param name="a_PatientDateBirth">Дата рождения пациента</param>
        /// <param name="a_Comment">Комментарий</param>
        /// <returns>Созданная медкарта</returns>
        public Cl_MedicalCard f_CreateMedicalCard(string a_Number, int a_PatientID, Cl_User.E_Sex a_PatientSex, string a_PatientSurName, string a_PatientName, string a_PatientLastName, DateTime a_PatientDateBirth, string a_Comment)
        {
            if (m_DataContextMegaTemplate != null)
            {
                if (string.IsNullOrWhiteSpace(a_Number))
                {
                    MonitoringStub.Error("Error_CreateMedicalCard", "Не указан номер медкарты", null, null, null);
                    return null;
                }
                if (string.IsNullOrWhiteSpace(a_PatientSurName))
                {
                    MonitoringStub.Error("Error_CreateMedicalCard", "Не указано фамилия пациента", null, null, null);
                    return null;
                }
                if (string.IsNullOrWhiteSpace(a_PatientName))
                {
                    MonitoringStub.Error("Error_CreateMedicalCard", "Не указано имя пациента", null, null, null);
                    return null;
                }
                if (string.IsNullOrWhiteSpace(a_PatientLastName))
                {
                    MonitoringStub.Error("Error_CreateMedicalCard", "Не указано отчество пациента", null, null, null);
                    return null;
                }
                var medicalCard = new Cl_MedicalCard();
                try
                {
                    medicalCard.p_Number = a_Number;
                    medicalCard.p_DateCreate = DateTime.Now;
                    medicalCard.p_PatientID = a_PatientID;
                    medicalCard.p_PatientSex = a_PatientSex;
                    medicalCard.p_PatientSurName = a_PatientSurName;
                    medicalCard.p_PatientName = a_PatientName;
                    medicalCard.p_PatientLastName = a_PatientLastName;
                    medicalCard.p_PatientDateBirth = a_PatientDateBirth;
                    medicalCard.p_Comment = a_Comment;
                    m_DataContextMegaTemplate.p_MedicalCards.Add(medicalCard);
                    m_DataContextMegaTemplate.SaveChanges();
                    return medicalCard;
                }
                catch (Exception er)
                {
                    m_DataContextMegaTemplate.p_MedicalCards.Remove(medicalCard);
                    MonitoringStub.Error("Error_CreateMedicalCard", "Не удалось создать медкарту", er, null, null);
                    return null;
                }
            }
            else
            {
                MonitoringStub.Error("Error_MedicalCardsFacade", "Не инициализирован фасад", null, null, null);
                return null;
            }
        }

        /// <summary>Получение медкарты</summary>
        /// <param name="a_Number">Номер медкарта</param>
        /// <param name="a_PatientID">ID пациента</param>
        public Cl_MedicalCard f_GetMedicalCard(string a_Number, int a_PatientID)
        {
            Cl_MedicalCard medicalCard = null;
            if (m_DataContextMegaTemplate != null)
            {
                return m_DataContextMegaTemplate.p_MedicalCards.FirstOrDefault(m => m.p_Number == a_Number && m.p_PatientID == a_PatientID);
            }
            else
            {
                MonitoringStub.Error("Error_MedicalCardsFacade", "Не инициализирован фасад", null, null, null);
                return null;
            }
        }

        /// <summary>Получение медкарты</summary>
        /// <param name="a_Number">Номер медкарта</param>
        /// <param name="a_PatientUID">UID пациента</param>
        public Cl_MedicalCard f_GetMedicalCard(string a_Number, Guid a_PatientUID)
        {
            Cl_MedicalCard medicalCard = null;
            if (m_DataContextMegaTemplate != null)
            {
                return m_DataContextMegaTemplate.p_MedicalCards.FirstOrDefault(m => m.p_Number == a_Number && m.p_PatientUID == a_PatientUID);
            }
            else
            {
                MonitoringStub.Error("Error_MedicalCardsFacade", "Не инициализирован фасад", null, null, null);
                return null;
            }
        }

        /// <summary>Удаление медкарты</summary>
        /// <param name="a_Number">Номер медкарта</param>
        /// <param name="a_PatientID">ID пациента</param>
        public bool f_DeleteMedicalCard(string a_Number, int a_PatientID)
        {
            if (m_DataContextMegaTemplate != null)
            {
                var medicalCard = f_GetMedicalCard(a_Number, a_PatientID);
                if (medicalCard != null)
                {
                    if (m_DataContextMegaTemplate.p_Records.Include(r => r.p_MedicalCard).Any(r => r.p_MedicalCard.p_Number == a_Number && !r.p_IsDelete))
                    {
                        MonitoringStub.Error("Error_CreateMedicalCard", $"Имеются не удаленные записи медкарты {a_Number}", null, null, null);
                        return false;
                    }
                    try
                    {
                        m_DataContextMegaTemplate.p_MedicalCards.Remove(medicalCard);
                        return true;
                    }
                    catch (Exception er)
                    {
                        MonitoringStub.Error("Error_CreateMedicalCard", "Не удалось удалить медкарту", er, null, null);
                        return false;
                    }
                }
                else
                {
                    MonitoringStub.Error("Error_DeleteMedicalCard", "Не удалось найти медкарту", null, null, null);
                    return false;
                }
            }
            else
            {
                MonitoringStub.Error("Error_MedicalCardsFacade", "Не инициализирован фасад", null, null, null);
                return false;
            }
        }

        /// <summary>Объединение медкарт в одну действующую</summary>
        /// <param name="a_PatientID">ID пациента</param>
        public bool f_MergeMedicalCards(int a_PatientID)
        {
            if (m_DataContextMegaTemplate != null)
            {
                var medicalCards = m_DataContextMegaTemplate.p_MedicalCards.Where(m => m.p_DateArchive == null && m.p_PatientID == a_PatientID).ToList();
                if (medicalCards != null && medicalCards.Count > 0)
                {
                    if (medicalCards.Count > 1)
                    {
                        Cl_MedicalCard actualMedicalCard = null;
                        foreach (var medicalCard in medicalCards)
                        {
                            if (actualMedicalCard == null || actualMedicalCard.p_DateCreate < medicalCard.p_DateCreate)
                            {
                                actualMedicalCard = medicalCard;
                            }
                        }
                        medicalCards.Remove(actualMedicalCard);
                        try
                        {
                            foreach (var medicalCard in medicalCards)
                            {
                                var records = Cl_RecordsFacade.f_GetInstance().f_GetRecords(medicalCard);
                                if (records != null)
                                {
                                    foreach (var record in records)
                                    {
                                        record.p_MedicalCardID = actualMedicalCard.p_ID;
                                        record.p_MedicalCard = actualMedicalCard;
                                    }
                                }
                                medicalCard.p_DateDelete = DateTime.Now;
                            }
                            actualMedicalCard.p_DateMerge = DateTime.Now;
                            m_DataContextMegaTemplate.SaveChanges();
                            return true;
                        }
                        catch (Exception er)
                        {
                            MonitoringStub.Error("Error_MergeMedicalCards", "Не удалось объединить медкарты в одну действующую", er, null, null);
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    MonitoringStub.Error("Error_MergeMedicalCards", "Не удалось найти действующую медкарту", null, null, null);
                    return false;
                }
            }
            else
            {
                MonitoringStub.Error("Error_MedicalCardsFacade", "Не инициализирован фасад", null, null, null);
                return false;
            }
        }

        /// <summary>Присоединение медкарт</summary>
        /// <param name="a_SourceNumber">Номер источника медкарта</param>
        /// <param name="a_SourcePatientID">ID источника пациента</param>
        /// <param name="a_TargetNumber">Номер адресата медкарта</param>
        /// <param name="a_TargetPatientID">ID адресата пациента</param>
        public bool f_MergeMedicalCards(string a_SourceNumber, int a_SourcePatientID, string a_TargetNumber, int a_TargetPatientID)
        {
            if (m_DataContextMegaTemplate != null)
            {
                var sourceMedicalCard = f_GetMedicalCard(a_SourceNumber, a_SourcePatientID);
                if (sourceMedicalCard != null)
                {
                    var targetMedicalCard = f_GetMedicalCard(a_TargetNumber, a_TargetPatientID);
                    if (targetMedicalCard != null)
                    {
                        try
                        {
                            var records = Cl_RecordsFacade.f_GetInstance().f_GetRecords(sourceMedicalCard);
                            if (records != null)
                            {
                                foreach (var record in records)
                                {
                                    record.p_MedicalCardID = targetMedicalCard.p_ID;
                                    record.p_MedicalCard = targetMedicalCard;
                                }
                            }
                            sourceMedicalCard.p_DateDelete = DateTime.Now;
                            targetMedicalCard.p_DateMerge = DateTime.Now;
                            m_DataContextMegaTemplate.SaveChanges();
                            return true;
                        }
                        catch (Exception er)
                        {
                            MonitoringStub.Error("Error_MergeMedicalCards", "Не удалось присоединить медкарты", er, null, null);
                            return false;
                        }
                    }
                    else
                    {
                        MonitoringStub.Error("Error_MergeMedicalCards", "Не удалось найти медкарту адресат", null, null, null);
                        return false;
                    }
                }
                else
                {
                    MonitoringStub.Error("Error_MergeMedicalCards", "Не удалось найти медкарту источник", null, null, null);
                    return false;
                }
            }
            else
            {
                MonitoringStub.Error("Error_MedicalCardsFacade", "Не инициализирован фасад", null, null, null);
                return false;
            }
        }

        /// <summary>Архивирование медкарты</summary>
        /// <param name="a_Number">Номер медкарта</param>
        /// <param name="a_PatientID">ID пациента</param>
        public bool f_ArchiveMedicalCard(string a_Number, int a_PatientID)
        {
            if (m_DataContextMegaTemplate != null)
            {
                var medicalCard = f_GetMedicalCard(a_Number, a_PatientID);
                if (medicalCard != null)
                {
                    medicalCard.p_DateArchive = DateTime.Now;
                    m_DataContextMegaTemplate.SaveChanges();
                    return true;
                }
                else
                {
                    MonitoringStub.Error("Error_MergeMedicalCards", "Не удалось найти медкарту для архивирования", null, null, null);
                    return false;
                }
            }
            else
            {
                MonitoringStub.Error("Error_MedicalCardsFacade", "Не инициализирован фасад", null, null, null);
                return false;
            }
        }

        /// <summary>Получение списка медкарт пациента</summary>
        /// <param name="a_PatientID">ID пациента</param>
        public List<Cl_MedicalCard> f_GetMedicalCardsByPatient(int a_PatientID)
        {
            if (m_DataContextMegaTemplate != null)
            {
                return m_DataContextMegaTemplate.p_MedicalCards.Where(mc => mc.p_PatientID == a_PatientID).ToList();
            }
            else
            {
                MonitoringStub.Error("Error_MedicalCardsFacade", "Не инициализирован фасад", null, null, null);
                return null;
            }
        }
    }
}
