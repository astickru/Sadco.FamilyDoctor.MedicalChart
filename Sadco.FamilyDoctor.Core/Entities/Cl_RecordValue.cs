using Sadco.FamilyDoctor.Core.EntityLogs;
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

        public bool f_Equals(object a_Value)
        {
            if (a_Value == null || !(a_Value.GetType() == this.GetType()))
                return false;

            Cl_RecordValue m_elm = (Cl_RecordValue)a_Value;
            Cl_Element m_baseElement = this.p_Element;
            bool m_isEqual = true;
            bool m_isEqualPart = true;

            if (m_baseElement.p_IsText)
            {
                if (m_baseElement.p_IsPartLocations)
                {
                    m_isEqualPart = Cl_EntityCompare.f_Array(p_PartLocations, m_elm.p_PartLocations);
                }

                if (m_baseElement.p_IsTextFromCatalog)
                {
                    m_isEqual = Cl_EntityCompare.f_Array(p_ValuesCatalog, m_elm.p_ValuesCatalog);
                    if (m_baseElement.p_Symmetrical && m_isEqual)
                    {
                        m_isEqual = Cl_EntityCompare.f_Array(p_ValuesDopCatalog, m_elm.p_ValuesDopCatalog);
                    }
                }
                else
                {
                    m_isEqual = Cl_EntityCompare.f_String(p_ValueUser, m_elm.p_ValueUser);
                    if (m_baseElement.p_Symmetrical && m_isEqual)
                    {
                        m_isEqual = Cl_EntityCompare.f_String(p_ValueDopUser, m_elm.p_ValueDopUser);
                    }
                }
            }
            else if (m_baseElement.p_IsImage)
            {
                return Cl_EntityCompare.f_Array_Byte(p_ImageBytes, m_elm.p_ImageBytes);
            }
            else
                throw new NotImplementedException("Не реализованный метод сравнения для объекта Cl_RecordValue");

            return m_isEqual && m_isEqualPart;
        }
    }
}
