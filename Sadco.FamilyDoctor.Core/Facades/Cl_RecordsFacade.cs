using FD.dat.mon.stb.lib;
using Sadco.FamilyDoctor.Core.Data;
using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.Core.Formula;
using Sadco.FamilyDoctor.Core.Permision;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using static Sadco.FamilyDoctor.Core.Entities.Cl_Record;

namespace Sadco.FamilyDoctor.Core.Facades
{
    /// <summary>
    /// Фасад работы с записями
    /// </summary>
    public class Cl_RecordsFacade
    {
        private static Cl_RecordsFacade INSTANCE = new Cl_RecordsFacade();
        public static Cl_RecordsFacade f_GetInstance()
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

        private object[] f_GetValuesProperty(Cl_Record a_Record, Cl_FormulaConditionBlock a_Block)
        {
            var vals = new List<object>();
            if (!a_Block.p_IsOperand && a_Block.p_Object is Cl_Element)
            {
                var recValue = a_Record.p_Values.FirstOrDefault(v => v.p_ElementID == ((Cl_Element)a_Block.p_Object).p_ID);
                if (recValue != null)
                {
                    if (recValue.p_ValuesCatalog != null && recValue.p_ValuesCatalog.Length > 0)
                    {
                        vals.AddRange(recValue.p_ValuesCatalog.Select(vc => vc.p_ElementParam.p_Value));
                    }
                    else
                    {
                        if (recValue.p_Element != null && recValue.p_Element.p_IsNumber)
                        {
                            decimal dVal = 0;
                            if (decimal.TryParse(recValue.p_ValueUser, out dVal)) vals.Add(dVal);
                            else vals.Add(recValue.p_ValueUser);
                        }
                        else
                        {
                            vals.Add(recValue.p_ValueUser);
                        }
                    }
                }
                else return null;
            }
            else if (a_Block.p_Object is int)
            {
                vals.Add(decimal.Parse(a_Block.p_Object.ToString()));
            }
            else if (a_Block.p_Object is decimal)
            {
                vals.Add(a_Block.p_Object);
            }
            else if (a_Block.p_Object is Cl_ElementParam)
            {
                var elParam = (Cl_ElementParam)a_Block.p_Object;
                if (elParam.p_Element != null && elParam.p_Element.p_IsNumber)
                {
                    decimal dVal = 0;
                    if (decimal.TryParse(elParam.p_Value, out dVal)) vals.Add(dVal);
                    else vals.Add(elParam.p_Value);
                }
                else
                {
                    vals.Add(elParam.p_Value);
                }
            }
            else return null;
            return vals.ToArray();
        }

