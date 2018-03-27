using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sadco.FamilyDoctor.Core.Controls.DesignerPanel
{
    /// <summary>
    /// Интерфейс элемента
    /// </summary>
    public interface I_Element
    {
        /// <summary>Наименование элемента</summary>
        string p_Name { get; }
        /// <summary>Флаг только чтения</summary>
        bool p_ReadOnly { get; set; }
    }
}
