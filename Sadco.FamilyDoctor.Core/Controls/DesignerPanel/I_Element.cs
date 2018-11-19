using System.Drawing;

namespace Sadco.FamilyDoctor.Core.Controls.DesignerPanel
{
    /// <summary>
    /// Интерфейс элемента
    /// </summary>
    public interface I_Element
    {
        /// <summary>ID элемента версии</summary>
        int p_ID { get; }
        /// <summary>ID элемента</summary>
        int p_ElementID { get; }
        /// <summary>Наименование элемента</summary>
        string p_Name { get; }
        /// <summary>Флаг только чтения</summary>
        bool p_ReadOnly { get; set; }
        /// <summary>Возвращает является ли вкладкой</summary>
        bool f_IsTab();
        /// <summary>Возвращает является ли заголовком</summary>
        bool f_IsHeader();
        /// <summary>Возвращает уровень заголовка</summary>
        int f_GetHeaderLevel();
        /// <summary>Прорисовка контрола</summary>
        void f_Draw(Graphics a_Graphics, Rectangle a_Bounds);
        /// <summary>Прорисовка контрола</summary>
        void f_Draw(Graphics a_Graphics, Rectangle a_Bounds, Font a_Font, Color a_Color);
    }
}
