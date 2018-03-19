using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sadco.FamilyDoctor.Core.Entities
{
    /// <summary>
    /// Интерфейс сущности учета версий
    /// </summary>
    public interface I_Version
    {
        /// <summary>Версия сущности</summary>
        int p_Version { get; set; }
    }
}
