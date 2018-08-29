using System;

namespace Sadco.FamilyDoctor.Core.Permision
{
    /// <summary>
    /// Уровни доступа
    /// </summary>
    [Flags]
	public enum E_AccessLevels
	{
		/// <summary>Не имеет доступа</summary>
		None = 0x0,
		/// <summary>Доступ ко всем записям (редактирование) (Основное окно и доступно всем)</summary>
		EditAllRecords = 0x1,
		/// <summary>Доступ к записям в роли ассистента (редактирование) (Основное окно и доступно всем)</summary>
		EditAssistantRecords = 0x2,
		/// <summary>Доступ только к своим записям (редактирование) (Основное окно и доступно всем)</summary>
		EditSelfRecords = 0x4,
		/// <summary>Доступ на чтение всех записей (Основное окно и доступно всем)</summary>
		ReadAllRecords = 0x8,
		/// <summary>Доступ на чтение, только выбранных записей (Основное окно и доступно всем)</summary>
		ReadSelectedRecords = 0x10,
        /// <summary>Доступ архивирования</summary>
        EditArchive = 0x20,
        /// <summary>Доступ к системе оценок (Основное окно и доступно всем)</summary>
        EditAllRatings = 0x40,
		/// <summary>Доступ к редактору шаблона (графическая часть) (Окно шаблонов)</summary>
		EditTemplates = 0x80,
		/// <summary>Доступ к мегашаблону (Окно элементов)</summary>
		EditMegaTemplates = 0x100,
        /// <summary>Доступ к удаленным элементам</summary>
		IsShowDeleted = 0x200,
        /// <summary>Доступ печати</summary>
        IsPrint = 0x400,
        /// <summary>Доступ редактирования справочников</summary>
        IsEditCatalogs = 0x800,
    }
}
