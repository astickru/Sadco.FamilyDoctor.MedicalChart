using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Sadco.FamilyDoctor.Core.Entities
{
    /// <summary>Интерфейс значения записи</summary>
    public interface I_RecordValue
    {
        /// <summary>ID значения записи</summary>
        int p_ID { get; set; }

        /// <summary>ID элемента</summary>
        int p_ElementID { get; set; }
        
        /// <summary>Элемент</summary>
        Cl_Element p_Element { get; set; }

        /// <summary>Произвольное значение</summary>
        string p_ValueUser { get; set; }

        /// <summary>Произвольное дополнительное значение</summary>
        string p_ValueDopUser { get; set; }

        /// <summary>Данные рисунка</summary>
        byte[] p_ImageBytes { get; set; }

        /// <summary>Рисунок</summary>
        Image p_Image { get; set; }

        /// <summary>Получение записи</summary>
        I_Record f_GetRecord();

        /// <summary>Возврат списка параметров элементов</summary>
        IEnumerable<I_RecordParam> f_GetParams();

        /// <summary>Локации</summary>
        I_RecordParam[] p_PartLocations { get; }

        /// <summary>Значения из справочника</summary>
        I_RecordParam[] p_ValuesCatalog { get; }

        /// <summary>Дополнительные значения из справочника</summary>
        I_RecordParam[] p_ValuesDopCatalog { get; }

        /// <summary>Сравнение значений</summary>
        bool f_Equals(object a_Value);
    }
}
