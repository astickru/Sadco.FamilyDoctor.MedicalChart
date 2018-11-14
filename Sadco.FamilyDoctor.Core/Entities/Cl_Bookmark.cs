using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sadco.FamilyDoctor.Core.Entities
{
    /// <summary>
    /// Класс элемента закладки
    /// </summary>
    public class Cl_Bookmark
    {
        /// <summary>Вид закладки</summary>
        public E_Bookmarks p_Type { get; set; } = E_Bookmarks.Bookmark_1;

        /// <summary>Текст закладки</summary>
        public string p_Text { get; set; }

        public override string ToString()
        {
            if (p_Type == E_Bookmarks.Bookmark_1)
                return "Заголовок 1";
            else if (p_Type == E_Bookmarks.Bookmark_2)
                return "Заголовок 2";
            else if (p_Type == E_Bookmarks.Bookmark_3)
                return "Заголовок 3";
            else if (p_Type == E_Bookmarks.Bookmark_4)
                return "Заголовок 4";
            else if (p_Type == E_Bookmarks.Bookmark_5)
                return "Заголовок 5";
            else if (p_Type == E_Bookmarks.Bookmark_6)
                return "Заголовок 6";
            else
                return "Заголовок 1";
        }
    }
}
