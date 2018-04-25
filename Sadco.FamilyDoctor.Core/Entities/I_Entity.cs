using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sadco.FamilyDoctor.Core.Entities
{
    /// <summary>
	/// Интерфейс сущности
	/// </summary>
    public interface I_Entity
    {
        /// <summary>Ключ сущности</summary>
        int p_ID { get; set; }
    }
}
