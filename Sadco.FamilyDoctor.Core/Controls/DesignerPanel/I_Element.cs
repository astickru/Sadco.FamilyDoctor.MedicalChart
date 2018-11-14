using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Sadco.FamilyDoctor.Core.Controls.DesignerPanel
{
    /// <summary>
    /// Интерфейс элемента
    /// </summary>
    public interface I_Element
    {
        /// <summary>ID элемента</summary>
        int p_ID { get; }
        /// <summary>Наименование элемента</summary>
        string p_Name { get; }
        /// <summary>Флаг только чтения</summary>
        bool p_ReadOnly { get; set; }
        /// <summary>Возвращает является ли вкладкой</summary>
        bool f_IsTab();
        /// <summary>Прорисовка контрола</summary>
        void f_Draw(Graphics a_Graphics, Rectangle a_Bounds);
        /// <summary>Прорисовка контрола</summary>
        void f_Draw(Graphics a_Graphics, Rectangle a_Bounds, Font a_Font, Color a_Color);
    }
}
