using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sadco.FamilyDoctor.Core.Entities
{
    /// <summary>
    /// Интерфейс сравнения
    /// </summary>
    public interface I_Comparable
    {
        /// <summary>Метод сравнения</summary>
        bool f_Equals(object a_Value);
    }
}
