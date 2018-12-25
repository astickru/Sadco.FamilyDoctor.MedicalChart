using FD.dat.mon.stb.lib;
using Sadco.FamilyDoctor.Core.Data;
using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.Core.Formula;
using Sadco.FamilyDoctor.Core.Permision;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

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
        private string m_LocalResourcesPath = "";
        private string m_SeparatorMulti = ", ";

        /// <summary>Инициализация фасада</summary>
        public bool f_Init(Cl_DataContextMegaTemplate a_DataContextMegaTemplate, string a_LocalResourcesPath)
        {
            m_DataContextMegaTemplate = a_DataContextMegaTemplate;
            m_LocalResourcesPath = a_LocalResourcesPath;
            m_IsInit = m_DataContextMegaTemplate != null;
            return m_IsInit;
        }

        /// <summary>Получение локального пути к общим ресурсам</summary>
        public string f_GetLocalResourcesPath()
        {
            return m_LocalResourcesPath;
        }

        /// <summary>Получение относительного пути к ресурсам записи</summary>
        public string f_GetLocalResourcesRelativePath(Cl_Record a_Record)
        {
            return $"{a_Record.p_ClinicName}/{a_Record.p_MedicalCard.p_PatientID}/{a_Record.p_ID}";
        }

        /// <summary>Получение относительного пути к файлу записи</summary>
        public string f_GetFileName(Cl_Record a_Record)
        {
            return $"file{f_GetFileExtension(a_Record.p_FileType)}";
        }

        /// <summary>Получение относительного пути к файлу записи</summary>
        public string f_GetLocalResourcesRelativeFilePath(Cl_Record a_Record)
        {
            return $"{a_Record.p_ClinicName}/{a_Record.p_MedicalCard.p_PatientID}/{a_Record.p_ID}/{f_GetFileName(a_Record)}";
        }

        /// <summary>Получение абсолютного пути к ресурсам записи</summary>
        public string f_GetLocalResourcesAbsolutePath(Cl_Record a_Record)
        {
            return $"{m_LocalResourcesPath}/{f_GetLocalResourcesRelativePath(a_Record)}";
        }

        /// <summary>Получение типа файла</summary>
        public E_RecordFileType? f_GetFileType(string a_FileName)
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

        /// <summary>Получение типа файла</summary>
        public string f_GetFileExtension(E_RecordFileType fileType)
        {
            if (fileType == E_RecordFileType.HTML)
            {
                return ".html";
            }
            else if (fileType == E_RecordFileType.PDF)
            {
                return ".pdf";
            }
            else if (fileType == E_RecordFileType.JPG)
            {
                return ".jpg";
            }
            else if (fileType == E_RecordFileType.JPEG)
            {
                return ".jpeg";
            }
            else if (fileType == E_RecordFileType.JPE)
            {
                return ".jpe";
            }
            else if (fileType == E_RecordFileType.JFIF)
            {
                return ".jfif";
            }
            else if (fileType == E_RecordFileType.JIF)
            {
                return ".jif";
            }
            else if (fileType == E_RecordFileType.PNG)
            {
                return ".png";
            }
            else if (fileType == E_RecordFileType.PNG)
            {
                return ".gif";
            }
            else if (fileType == E_RecordFileType.XML)
            {
                return ".xml";
            }
            else
            {
                return null;
            }
        }

        /// <summary>Получение элемента возраста</summary>
        public Cl_Element f_GetAgeElement(string a_Tag)
        {
            if (a_Tag == "age")
            {
                return new Cl_Element()
                {
                    p_Name = "Возраст",
                    p_Tag = "age",
                    p_IsNumber = true
                };
            }
            else
            {
                return null;
            }
        }

        /// <summary>Получение элемента пола</summary>
        public Cl_Element f_GetGenderElement(string a_Tag)
        {
            if (a_Tag == "gender")
            {
                return new Cl_Element()
                {
                    p_Name = "Пол",
                    p_Tag = "gender",
                    p_IsNumber = false
                };
            }
            else
            {
                return null;
            }
        }

        /// <summary>Получение элемента шаблона</summary>
        private Cl_TemplateElement f_GetTemplateElement(I_RecordValue a_RecordValue, Cl_Template a_Template)
        {
            if (a_Template != null && a_Template.p_TemplateElements != null)
            {
                foreach (var te in a_Template.p_TemplateElements)
                {
                    if (te.p_ChildElement == a_RecordValue.p_Element)
                    {
                        return te;
                    }
                    else
                    {
                        var t = f_GetTemplateElement(a_RecordValue, te.p_ChildTemplate);
                        if (t != null)
                            return t;
                    }
                }
            }
            return null;
        }

        /// <summary>Получение файла из сервер через БД</summary>
        public byte[] f_GetFileFromSql(Cl_Record record)
        {
            if (record != null && !string.IsNullOrWhiteSpace(record.p_FilePath))
            {
                var prm_path = new SqlParameter("@path", f_GetLocalResourcesPath() + "/" + record.p_FilePath);
                var prm_binaryout = new SqlParameter("@binaryout", SqlDbType.VarBinary, -1);
                prm_binaryout.Direction = ParameterDirection.Output;
                var dbResult = m_DataContextMegaTemplate.Database.ExecuteSqlCommand("GetFile @path, @binaryout out", prm_path, prm_binaryout);
                return prm_binaryout.Value as byte[];
            }
            else
            {
                return null;
            }
        }

        /// <summary>Сохранение файла на сервер через БД</summary>
        public void f_SaveFileFromSql(Cl_Record record)
        {
            if (record != null && record.p_FileBytes != null && !string.IsNullOrWhiteSpace(record.p_FilePath))
            {
                f_SaveFileFromSql(f_GetLocalResourcesRelativePath(record), f_GetFileName(record), record.p_FileBytes);
            }
        }

        /// <summary>Сохранение файла на сервер через БД</summary>
        public void f_SaveFileFromSql(string relativePath, string fileName, byte[] data)
        {
            if (!string.IsNullOrWhiteSpace(relativePath) && !string.IsNullOrWhiteSpace(fileName) && data != null && data.Length > 0)
            {
                var prm_root_path = new SqlParameter("@root_path", f_GetLocalResourcesPath());
                var prm_path = new SqlParameter("@path", relativePath);
                var prm_file_name = new SqlParameter("@file_name", fileName);
                var prm_var = new SqlParameter("@var", data);
                m_DataContextMegaTemplate.Database.ExecuteSqlCommand("CreateFile @root_path, @path, @file_name, @var", prm_root_path, prm_path, prm_file_name, prm_var);
            }
        }

        /// <summary>Удаление файла с сервера через БД</summary>
        public void f_DeleteFileFromSql(Cl_Record record)
        {
            if (record != null && !string.IsNullOrWhiteSpace(record.p_FilePath))
            {
                var prm_path = new SqlParameter("@path", f_GetLocalResourcesPath() + "/" + record.p_FilePath);
                m_DataContextMegaTemplate.Database.ExecuteSqlCommand("RemoveFile @path", prm_path);
            }
        }

        /// <summary>Получение элемента шаблона</summary>
        public Cl_TemplateElement f_GetTemplateElement(Cl_RecordValue a_RecordValue)
        {
            if (a_RecordValue != null && a_RecordValue.p_Record != null)
            {
                return f_GetTemplateElement(a_RecordValue, a_RecordValue.p_Record.p_Template);
            }
            return null;
        }

        private object[] f_GetValuesProperty(I_Record a_Record, Cl_FormulaConditionBlock a_Block)
        {
            var vals = new List<object>();
            if (!a_Block.p_IsOperand && a_Block.p_Object is Cl_Element)
            {
                var el = (Cl_Element)a_Block.p_Object;
                if (el.p_Tag == "age")
                {
                    if (a_Record is Cl_Record)
                    {
                        var record = (Cl_Record)a_Record;
                        vals.Add((decimal)record.p_MedicalCard.f_GetPatientAgeByYear(record.p_DateCreate));
                    }
                }
                else if (el.p_Tag == "gender")
                {
                    if (a_Record is Cl_Record)
                    {
                        var record = (Cl_Record)a_Record;
                        vals.Add(Enum.GetName(typeof(Cl_User.E_Sex), record.p_MedicalCard.p_PatientSex).ToLower());
                    }
                }
                else
                {
                    var recValue = a_Record.f_GetRecordsValues().FirstOrDefault(v => v.p_ElementID == el.p_ID);
                    if (recValue != null)
                    {
                        if (recValue.p_ValuesCatalog != null && recValue.p_ValuesCatalog.Length > 0)
                        {
                            var valsObjects = recValue.p_ValuesCatalog.Select(vc => vc.p_ElementParam.p_Value);
                            foreach (var val in valsObjects)
                            {
                                if (recValue.p_Element != null && recValue.p_Element.p_IsNumber)
                                {
                                    decimal dVal = 0;
                                    if (decimal.TryParse(val, out dVal)) vals.Add(dVal);
                                    else vals.Add(dVal);
                                }
                                else
                                {
                                    vals.Add(val);
                                }
                            }
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
            }
            else if (a_Block.p_Object is int)
            {
                vals.Add(decimal.Parse(a_Block.p_Object.ToString()));
            }
            else if (a_Block.p_Object is decimal)
            {
                vals.Add(a_Block.p_Object);
            }
            else if (a_Block.p_Object is Cl_User.E_Sex)
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
                var element = (Cl_Element)a_Block.p_Object;
                if (element.p_IsNumber && !string.IsNullOrWhiteSpace(element.p_NumberFormula))
                {
                    return f_GetElementMathematicValue(a_Record, element);
                }
                else
                {
                    var recValue = a_Record.p_Values.FirstOrDefault(v => v.p_ElementID == element.p_ID);
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
        public bool f_GetElementVisible(I_Record a_Record, string a_Formula)
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
                                                result = vals1[0].ToString().ToLower() == vals2[0].ToString().ToLower();
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

        /// <summary>Получает результат элемента по формуле</summary>
        public decimal? f_GetElementMathematicValue(Cl_Record a_Record, Cl_Element a_Element)
        {
            if (a_Record == null || a_Record.p_Template == null || a_Element == null) return null;
            if (string.IsNullOrWhiteSpace(a_Element.p_NumberFormula)) return 0;
            var elements = Cl_TemplatesFacade.f_GetInstance().f_GetElements(a_Record.p_Template);
            if (elements != null && elements.Length > 0)
            {
                var blocks = Cl_FormulaFacade.f_GetInstance().f_GetMathematicalsBlocks(elements, a_Element.p_NumberFormula);
                if (blocks != null)
                {
                    if (blocks.Length == 0) return 0;
                    if (blocks[0].p_IsOperand) return null;
                    if ("tag_" + a_Element.p_Tag == blocks[0].f_GetTextFromBlock())
                    {
                        MonitoringStub.Warning("В формуле имеется зацикливание.");
                        return null;
                    }
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
                                    if ("tag_" + a_Element.p_Tag == blocks[i].f_GetTextFromBlock())
                                    {
                                        MonitoringStub.Warning("В формуле имеется зацикливание.");
                                        return null;
                                    }
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

        /// <summary>Получение HTML текста для клиента</summary>
        public string f_GetHTMLPatient(Cl_Record a_Record, I_RecordValue a_RecordValue, bool a_IsTable, decimal? a_Min, decimal? a_Max)
        {
            return f_GetHTML(a_Record, a_RecordValue, false, a_IsTable, a_Min, a_Max);
        }

        /// <summary>Получение HTML текста для пользователя</summary>
        public string f_GetHTMLDoctor(Cl_Record a_Record, I_RecordValue a_RecordValue, bool a_IsTable, decimal? a_Min, decimal? a_Max)
        {
            return f_GetHTML(a_Record, a_RecordValue, true, a_IsTable, a_Min, a_Max);
        }

        public string f_GetHTMLHeader(Cl_TemplateElement a_TemplateElement)
        {
            if (a_TemplateElement.p_ChildElement != null && a_TemplateElement.p_ChildElement.p_IsHeader)
            {
                return $"<p style='text-align:center;font-size:{Cl_App.f_GetRecordSetting().p_SizeH1 - 2 * (a_TemplateElement.p_ChildElement.p_HeaderLevel - 1)}'>{a_TemplateElement.p_Value}</p>";
            }
            return null;
        }

        private string f_GetValWithColorForHtml(I_RecordValue a_RecordValue, IEnumerable<string> a_Vals, decimal? a_Min, decimal? a_Max)
        {
            if (a_RecordValue.p_Element.p_IsNumber)
            {
                decimal dVal = 0;
                var resVals = new List<string>();
                foreach (var text in a_Vals)
                {
                    if (decimal.TryParse(text, out dVal))
                    {
                        if ((a_Min != null && a_Min > dVal) || (a_Max != null && a_Max < dVal))
                            resVals.Add(string.Format("<span style=\"color=red\">{0}</span>", text));
                        else
                            resVals.Add(text);
                    }
                    else
                    {
                        resVals.Add(string.Format("<span style=\"color=red\">{0}</span>", text));
                    }
                }
                return string.Join(m_SeparatorMulti, resVals);
            }
            return string.Join(m_SeparatorMulti, a_Vals);
        }

        private string f_GetValForHTML(I_RecordValue a_RecordValue, decimal? a_Min, decimal? a_Max)
        {
            string val = "";
            if (a_RecordValue.p_Element.p_IsTextFromCatalog)
            {
                if (a_RecordValue.p_ValuesCatalog != null && a_RecordValue.p_ValuesCatalog.Length > 0)
                {
                    var vals = a_RecordValue.p_ValuesCatalog.Select(vc => vc.p_ElementParam.p_Value);
                    val = f_GetValWithColorForHtml(a_RecordValue, vals, a_Min, a_Max);
                    if (a_RecordValue.p_ValuesDopCatalog != null && a_RecordValue.p_ValuesDopCatalog.Length > 0)
                    {
                        var valsDop = a_RecordValue.p_ValuesDopCatalog.Select(vc => vc.p_ElementParam.p_Value);
                        string valDop = f_GetValWithColorForHtml(a_RecordValue, valsDop, a_Min, a_Max);
                        val = string.Format("{0}: {1}, {2}: {3}", a_RecordValue.p_Element.p_SymmetryParamLeft, val, a_RecordValue.p_Element.p_SymmetryParamRight, valDop);
                    }
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(a_RecordValue.p_ValueUser))
                    val = f_GetValWithColorForHtml(a_RecordValue, new string[] { a_RecordValue.p_ValueUser }, a_Min, a_Max);
                if (!string.IsNullOrWhiteSpace(a_RecordValue.p_ValueDopUser))
                    val = string.Format("{0}: {1}, {2}: {3}", a_RecordValue.p_Element.p_SymmetryParamLeft, f_GetValWithColorForHtml(a_RecordValue, new string[] { a_RecordValue.p_ValueUser }, a_Min, a_Max),
                        a_RecordValue.p_Element.p_SymmetryParamRight, f_GetValWithColorForHtml(a_RecordValue, new string[] { a_RecordValue.p_ValueDopUser }, a_Min, a_Max));
            }
            return val;
        }

        /// <summary>Получение HTML текста записи</summary>
        private string f_GetHTML(Cl_Record a_Record, I_RecordValue a_RecordValue, bool a_IsDoctor, bool a_IsTable, decimal? a_Min, decimal? a_Max)
        {
            var html = "";
            if (a_RecordValue.p_Element != null && a_RecordValue.p_Element.p_Visible && f_GetElementVisible(a_Record, a_RecordValue.p_Element.p_VisibilityFormula) && (a_IsDoctor || a_RecordValue.p_Element.p_VisiblePatient))
            {
                if (a_IsTable)
                {
                    string val = f_GetValForHTML(a_RecordValue, a_Min, a_Max);
                    var partNorm = a_RecordValue.p_Element.f_GetPartNormValue(a_Record.p_MedicalCard.p_PatientSex, a_Record.p_MedicalCard.f_GetPatientAgeByYear(a_Record.p_DateCreate));
                    html = string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>", a_RecordValue.p_Element.p_PartPre, val, a_RecordValue.p_Element.p_PartPost, partNorm);
                }
                else if (a_RecordValue.p_Element.p_IsText)
                {
                    string tag = "span";
                    if (a_RecordValue.p_Element.p_ElementType == Cl_Element.E_ElementsTypes.Line || a_RecordValue.p_Element.p_ElementType == Cl_Element.E_ElementsTypes.Bigbox)
                        tag = "p";
                    string val = f_GetValForHTML(a_RecordValue, a_Min, a_Max);
                    if (a_RecordValue.p_Element.p_IsTextFromCatalog)
                    {
                        if (a_RecordValue.p_ValuesCatalog != null && a_RecordValue.p_ValuesCatalog.Length > 0)
                        {
                            if ((a_RecordValue.p_Element.p_ElementType == Cl_Element.E_ElementsTypes.Line || a_RecordValue.p_Element.p_ElementType == Cl_Element.E_ElementsTypes.Bigbox) && val[val.Length - 1] != '.')
                            {
                                val += ".";
                            }
                            html = string.Format("<{0} title=\"{1}\">{1}: {2}</{0}>", tag, a_RecordValue.p_Element.p_Name, val);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(a_RecordValue.p_ValueUser))
                        {
                            if ((a_RecordValue.p_Element.p_ElementType == Cl_Element.E_ElementsTypes.Line || a_RecordValue.p_Element.p_ElementType == Cl_Element.E_ElementsTypes.Bigbox) && val[val.Length - 1] != '.')
                            {
                                val += ".";
                            }
                            if (a_RecordValue.p_Element.p_ElementType == Cl_Element.E_ElementsTypes.Bigbox)
                                html = string.Format("<{0} title=\"{1}\"><pre>{1}: {2}</pre></{0}>", tag, a_RecordValue.p_Element.p_Name, val);
                            else
                                html = string.Format("<{0} title=\"{1}\">{1}: {2}</{0}>", tag, a_RecordValue.p_Element.p_Name, val);
                        }
                    }
                }
                else if (a_RecordValue.p_Element.p_IsImage && a_RecordValue.p_ImageBytes != null)
                {
                    html = string.Format("<div><img title=\"{0}\" src=\"data:image/jpeg;base64,{1}\" /></div>", a_RecordValue.p_Element.p_Name, Convert.ToBase64String(a_RecordValue.p_ImageBytes));
                }
            }
            return html;
        }

        /// <summary>Получение записей медкарты из БД</summary>
        public IEnumerable<Cl_Record> f_GetRecords(Cl_MedicalCard a_MedicalCard)
        {
            if (a_MedicalCard != null)
                return f_GetRecords(a_MedicalCard.p_ID);
            else
                return null;
        }

        /// <summary>Получение записей медкарты из БД</summary>
        public IEnumerable<Cl_Record> f_GetRecords(int a_MedicalCardId)
        {
            if (m_DataContextMegaTemplate != null)
            {
                return m_DataContextMegaTemplate.p_Records.Include(r => r.p_MedicalCard).Where(m => m.p_MedicalCard.p_ID == a_MedicalCardId);
            }
            return null;
        }

        /// <summary>Добавление записей в БД</summary>
        public bool f_AddRecords(IEnumerable<Cl_Record> a_Records)
        {
            if (a_Records.Any(r => r.p_MedicalCard != null))
            {
                if (a_Records.Any(r => r.p_MedicalCard.p_IsArchive || r.p_MedicalCard.p_IsDelete))
                {
                    MonitoringStub.Error("Error_AddRecords", "В списке записей имеется не действующая медкарта", null, null, null);
                    return false;
                }
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
                        MonitoringStub.Error("Error_AddRecords", "При сохранении изменений записей произошла ошибка", ex, null, null);
                        return false;
                    }
                }
            }
            else
            {
                MonitoringStub.Error("Error_AddRecords", "При сохранении изменений записей не указана медицинская карта", null, null, null);
                return false;
            }
        }

        private Cl_RecordPatternParam f_GetRecordPatternParam(Cl_RecordPatternValue a_RecordPatternValue, I_RecordParam a_RecordParam)
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

        private Cl_RecordParam f_GetRecordParam(Cl_RecordValue a_RecordValue, I_RecordParam a_RecordPatternParam)
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

        private Cl_RecordPatternValue f_GetRecordPatternValue(Cl_RecordPattern a_RecordPattern, I_RecordValue a_RecordValue)
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
                val.p_Params = a_RecordValue.f_GetParams().Select(p => f_GetRecordPatternParam(val, p)).ToList();

            }
            return val;
        }

        private Cl_RecordValue f_GetRecordValue(Cl_Record a_Record, I_RecordValue a_RecordPatternValue)
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
                val.p_Params = a_RecordPatternValue.f_GetParams().Select(p => f_GetRecordParam(val, p)).ToList();

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
        public Cl_RecordPattern f_GetNewRecordPattern(string a_PatternName, I_Record a_Record)
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
                pattern.p_Values = a_Record.f_GetRecordsValues().Select(v => f_GetRecordPatternValue(pattern, v)).ToList();
            }
            return pattern;
        }

        /// <summary>Получение записи из паттерна записей</summary>
        /// <param name="a_RecordPattern">Паттерн записи</param>
        /// <returns>Паттерн записей</returns>
        public Cl_Record f_GetNewRecord(I_Record a_RecordPattern)
        {
            Cl_Record record = null;
            if (a_RecordPattern != null)
            {
                record = new Cl_Record();
                record.p_DateCreate = DateTime.Now;
                record.p_DateLastChange = record.p_DateCreate;
                record.p_MedicalCard = Cl_SessionFacade.f_GetInstance().p_MedicalCard;
                record.p_MedicalCardID = record.p_MedicalCard.p_ID;
                record.p_ClinicName = Cl_SessionFacade.f_GetInstance().p_Doctor.p_ClinicName;
                record.f_SetDoctor(Cl_SessionFacade.f_GetInstance().p_Doctor);
                record.p_CategoryClinicID = a_RecordPattern.p_CategoryClinicID;
                record.p_CategoryClinic = a_RecordPattern.p_CategoryClinic;
                record.p_CategoryTotalID = a_RecordPattern.p_CategoryTotalID;
                record.p_CategoryTotal = a_RecordPattern.p_CategoryTotal;

                var template = Cl_TemplatesFacade.f_GetInstance().f_GetLastVersionTemplate(a_RecordPattern.p_Template);
                Cl_TemplatesFacade.f_GetInstance().f_LoadTemplatesElements(template);
                var els = Cl_TemplatesFacade.f_GetInstance().f_GetElements(template);
                record.f_SetTemplate(template);
                var values = a_RecordPattern.f_GetRecordsValues().Select(v => f_GetRecordValue(record, v)).ToList();
                values.RemoveAll(val => !els.Any(el => el.p_ID == val.p_ElementID));
                record.p_Values = values;
            }
            return record;
        }

        /// <summary>Изменение записи из паттерна записей</summary>
        /// <param name="a_Record">Запись</param>
        /// <param name="a_RecordPattern">Паттерн записи</param>
        public void f_EditRecordFromPattern(Cl_Record a_Record, I_Record a_RecordPattern)
        {
            if (a_Record == null)
            {
                MonitoringStub.Error("Error_RecordsFacade_EditRecordFromPattern", "Запись пустая", null, null, null);
                return;
            }
            if (a_RecordPattern == null)
            {
                MonitoringStub.Error("Error_RecordsFacade_EditRecordFromPattern", "Паттерн записи пустой", null, null, null);
                return;
            }
            if (a_RecordPattern != null)
            {
                a_Record.p_DateCreate = DateTime.Now;
                a_Record.p_DateLastChange = a_Record.p_DateCreate;
                a_Record.p_MedicalCard = Cl_SessionFacade.f_GetInstance().p_MedicalCard;
                a_Record.p_MedicalCardID = a_Record.p_MedicalCard.p_ID;
                a_Record.p_ClinicName = Cl_SessionFacade.f_GetInstance().p_Doctor.p_ClinicName;
                a_Record.f_SetDoctor(Cl_SessionFacade.f_GetInstance().p_Doctor);
                a_Record.p_CategoryClinicID = a_RecordPattern.p_CategoryClinicID;
                a_Record.p_CategoryClinic = a_RecordPattern.p_CategoryClinic;
                a_Record.p_CategoryTotalID = a_RecordPattern.p_CategoryTotalID;
                a_Record.p_CategoryTotal = a_RecordPattern.p_CategoryTotal;

                var els = Cl_TemplatesFacade.f_GetInstance().f_GetElements(a_Record.p_Template);
                var values = a_RecordPattern.f_GetRecordsValues().Select(v => f_GetRecordValue(a_Record, v)).ToList();
                values.RemoveAll(val => !els.Any(el => el.p_ID == val.p_ElementID));
                var curVals = a_Record.p_Values.Where(val => !values.Any(el => el.p_ID == val.p_ElementID));
                values.AddRange(curVals);
                a_Record.p_Values = values;
            }
        }

        /// <summary>Получение списка паттернов записи</summary>
        /// <param name="a_Record">Запись</param>
        public List<Cl_RecordPattern> f_GetRecordPatterns(Cl_Record a_Record)
        {
            if (m_DataContextMegaTemplate != null)
            {
                if (a_Record != null)
                {
                    return m_DataContextMegaTemplate.p_RecordsPatterns.Include(p => p.p_Template).Include(p => p.p_Values).Include(p => p.p_Values.Select(v => v.p_Params)).Where(p => p.p_Template.p_TemplateID == a_Record.p_Template.p_TemplateID).ToList();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                MonitoringStub.Error("Error_RecordsFacade", "Не инициализирован фасад", null, null, null);
                return null;
            }
        }

        /// <summary>Получение последнего паттернов записи</summary>
        /// <param name="a_Record">Запись</param>
        public Cl_RecordPattern f_GetRecordLastPattern(Cl_Record a_Record)
        {
            if (m_DataContextMegaTemplate != null)
            {
                if (a_Record != null)
                {
                    return m_DataContextMegaTemplate.p_RecordsPatterns.Include(p => p.p_Template).Include(p => p.p_Values).Include(p => p.p_Values.Select(v => v.p_Params)).FirstOrDefault(p => p.p_Template.p_TemplateID == a_Record.p_Template.p_TemplateID);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                MonitoringStub.Error("Error_RecordsFacade", "Не инициализирован фасад", null, null, null);
                return null;
            }
        }

        /// <summary>Полечение новой записи</summary>
        /// <param name="a_MedicalCard">Медкарта</param>
        /// <param name="a_CategoryTotal">Общая категория</param>
        /// <param name="a_CategoryClinic">Клиническая категория</param>
        /// <param name="a_Title">Заголовок записи</param>
        /// <param name="a_ClinicName">Название клиники</param>
        /// <param name="a_DoctorID">ID доктора</param>
        /// <param name="a_DoctorSurName">Фамиля доктора</param>
        /// <param name="a_DoctorName">Имя доктора</param>
        /// <param name="a_DoctorLastName">Отчество доктора</param>
        /// <returns>Флаг успешного создания записи</returns>
        private Cl_Record f_GetNewRecord(Cl_MedicalCard a_MedicalCard, Cl_Category a_CategoryTotal, Cl_Category a_CategoryClinic, string a_Title, string a_ClinicName,
            int a_DoctorID, string a_DoctorSurName, string a_DoctorName, string a_DoctorLastName)
        {
            Cl_Record record = new Cl_Record();
            record.p_Version = 1;
            record.p_DateCreate = record.p_DateLastChange = record.p_DateReception = DateTime.Now;
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
            record.p_MedicalCardID = a_MedicalCard?.p_ID;
            record.p_MedicalCard = a_MedicalCard;
            record.p_DoctorID = a_DoctorID;
            record.p_DoctorSurName = a_DoctorSurName;
            record.p_DoctorName = a_DoctorName;
            record.p_DoctorLastName = a_DoctorLastName;
            record.p_MedicalCard = a_MedicalCard;
            return record;
        }

        private bool f_ValidCreateRecord(Cl_MedicalCard a_MedicalCard, Cl_Category a_CategoryTotal, Cl_Category a_CategoryClinic, string a_Title, string a_ClinicName,
            int a_DoctorID, string a_DoctorSurName, string a_DoctorName, string a_DoctorLastName)
        {
            if (a_MedicalCard == null)
            {
                MonitoringStub.Error("Error_RecordFacade", "Медкарта пустая", null, null, null);
                return false;
            }
            if (a_CategoryTotal == null)
            {
                MonitoringStub.Error("Error_RecordFacade", "Общая категория пустая", null, null, null);
                return false;
            }
            if (a_CategoryTotal == null)
            {
                MonitoringStub.Error("Error_RecordFacade", "Клиническая категория пустая", null, null, null);
                return false;
            }
            if (string.IsNullOrWhiteSpace(a_Title))
            {
                MonitoringStub.Error("Error_RecordFacade", "Заголовок пустой", null, null, null);
                return false;
            }
            if (string.IsNullOrWhiteSpace(a_ClinicName))
            {
                MonitoringStub.Error("Error_RecordFacade", "Имя клиники пустое", null, null, null);
                return false;
            }
            if (string.IsNullOrWhiteSpace(a_DoctorSurName))
            {
                MonitoringStub.Error("Error_RecordFacade", "Фамилия доктора пустое", null, null, null);
                return false;
            }
            if (string.IsNullOrWhiteSpace(a_DoctorName))
            {
                MonitoringStub.Error("Error_RecordFacade", "Имя доктора пустое", null, null, null);
                return false;
            }
            if (string.IsNullOrWhiteSpace(a_DoctorLastName))
            {
                MonitoringStub.Error("Error_RecordFacade", "Отчество доктора пустое", null, null, null);
                return false;
            }
            if (a_MedicalCard.p_IsDelete || a_MedicalCard.p_IsArchive)
            {
                MonitoringStub.Error("Error_AppInit", "Медицинская карта не является действующей", null, null, null);
                return false;
            }
            return true;
        }

        /// <summary>Создание новой записи</summary>
        /// <param name="a_MedicalCard">Медкарта</param>
        /// <param name="a_CategoryTotal">Общая категория</param>
        /// <param name="a_CategoryClinic">Клиническая категория</param>
        /// <param name="a_Title">Заголовок записи</param>
        /// <param name="a_ClinicName">Название клиники</param>
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
        public bool f_CreateRecord(Cl_MedicalCard a_MedicalCard, Cl_Category a_CategoryTotal, Cl_Category a_CategoryClinic, string a_Title, string a_ClinicName,
            int a_DoctorID, string a_DoctorSurName, string a_DoctorName, string a_DoctorLastName,
            Cl_Template a_Template, IEnumerable<Cl_RecordValue> a_Values)
        {
            if (f_ValidCreateRecord(a_MedicalCard, a_CategoryTotal, a_CategoryClinic, a_Title, a_ClinicName, a_DoctorID, a_DoctorSurName, a_DoctorName, a_DoctorLastName))
            {
                if (a_Template == null)
                {
                    MonitoringStub.Error("Error_RecordFacade", "Шаблон пустой", null, null, null);
                    return false;
                }
                Cl_Record record = f_GetNewRecord(a_MedicalCard, a_CategoryTotal, a_CategoryClinic, a_Title, a_ClinicName, a_DoctorID, a_DoctorSurName, a_DoctorName, a_DoctorLastName);
                if (record != null)
                {
                    record.p_TemplateID = a_Template.p_ID;
                    return f_CreateRecord(record, a_Values);
                }
            }
            return false;
        }

        /// <summary>Создание новой записи</summary>
        /// <param name="a_MedicalCard">Медкарта</param>
        /// <param name="a_CategoryTotal">Общая категория</param>
        /// <param name="a_CategoryClinic">Клиническая категория</param>
        /// <param name="a_Title">Заголовок записи</param>
        /// <param name="a_ClinicName">Название клиники</param>
        /// <param name="a_DoctorID">ID доктора</param>
        /// <param name="a_DoctorSurName">Фамиля доктора</param>
        /// <param name="a_DoctorName">Имя доктора</param>
        /// <param name="a_DoctorLastName">Отчество доктора</param>
        /// <param name="a_RecordFileType">Тип файла</param>
        /// <param name="a_FileBytes">Данные файла записи</param>
        /// <returns>Флаг успешного создания записи</returns>
        public bool f_CreateRecord(Cl_MedicalCard a_MedicalCard, Cl_Category a_CategoryTotal, Cl_Category a_CategoryClinic, string a_Title, string a_ClinicName,
            int a_DoctorID, string a_DoctorSurName, string a_DoctorName, string a_DoctorLastName,
            E_RecordFileType a_RecordFileType, byte[] a_FileBytes)
        {
            if (f_ValidCreateRecord(a_MedicalCard, a_CategoryTotal, a_CategoryClinic, a_Title, a_ClinicName, a_DoctorID, a_DoctorSurName, a_DoctorName, a_DoctorLastName))
            {
                Cl_Record record = f_GetNewRecord(a_MedicalCard, a_CategoryTotal, a_CategoryClinic, a_Title, a_ClinicName,
                a_DoctorID, a_DoctorSurName, a_DoctorName, a_DoctorLastName);
                return f_CreateRecord(record, a_RecordFileType, a_FileBytes);
            }
            return false;
        }



        /// <summary>Создание новой записи</summary>
        /// <param name="a_Record">Новая запись</param>
        /// <param name="a_Values">Значения записи</param>
        /// <returns>Флаг успешного создания записи</returns>
        public bool f_CreateRecord(Cl_Record a_Record, IEnumerable<Cl_RecordValue> a_Values)
        {
            if (m_DataContextMegaTemplate == null)
            {
                MonitoringStub.Error("Error_RecordFacade", "Фасад не инициализирован", null, null, null);
                return false;
            }
            if (a_Record == null)
            {
                MonitoringStub.Error("Error_RecordFacade", "Запись пустая", null, null, null);
                return false;
            }
            if (a_Record.p_TemplateID == null)
            {
                MonitoringStub.Error("Error_RecordFacade", "Шаблон записи пустой", null, null, null);
                return false;
            }
            if (!a_Record.f_IsValid())
            {
                MonitoringStub.Error("Error_RecordFacade", "Некорректная запись", null, null, null);
                return false;
            }
            if (a_Values == null && a_Values.Count() == 0)
            {
                MonitoringStub.Error("Error_RecordFacade", "Не указаны значения записи", null, null, null);
                return false;
            }
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
                        a_Record.p_FileType = a_RecordFileType;
                        a_Record.p_Version = 1;
                        a_Record.p_Type = E_RecordType.FinishedFile;
                        a_Record.p_IsAutomatic = true;
                        m_DataContextMegaTemplate.p_Records.Add(a_Record);
                        m_DataContextMegaTemplate.SaveChanges();
                        a_Record.p_RecordID = a_Record.p_ID;
                        m_DataContextMegaTemplate.SaveChanges();
                        a_Record.p_FilePath = f_GetLocalResourcesRelativeFilePath(a_Record);
                        m_DataContextMegaTemplate.SaveChanges();

                        a_Record.p_FilePath = Cl_RecordsFacade.f_GetInstance().f_GetLocalResourcesRelativeFilePath(a_Record);
                        DirectoryInfo DirInfo = new DirectoryInfo(Cl_RecordsFacade.f_GetInstance().f_GetLocalResourcesPath());
                        DirInfo.CreateSubdirectory(Cl_RecordsFacade.f_GetInstance().f_GetLocalResourcesRelativePath(a_Record));
                        File.WriteAllBytes(Cl_RecordsFacade.f_GetInstance().f_GetLocalResourcesPath() + "/" + a_Record.p_FilePath, a_FileBytes);

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
