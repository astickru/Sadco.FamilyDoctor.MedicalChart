using System.Drawing;

namespace Sadco.FamilyDoctor.Core.Entities
{
	public interface I_MenuIcon
	{
		/// <summary>
		/// Получает название иконки используемую в меню
		/// </summary>
		string p_IconName { get; }

		/// <summary>
		/// Возвращает иконку используемую в меню
		/// </summary>
		Bitmap p_Icon { get; }
	}
}
