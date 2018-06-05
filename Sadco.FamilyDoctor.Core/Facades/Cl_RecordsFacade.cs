using FD.dat.mon.stb.lib;
using Sadco.FamilyDoctor.Core.Data;
using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.Core.Formula;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
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
    }
}
