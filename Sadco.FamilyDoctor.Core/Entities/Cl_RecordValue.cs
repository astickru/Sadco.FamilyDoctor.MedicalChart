using Sadco.FamilyDoctor.Core.EntityLogs;
using Sadco.FamilyDoctor.Core.Facades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Sadco.FamilyDoctor.Core.Entities
{
    /// <summary>
    /// Класс сущности значения записи
    /// </summary>
    [Table("T_RECORDSVALUES")]
    public class Cl_RecordValue : Cl_RecordValueBase, I_Comparable
    {
        // <summary>ID записи</summary>
        [Column("F_RECORD_ID")]
        [ForeignKey("p_Record")]
        public int p_RecordID { get; set; }
        /// <summary>Запись</summary>
        public Cl_Record p_Record { get; set; }

        private List<Cl_RecordParam> m_Params = new List<Cl_RecordParam>();
        /// <summary>Список параметров элементов</summary>
        [ForeignKey("p_ElementID")]
        public List<Cl_RecordParam> p_Params {
            get { return m_Params; }
            set { m_Params = value; }
        }

        /// <summary>Локации</summary>
        public Cl_RecordParam[] p_PartLocations {
            get { return p_Params.Where(p => p.p_ElementParam.p_TypeParam == Cl_ElementParam.E_TypeParam.Location).ToArray(); }
        }

        /// <summary>Значения из справочника</summary>
        public Cl_RecordParam[] p_ValuesCatalog {
            get { return p_Params.Where(p => !p.p_IsDop && (p.p_ElementParam.p_TypeParam == Cl_ElementParam.E_TypeParam.NormValues || p.p_ElementParam.p_TypeParam == Cl_ElementParam.E_TypeParam.PatValues)).ToArray(); }
        }

        /// <summary>Дополнительные значения из справочника</summary>
        public Cl_RecordParam[] p_ValuesDopCatalog {
            get { return p_Params.Where(p => p.p_IsDop && (p.p_ElementParam.p_TypeParam == Cl_ElementParam.E_TypeParam.NormValues || p.p_ElementParam.p_TypeParam == Cl_ElementParam.E_TypeParam.PatValues)).ToArray(); }
        }

        /// <summary>Сравнение значений</summary>
        public bool f_Equals(object a_Value)
        {
            if (a_Value == null || !(a_Value.GetType() == this.GetType()))
                return false;

            Cl_RecordValue elm = (Cl_RecordValue)a_Value;
            Cl_Element baseElement = this.p_Element;
            bool m_isEqual = true;
            bool m_isEqualPart = true;

            if (baseElement.p_IsText)
            {
                if (baseElement.p_IsPartLocations)
                {
                    m_isEqualPart = Cl_EntityCompare.f_Array(p_PartLocations, elm.p_PartLocations);
                }

                if (baseElement.p_IsTextFromCatalog)
                {
                    m_isEqual = Cl_EntityCompare.f_Array(p_ValuesCatalog, elm.p_ValuesCatalog);
                    if (baseElement.p_Symmetrical && m_isEqual)
                    {
                        m_isEqual = Cl_EntityCompare.f_Array(p_ValuesDopCatalog, elm.p_ValuesDopCatalog);
                    }
                }
                else
                {
                    m_isEqual = Cl_EntityCompare.f_String(p_ValueUser, elm.p_ValueUser);
                    if (baseElement.p_Symmetrical && m_isEqual)
                    {
                        m_isEqual = Cl_EntityCompare.f_String(p_ValueDopUser, elm.p_ValueDopUser);
                    }
                }
            }
            else if (baseElement.p_IsImage)
            {
                return Cl_EntityCompare.f_Array_Byte(p_ImageBytes, elm.p_ImageBytes);
            }
            else
                throw new NotImplementedException("Не реализованный метод сравнения для объекта Cl_RecordValue");

            return m_isEqual && m_isEqualPart;
        }

        private string m_SeparatorMulti = ", ";

        /// <summary>Получение элемента шаблона</summary>
        private Cl_TemplateElement f_GetTemplateElement(Cl_Template a_Template)
        {
            if (a_Template != null && a_Template.p_TemplateElements != null)
            {
                foreach (var te in a_Template.p_TemplateElements)
                {
                    if (te.p_ChildElement == p_Element)
                    {
                        return te;
                    }
                    else
                    {
                        var t = f_GetTemplateElement(te.p_ChildTemplate);
                        if (t != null)
                            return t;
                    }
                }
            }
            return null;
        }

        /// <summary>Получение элемента шаблона</summary>
        public Cl_TemplateElement f_GetTemplateElement()
        {
            if (p_Record != null)
            {
                return f_GetTemplateElement(p_Record.p_Template);
            }
            return null;
        }

        /// <summary>Получение HTML текста для клиента</summary>
        public string f_GetHTMLPatient(Cl_Record a_Record, bool a_IsTable, decimal? a_Min, decimal? a_Max)
        {
            return f_GetHTML(false, a_Record, a_IsTable, a_Min, a_Max);
        }

        /// <summary>Получение HTML текста для пользователя</summary>
        public string f_GetHTMLDoctor(Cl_Record a_Record, bool a_IsTable, decimal? a_Min, decimal? a_Max)
        {
            return f_GetHTML(true, a_Record, a_IsTable, a_Min, a_Max);
        }

        private string f_GetValWithColorForHtml(IEnumerable<string> a_Vals, decimal? a_Min, decimal? a_Max)
        {
            if (p_Element.p_IsNumber)
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

        private string f_GetValForHTML(decimal? a_Min, decimal? a_Max)
        {
            string val = "";
            if (p_Element.p_IsTextFromCatalog)
            {
                if (p_ValuesCatalog != null && p_ValuesCatalog.Length > 0)
                {
                    var vals = p_ValuesCatalog.Select(vc => vc.p_ElementParam.p_Value);
                    val = f_GetValWithColorForHtml(vals, a_Min, a_Max);
                    if (p_ValuesDopCatalog != null && p_ValuesDopCatalog.Length > 0)
                    {
                        var valsDop = p_ValuesDopCatalog.Select(vc => vc.p_ElementParam.p_Value);
                        string valDop = f_GetValWithColorForHtml(valsDop, a_Min, a_Max);
                        val = string.Format("{0}: {1}, {2}: {3}", p_Element.p_SymmetryParamLeft, val, p_Element.p_SymmetryParamRight, valDop);
                    }
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(p_ValueUser))
                    val = f_GetValWithColorForHtml(new string[] { p_ValueUser }, a_Min, a_Max);
            }
            return val;
        }

        /// <summary>Получение HTML текста запис</summary>
        private string f_GetHTML(bool a_IsDoctor, Cl_Record a_Record, bool a_IsTable, decimal? a_Min, decimal? a_Max)
        {
            var html = "";
            if (p_Element.p_Visible && Cl_RecordsFacade.f_GetInstance().f_GetElementVisible(p_Record, p_Element.p_VisibilityFormula) && (a_IsDoctor || p_Element.p_VisiblePatient))
            {
                if (a_IsTable)
                {
                    string val = f_GetValForHTML(a_Min, a_Max);
                    var partNorm = p_Element.f_GetPartNormValue(a_Record.p_PatientSex, a_Record.f_GetPatientAge());
                    html = string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>", p_Element.p_PartPre, val, p_Element.p_PartPost, partNorm);
                }
                else if (p_Element.p_IsText)
                {
                    string tag = "span";
                    if (p_Element.p_ElementType == Cl_Element.E_ElementsTypes.Line || p_Element.p_ElementType == Cl_Element.E_ElementsTypes.Bigbox)
                        tag = "p";
                    string val = f_GetValForHTML(a_Min, a_Max);
                    if (p_Element.p_IsTextFromCatalog)
                    {
                        if (p_ValuesCatalog != null && p_ValuesCatalog.Length > 0)
                        {
                            if ((p_Element.p_ElementType == Cl_Element.E_ElementsTypes.Line || p_Element.p_ElementType == Cl_Element.E_ElementsTypes.Bigbox) && val[val.Length - 1] != '.')
                            {
                                val += ".";
                            }
                            html = string.Format("<{0} title=\"{1}\">{2}</{0}>", tag, p_Element.p_Name, val);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(p_ValueUser))
                        {
                            if ((p_Element.p_ElementType == Cl_Element.E_ElementsTypes.Line || p_Element.p_ElementType == Cl_Element.E_ElementsTypes.Bigbox) && val[val.Length - 1] != '.')
                            {
                                val += ".";
                            }
                            if (p_Element.p_ElementType == Cl_Element.E_ElementsTypes.Bigbox)
                                html = string.Format("<{0} title=\"{1}\"><pre>{2}</pre></{0}>", tag, p_Element.p_Name, val);
                            else
                                html = string.Format("<{0} title=\"{1}\">{2}</{0}>", tag, p_Element.p_Name, val);
                        }
                    }
                }
                else if (p_Element.p_IsImage && p_ImageBytes != null)
                {
                    html = string.Format("<div><img title=\"{0}\" src=\"data:image/jpeg;base64,{1}\" /></div>", p_Element.p_Name, Convert.ToBase64String(p_ImageBytes));
                }
            }
            return html;
        }
    }
}
