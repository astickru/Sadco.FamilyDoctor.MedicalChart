using Sadco.FamilyDoctor.Core.EntityLogs;
using Sadco.FamilyDoctor.Core.Facades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Sadco.FamilyDoctor.Core.Entities
{
    /// <summary>
    /// Класс сущности значения записи
    /// </summary>
    [Table("T_RECORDSVALUES")]
    public class Cl_RecordValue : I_Comparable
    {
        /// <summary>ID значения записи</summary>
        [Key]
        [Column("F_ID")]
        public int p_ID { get; set; }

        // <summary>ID записи</summary>
        [Column("F_RECORD_ID")]
        [ForeignKey("p_Record")]
        public int p_RecordID { get; set; }
        /// <summary>Запись</summary>
        public Cl_Record p_Record { get; set; }

        /// <summary>ID элемента</summary>
        [Column("F_ELEMENT_ID")]
        [ForeignKey("p_Element")]
        public int p_ElementID { get; set; }
        /// <summary>Элемент</summary>
        public Cl_Element p_Element { get; set; }

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

        /// <summary>Произвольное значение</summary>
        [Column("F_VALUE")]
        public string p_ValueUser { get; set; }

        /// <summary>Произвольное дополнительное значение</summary>
        [Column("F_VALUEDOP")]
        public string p_ValueDopUser { get; set; }

        /// <summary>Данные рисунка</summary>
        [Column("F_VALUEIMAGE")]
        public byte[] p_ImageBytes { get; set; }
        [NotMapped]
        /// <summary>Рисунок</summary>
        public Image p_Image {
            get {
                if (p_ImageBytes != null && p_ImageBytes.Length > 0)
                {
                    MemoryStream ms = new MemoryStream(p_ImageBytes);
                    return Image.FromStream(ms);
                }
                return null;
            }
            set {
                if (value != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        value.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        p_ImageBytes = ms.ToArray();
                    }
                }
                else
                {
                    p_ImageBytes = null;
                }
            }
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

        /// <summary>Получение HTML текста для клиента</summary>
        public string f_GetHTMLPatient()
        {
            return f_GetHTML(false);
        }

        /// <summary>Получение HTML текста для пользователя</summary>
        public string f_GetHTMLUser()
        {
            return f_GetHTML(true);
        }

        /// <summary>Получение HTML текста запис</summary>
        private string f_GetHTML(bool a_IsUser)
        {
            var html = "";
            if (p_Element.p_Visible && Cl_RecordsFacade.f_GetInstance().f_GetElementVisible(p_Record, p_Element.p_VisibilityFormula) && (a_IsUser || p_Element.p_VisiblePatient))
            {
                if (p_Element.p_IsText)
                {
                    if (p_Element.p_IsTextFromCatalog)
                    {
                        var htmlVals = "";
                        if (p_ValuesCatalog.Length > 0)
                        {
                            htmlVals = string.Format("<div class=\"record_value_name\">{0}</div><div class=\"record_value_val\">", p_Element.p_Name);
                            foreach (var val in p_ValuesCatalog)
                            {
                                if (val.p_ElementParam != null)
                                {
                                    htmlVals += string.Format("<div class=\"record_value_val_item\">{0}</div>", val.p_ElementParam.p_Value);
                                }
                            }
                            htmlVals += "</div>";
                            html = string.Format("<div class=\"record_value\">{0}</div>", htmlVals);
                        }
                    }
                    else
                    {
                        html = string.Format("<div class=\"record_value\"><div class=\"record_value_name\">{0}</div><div class=\"record_value_val\">{1}</div></div>",
                                    p_Element.p_Name, p_ValueUser);
                    }
                }
                else if (p_Element.p_IsImage)
                {
                    html = string.Format("<div class=\"record_value record_img\"><div class=\"record_value_name\">{0}</div><div class=\"record_value_val\">{1}</div></div>",
                                    p_Element.p_Name, p_ImageBytes);
                }
            }
            return html;
        }
    }
}
