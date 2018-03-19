using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sadco.FamilyDoctor.Core.Entities
{
    /// <summary>
    /// Интерфейс сущности архивирования
    /// </summary>
    public interface I_Archive
    {
        /// <summary>Флаг нахождения сущности в архиве</summary>
        bool p_IsArhive { get; set; }
    }
}