        private decimal? f_GetValuesProperty(Cl_Record a_Record, Cl_FormulaMathematicalBlock a_Block)
        {
            if (!a_Block.p_IsOperand && a_Block.p_Object is Cl_Element)
            {
                var recValue = a_Record.p_Values.FirstOrDefault(v => v.p_ElementID == ((Cl_Element)a_Block.p_Object).p_ID);
                if (recValue != null)
                {
                    if (recValue.p_ValuesCatalog != null && recValue.p_ValuesCatalog.Length > 0)
                    {
                        return null;
                    }
                    else
                    {
                        if (recValue.p_Element != null && recValue.p_Element.p_IsNumber)
                        {
                            decimal dVal = 0;
                            if (decimal.TryParse(recValue.p_ValueUser, out dVal)) return dVal;
                            else return null;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                else return null;
            }
            else if (a_Block.p_Object is int)
            {
                return decimal.Parse(a_Block.p_Object.ToString());
            }
            else if (a_Block.p_Object is decimal)
            {
                return (decimal)a_Block.p_Object;
            }
            else if (a_Block.p_Object is Cl_ElementParam)
            {
                var elParam = (Cl_ElementParam)a_Block.p_Object;
                if (elParam.p_Element != null && elParam.p_Element.p_IsNumber)
                {
                    decimal dVal = 0;
                    if (decimal.TryParse(elParam.p_Value, out dVal)) return dVal;
                    else return null;
                }
                else
                {
                    return null;
                }
            }
            else return null;
        }

        /// <summary>Получает видимость элемента по формуле</summary>
        public bool f_GetElementVisible(Cl_Record a_Record, string a_Formula)
        {
            if (a_Record == null || a_Record.p_Template == null) return false;
            if (string.IsNullOrWhiteSpace(a_Formula)) return true;
            var elements = Cl_TemplatesFacade.f_GetInstance().f_GetElements(a_Record.p_Template);
            if (elements != null && elements.Length > 0)
            {
                var blocks = Cl_FormulaFacade.f_GetInstance().f_GetConditionsBlocks(elements, a_Formula);
                if (blocks != null && blocks.Length > 2)
                {
                    //DataTable dt = new DataTable();
                    //var answer = dt.Compute("50 > 10 AND 20 > 12", "");
                    var blocksOr = new List<Cl_FormulaConditionBlock[]>();
                    var blocksAnd = new List<Cl_FormulaConditionBlock>();
                    for (int i = 0; i < blocks.Length; i++)
                    {
                        if (i % 2 == 1 && blocks[i].p_IsOperand && ((Cl_FormulaConditionBlock.E_Opers)blocks[i].p_Object) == Cl_FormulaConditionBlock.E_Opers.or)
                        {
                            blocksOr.Add(blocksAnd.ToArray());
                            blocksAnd.Clear();
                        }
                        else
                        {
                            blocksAnd.Add(blocks[i]);
                        }
                    }
                    if (blocksAnd.Count > 2) blocksOr.Add(blocksAnd.ToArray());

                    foreach (var blocksInOr in blocksOr)
                    {
                        bool result = false;
                        for (int i = 0; i < blocksInOr.Length; i += 4)
                        {
                            if (!blocksInOr[i].p_IsOperand && blocksInOr[i + 1].p_IsOperand && !blocksInOr[i + 2].p_IsOperand)
                            {
                                var oper = (Cl_FormulaConditionBlock.E_Opers)blocksInOr[i + 1].p_Object;
                                object[] vals1 = f_GetValuesProperty(a_Record, blocksInOr[i]);
                                object[] vals2 = f_GetValuesProperty(a_Record, blocksInOr[i + 2]);
                                if (vals1 != null && vals2 != null)
                                {
                                    if (vals1.Length == 1 && vals2.Length == 1)
                                    {
                                        if (vals1[0] != null && vals2[0] != null)
                                        {
                                            if (vals1[0] is decimal && vals2[0] is decimal)
                                            {
                                                decimal dVal1 = (decimal)vals1[0];
                                                decimal dVal2 = (decimal)vals2[0];
                                                if (oper == Cl_FormulaConditionBlock.E_Opers.equals)
                                                    result = dVal1 == dVal2;
                                                else if (oper == Cl_FormulaConditionBlock.E_Opers.notEquals)
                                                    result = dVal1 != dVal2;
                                                else if (oper == Cl_FormulaConditionBlock.E_Opers.more)
                                                    result = dVal1 > dVal2;
                                                else if (oper == Cl_FormulaConditionBlock.E_Opers.less)
                                                    result = dVal1 < dVal2;
                                                else result = false;
                                            }
                                            else if (oper == Cl_FormulaConditionBlock.E_Opers.equals)
                                            {
                                                result = vals1[0].ToString() == vals2[0].ToString();
                                            }
                                            else if (oper == Cl_FormulaConditionBlock.E_Opers.notEquals)
                                            {
                                                result = vals1[0].ToString() != vals2[0].ToString();
                                            }
                                            else result = false;
                                        }
                                        else result = false;
                                    }
                                    else if (oper == Cl_FormulaConditionBlock.E_Opers.equals)
                                    {
                                        result = vals1.Any(vals2.Contains);
                                        if (!result) result = false;
                                    }
                                    else if (oper == Cl_FormulaConditionBlock.E_Opers.notEquals)
                                    {
                                        result = vals1.Any(vals2.Contains);
                                        if (result) result = false;
                                    }
                                    else result = false;
                                }
                                else result = false;
                            }
                            else return false;
                            if (!result) break;
                        }
                        if (result) return true;
                    }
                }
            }
            return false;
        }


        /// <summary>Получает видимость элемента по формуле</summary>
        public decimal? f_GetElementMathematicValue(Cl_Record a_Record, string a_Formula)
        {
            if (a_Record == null || a_Record.p_Template == null) return null;
            if (string.IsNullOrWhiteSpace(a_Formula)) return 0;
            var elements = Cl_TemplatesFacade.f_GetInstance().f_GetElements(a_Record.p_Template);
            if (elements != null && elements.Length > 0)
            {
                var blocks = Cl_FormulaFacade.f_GetInstance().f_GetMathematicalsBlocks(elements, a_Formula);
                if (blocks != null)
                {
                    if (blocks.Length == 0) return 0;
                    if (blocks[0].p_IsOperand) return null;


                    decimal? dVal = f_GetValuesProperty(a_Record, blocks[0]);
                    if (dVal != null)
                    {
                        var result = (decimal)dVal;
                        Cl_FormulaMathematicalBlock oldOper = null;
                        for (int i = 1; i < blocks.Length; i++)
                        {
                            if (i % 2 == 0 && !blocks[i].p_IsOperand)
                            {
                                if (oldOper != null && oldOper.p_Object is Cl_FormulaMathematicalBlock.E_Opers)
                                {
                                    var oper = (Cl_FormulaMathematicalBlock.E_Opers)oldOper.p_Object;
                                    dVal = f_GetValuesProperty(a_Record, blocks[i]);
                                    if (dVal != null)
                                    {
                                        if (oper == Cl_FormulaMathematicalBlock.E_Opers.minus)
                                            result -= (decimal)dVal;
                                        else if (oper == Cl_FormulaMathematicalBlock.E_Opers.plus)
                                            result += (decimal)dVal;
                                        else if (oper == Cl_FormulaMathematicalBlock.E_Opers.carve)
                                            result /= (decimal)dVal;
                                        else if (oper == Cl_FormulaMathematicalBlock.E_Opers.multiply)
                                            result *= (decimal)dVal;
                                        else
                                            return null;
                                    }
                                    else return null;
                                }
                                else return null;
                            }
                            else if (i % 2 == 1 && blocks[i].p_IsOperand)
                            {
                                oldOper = blocks[i];
                            }
                            else return null;
                        }
                        return result;
                    }
                    else return null;
                }
            }
            return null;
        }

        /// <summary>Добавление записей в БД</summary>
        public bool f_AddRecords(IEnumerable<Cl_Record> a_Records)
        {
            using (var transaction = m_DataContextMegaTemplate.Database.BeginTransaction())
            {
                try
                {
                    m_DataContextMegaTemplate.p_Records.AddRange(a_Records);
                    m_DataContextMegaTemplate.SaveChanges();
                    foreach (var record in a_Records)
                    {
                        record.p_RecordID = record.p_ID;
                    }
                    m_DataContextMegaTemplate.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MonitoringStub.Error("Error_Editor", "При сохранении изменений записей произошла ошибка", ex, null, null);
                    return false;
                }
            }
        }

        private Cl_RecordPatternParam f_GetRecordPatternParam(Cl_RecordPatternValue a_RecordPatternValue, Cl_RecordParam a_RecordParam)
        {
            Cl_RecordPatternParam param = null;
            if (a_RecordPatternValue != null && a_RecordParam != null)
            {
                param = new Cl_RecordPatternParam();
                param.p_RecordPatternValueID = a_RecordPatternValue.p_ID;
                param.p_RecordPatternValue = a_RecordPatternValue;
                param.p_ElementParamID = a_RecordParam.p_ElementParamID;
                param.p_ElementParam = a_RecordParam.p_ElementParam;
                param.p_IsDop = a_RecordParam.p_IsDop;

            }
            return param;
        }

        private Cl_RecordParam f_GetRecordParam(Cl_RecordValue a_RecordValue, Cl_RecordPatternParam a_RecordPatternParam)
        {
            Cl_RecordParam param = null;
            if (a_RecordValue != null && a_RecordPatternParam != null)
            {
                param = new Cl_RecordParam();
                param.p_RecordValueID = a_RecordValue.p_ID;
                param.p_RecordValue = a_RecordValue;
                param.p_ElementParamID = a_RecordPatternParam.p_ElementParamID;
                param.p_ElementParam = a_RecordPatternParam.p_ElementParam;
                param.p_IsDop = a_RecordPatternParam.p_IsDop;

            }
            return param;
        }

        private Cl_RecordPatternValue f_GetRecordPatternValue(Cl_RecordPattern a_RecordPattern, Cl_RecordValue a_RecordValue)
        {
            Cl_RecordPatternValue val = null;
            if (a_RecordPattern != null && a_RecordValue != null)
            {
                val = new Cl_RecordPatternValue();
                val.p_RecordPatternID = a_RecordPattern.p_ID;
                val.p_RecordPattern = a_RecordPattern;
                val.p_ElementID = a_RecordValue.p_ElementID;
                val.p_Element = a_RecordValue.p_Element;
                val.p_ImageBytes = a_RecordValue.p_ImageBytes;
                val.p_Image = a_RecordValue.p_Image;
                val.p_ValueUser = a_RecordValue.p_ValueUser;
                val.p_ValueDopUser = a_RecordValue.p_ValueDopUser;
                val.p_Params = a_RecordValue.p_Params.Select(p => f_GetRecordPatternParam(val, p)).ToList();

            }
            return val;
        }

        private Cl_RecordValue f_GetRecordValue(Cl_Record a_Record, Cl_RecordPatternValue a_RecordPatternValue)
        {
            Cl_RecordValue val = null;
            if (a_Record != null && a_RecordPatternValue != null)
            {
                val = new Cl_RecordValue();
                val.p_RecordID = a_Record.p_ID;
                val.p_Record = a_Record;
                val.p_ElementID = a_RecordPatternValue.p_ElementID;
                val.p_Element = a_RecordPatternValue.p_Element;
                val.p_ImageBytes = a_RecordPatternValue.p_ImageBytes;
                val.p_Image = a_RecordPatternValue.p_Image;
                val.p_ValueUser = a_RecordPatternValue.p_ValueUser;
                val.p_ValueDopUser = a_RecordPatternValue.p_ValueDopUser;
                val.p_Params = a_RecordPatternValue.p_Params.Select(p => f_GetRecordParam(val, p)).ToList();

            }
            return val;
        }

        /// <summary>Получение нового паттерна записей</summary>
        /// <param name="a_Record">Запись</param>
        /// <returns>Новый паттерн записей</returns>
        public Cl_RecordPattern f_GetNewRecordPattern(Cl_Record a_Record)
        {
            return f_GetNewRecordPattern("Новый паттерн", a_Record);
        }

        /// <summary>Получение нового паттерна записей</summary>
        /// <param name="a_Record">Запись</param>
        /// <param name="a_PatternName">Название паттерна</param>
        /// <returns>Новый паттерн записей</returns>
        public Cl_RecordPattern f_GetNewRecordPattern(string a_PatternName, Cl_Record a_Record)
        {
            Cl_RecordPattern pattern = null;
            if (!string.IsNullOrEmpty(a_PatternName) && a_Record != null)
            {
                pattern = new Cl_RecordPattern();
                pattern.p_Name = a_PatternName;
                pattern.p_ClinicName = a_Record.p_ClinicName;
                pattern.p_DoctorID = a_Record.p_DoctorID;
                pattern.p_DoctorSurName = a_Record.p_DoctorSurName;
                pattern.p_DoctorName = a_Record.p_DoctorName;
                pattern.p_DoctorLastName = a_Record.p_DoctorLastName;
                pattern.p_CategoryClinicID = a_Record.p_CategoryClinicID;
                pattern.p_CategoryClinic = a_Record.p_CategoryClinic;
                pattern.p_CategoryTotalID = a_Record.p_CategoryTotalID;
                pattern.p_CategoryTotal = a_Record.p_CategoryTotal;
                pattern.f_SetTemplate(a_Record.p_Template);
                pattern.p_Title = a_Record.p_Title;
                pattern.p_Values = a_Record.p_Values.Select(v => f_GetRecordPatternValue(pattern, v)).ToList();
            }
            return pattern;
        }

        /// <summary>Получение паттерна записей</summary>
        /// <param name="a_Record">Запись</param>
        /// <returns>Паттерн записей</returns>
        public Cl_Record f_GetNewRecord(Cl_RecordPattern a_RecordPattern)
        {
            Cl_Record record = null;
            if (a_RecordPattern != null)
            {
                record = new Cl_Record();
                record.p_DateCreate = DateTime.Now;
                record.p_DateLastChange = record.p_DateForming = record.p_DateCreate;
                record.p_MedicalCardID = Cl_SessionFacade.f_GetInstance().p_MedCardNumber;
                record.p_ClinicName = Cl_SessionFacade.f_GetInstance().p_Doctor.p_ClinicName;
                record.f_SetDoctor(Cl_SessionFacade.f_GetInstance().p_Doctor);
                record.f_SetPatient(Cl_SessionFacade.f_GetInstance().p_Patient);
                record.p_CategoryClinicID = a_RecordPattern.p_CategoryClinicID;
                record.p_CategoryClinic = a_RecordPattern.p_CategoryClinic;
                record.p_CategoryTotalID = a_RecordPattern.p_CategoryTotalID;
                record.p_CategoryTotal = a_RecordPattern.p_CategoryTotal;
                record.f_SetTemplate(a_RecordPattern.p_Template);
                record.p_Title = a_RecordPattern.p_Title;
                record.p_Values = a_RecordPattern.p_Values.Select(v => f_GetRecordValue(record, v)).ToList();
            }
            return record;
        }

        /// <summary>Полечение новой записи</summary>
        /// <param name="a_CategoryTotal">Общая категория</param>
        /// <param name="a_CategoryClinic">Клиническая категория</param>
        /// <param name="a_Title">Заголовок записи</param>
        /// <param name="a_ClinicName">Название клиники</param>
        /// <param name="a_MedicalCardID">ID медицинской карты</param>
        /// <param name="a_DoctorID">ID доктора</param>
        /// <param name="a_DoctorSurName">Фамиля доктора</param>
        /// <param name="a_DoctorName">Имя доктора</param>
        /// <param name="a_DoctorLastName">Отчество доктора</param>
        /// <param name="a_PatientID">ID пациента</param>
        /// <param name="a_PatientSex">Пол пациента</param>
        /// <param name="a_PatientSurName">Фамиля пациента</param>
        /// <param name="a_PatientName">Имя пациента</param>
        /// <param name="a_PatientLastName">Отчество пациента</param>
        /// <param name="a_PatientDateBirth">Дата рождения пациента</param>
        /// <returns>Флаг успешного создания записи</returns>
        private Cl_Record f_GetRecord(Cl_Category a_CategoryTotal, Cl_Category a_CategoryClinic, string a_Title, string a_ClinicName, int a_MedicalCardID,
            int a_DoctorID, string a_DoctorSurName, string a_DoctorName, string a_DoctorLastName,
            int a_PatientID, Cl_User.E_Sex a_PatientSex, string a_PatientSurName, string a_PatientName, string a_PatientLastName, DateTime a_PatientDateBirth)
        {
            Cl_Record record = new Cl_Record();
            record.p_Version = 1;
            record.p_DateCreate = record.p_DateForming = record.p_DateLastChange = DateTime.Now;
            if (a_CategoryTotal != null)
            {
                record.p_CategoryTotalID = a_CategoryTotal.p_ID;
                record.p_CategoryTotal = a_CategoryTotal;
            }
            if (a_CategoryClinic != null)
            {
                record.p_CategoryClinicID = a_CategoryClinic.p_ID;
                record.p_CategoryClinic = a_CategoryClinic;
            }
            record.p_Title = a_Title;
            record.p_ClinicName = a_ClinicName;
            record.p_MedicalCardID = a_MedicalCardID;
            record.p_DoctorID = a_DoctorID;
            record.p_DoctorSurName = a_DoctorSurName;
            record.p_DoctorName = a_DoctorName;
            record.p_DoctorLastName = a_DoctorLastName;
            record.p_PatientID = a_PatientID;
            record.p_PatientSex = a_PatientSex;
            record.p_PatientSurName = a_PatientSurName;
            record.p_PatientName = a_PatientName;
            record.p_PatientLastName = a_PatientLastName;
            record.p_PatientDateBirth = a_PatientDateBirth;
            return record;
        }

        /// <summary>Создание новой записи</summary>
        /// <param name="a_CategoryTotal">Общая категория</param>
        /// <param name="a_CategoryClinic">Клиническая категория</param>
        /// <param name="a_Title">Заголовок записи</param>
        /// <param name="a_ClinicName">Название клиники</param>
        /// <param name="a_MedicalCardID">ID медицинской карты</param>
        /// <param name="a_Type">Тип записи</param>
        /// <param name="a_DoctorID">ID доктора</param>
        /// <param name="a_DoctorSurName">Фамиля доктора</param>
        /// <param name="a_DoctorName">Имя доктора</param>
        /// <param name="a_DoctorLastName">Отчество доктора</param>
        /// <param name="a_PatientID">ID пациента</param>
        /// <param name="a_PatientSex">Пол пациента</param>
        /// <param name="a_PatientSurName">Фамиля пациента</param>
        /// <param name="a_PatientName">Имя пациента</param>
        /// <param name="a_PatientLastName">Отчество пациента</param>
        /// <param name="a_PatientDateBirth">Дата рождения пациента</param>
        /// <param name="a_Template">Шаблон</param>
        /// <param name="a_Values">Значения записи</param>
        /// <returns>Флаг успешного создания записи</returns>
        public bool f_CreateRecord(Cl_Category a_CategoryTotal, Cl_Category a_CategoryClinic, string a_Title, string a_ClinicName, int a_MedicalCardID,
            int a_DoctorID, string a_DoctorSurName, string a_DoctorName, string a_DoctorLastName,
            int a_PatientID, Cl_User.E_Sex a_PatientSex, string a_PatientSurName, string a_PatientName, string a_PatientLastName, DateTime a_PatientDateBirth,
            Cl_Template a_Template, IEnumerable<Cl_RecordValue> a_Values)
        {
            if (a_Template != null)
            {
                Cl_Record record = f_GetRecord(a_CategoryTotal, a_CategoryClinic, a_Title, a_ClinicName, a_MedicalCardID,
                    a_DoctorID, a_DoctorSurName, a_DoctorName, a_DoctorLastName,
                    a_PatientID, a_PatientSex, a_PatientSurName, a_PatientName, a_PatientLastName, a_PatientDateBirth);
                if (record != null)
                {
                    record.p_TemplateID = a_Template.p_ID;
                    return f_CreateRecord(record, a_Values);
                }
            }
            return false;
        }

        /// <summary>Создание новой записи</summary>
        /// <param name="a_CategoryTotal">Общая категория</param>
        /// <param name="a_CategoryClinic">Клиническая категория</param>
        /// <param name="a_Title">Заголовок записи</param>
        /// <param name="a_ClinicName">Название клиники</param>
        /// <param name="a_MedicalCardID">ID медицинской карты</param>
        /// <param name="a_DoctorID">ID доктора</param>
        /// <param name="a_DoctorSurName">Фамиля доктора</param>
        /// <param name="a_DoctorName">Имя доктора</param>
        /// <param name="a_DoctorLastName">Отчество доктора</param>
        /// <param name="a_PatientID">ID пациента</param>
        /// <param name="a_PatientSex">Пол пациента</param>
        /// <param name="a_PatientSurName">Фамиля пациента</param>
        /// <param name="a_PatientName">Имя пациента</param>
        /// <param name="a_PatientLastName">Отчество пациента</param>
        /// <param name="a_PatientDateBirth">Дата рождения пациента</param>
        /// <param name="a_RecordFileType">Тип файла</param>
        /// <param name="a_FileBytes">Данные файла записи</param>
        /// <returns>Флаг успешного создания записи</returns>
        public bool f_CreateRecord(Cl_Category a_CategoryTotal, Cl_Category a_CategoryClinic, string a_Title, string a_ClinicName, int a_MedicalCardID,
            int a_DoctorID, string a_DoctorSurName, string a_DoctorName, string a_DoctorLastName,
            int a_PatientID, Cl_User.E_Sex a_PatientSex, string a_PatientSurName, string a_PatientName, string a_PatientLastName, DateTime a_PatientDateBirth,
            E_RecordFileType a_RecordFileType, byte[] a_FileBytes)
        {
            Cl_Record record = f_GetRecord(a_CategoryTotal, a_CategoryClinic, a_Title, a_ClinicName, a_MedicalCardID,
                a_DoctorID, a_DoctorSurName, a_DoctorName, a_DoctorLastName,
                a_PatientID, a_PatientSex, a_PatientSurName, a_PatientName, a_PatientLastName, a_PatientDateBirth);
            return f_CreateRecord(record, a_RecordFileType, a_FileBytes);
        }



        /// <summary>Создание новой записи</summary>
        /// <param name="a_Record">Новая запись</param>
        /// <param name="a_Values">Значения записи</param>
        /// <returns>Флаг успешного создания записи</returns>
        public bool f_CreateRecord(Cl_Record a_Record, IEnumerable<Cl_RecordValue> a_Values)
        {
            if (m_DataContextMegaTemplate != null && a_Record != null && a_Record.p_TemplateID != null && a_Record.f_IsValid() && a_Values != null && a_Values.Count() > 0)
            {
                using (var transaction = m_DataContextMegaTemplate.Database.BeginTransaction())
                {
                    try
                    {
                        a_Record.p_Version = 1;
                        a_Record.p_Type = E_RecordType.ByTemplate;
                        a_Record.p_Values.AddRange(a_Values);
                        m_DataContextMegaTemplate.p_Records.Add(a_Record);
                        m_DataContextMegaTemplate.SaveChanges();
                        a_Record.p_FileType = E_RecordFileType.HTML;
                        a_Record.p_HTMLDoctor = a_Record.f_GetHTMLDoctor();
                        a_Record.p_HTMLPatient = a_Record.f_GetHTMLPatient();
                        a_Record.p_RecordID = a_Record.p_ID;
                        m_DataContextMegaTemplate.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                    }
                }
            }
            return false;
        }

        /// <summary>Создание новой записи</summary>
        /// <param name="a_Record">Новая запись</param>
        /// <param name="a_RecordFileType">Тип файла</param>
        /// <param name="a_FileBytes">Данные файла записи</param>
        /// <returns>Флаг успешного создания записи</returns>
        public bool f_CreateRecord(Cl_Record a_Record, E_RecordFileType a_RecordFileType, byte[] a_FileBytes)
        {
            if (m_DataContextMegaTemplate != null && a_Record != null && a_Record.f_IsValid() && a_FileBytes != null)
            {
                using (var transaction = m_DataContextMegaTemplate.Database.BeginTransaction())
                {
                    try
                    {
                        a_Record.p_Version = 1; a_Record.p_Type = E_RecordType.FinishedFile;
                        a_Record.p_IsAutomatic = true;
                        a_Record.p_FileType = a_RecordFileType;
                        a_Record.p_FileBytes = a_FileBytes;
                        m_DataContextMegaTemplate.p_Records.Add(a_Record);
                        m_DataContextMegaTemplate.SaveChanges();
                        a_Record.p_RecordID = a_Record.p_ID;
                        m_DataContextMegaTemplate.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                    }
                }
            }
            return false;
        }
    }
}
