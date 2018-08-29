namespace Sadco.FamilyDoctor.Core.Entities
{
    /// <summary>Интерфейс параметра паттерна записи</summary>
    public interface I_RecordParam
    {
        /// <summary>ID параметра записи</summary>
        int p_ID { get; set; }

        /// <summary>ID параметра элемента записи</summary>
        int p_ElementParamID { get; set; }
        
        /// <summary>Параметр элемента записи</summary>
        Cl_ElementParam p_ElementParam { get; set; }

        /// <summary>Признак принадлежности к дополнительному параметру</summary>
        bool p_IsDop { get; set; }
    }
}
