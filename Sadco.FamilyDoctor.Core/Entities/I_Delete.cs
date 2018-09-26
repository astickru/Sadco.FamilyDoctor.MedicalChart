using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sadco.FamilyDoctor.Core.Entities
{
    /// <summary>
    /// Интерфейс сущности архивирования
    /// </summary>
    public interface I_Delete
    {
        /// <summary>Флаг нахождения сущности в удалении</summary>
        bool p_IsDelete { get; }
    }
}
