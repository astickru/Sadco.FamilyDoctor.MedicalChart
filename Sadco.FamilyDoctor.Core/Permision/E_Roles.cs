using System.ComponentModel;

namespace Sadco.FamilyDoctor.Core.Permision
{
    /// <summary>
    /// Роли доступа
    /// </summary>
    public enum E_Roles
	{
		/// <summary>
		/// Главный врач
		/// </summary>
		[Description("Главный врач")]
		[Cl_AccessLevel(E_AccessLevels.EditAllRecords | E_AccessLevels.EditAllRatings)]
		ChiefDoctor,
		/// <summary>
		/// Врач - заведующий отделением
		/// </summary>
		[Description("Врач - заведующий отделением")]
		[Cl_AccessLevel(E_AccessLevels.EditMegaTemplates | E_AccessLevels.EditTemplates | E_AccessLevels.IsShowDeleted)]
		ChiefUnitDoctor,
		/// <summary>
		/// Врач
		/// </summary>
		[Description("Врач")]
		[Cl_AccessLevel(E_AccessLevels.EditAllRecords)]
		Doctor,
		/// <summary>
		/// Ассистент врача
		/// </summary>
		[Description("Ассистент врача")]
		[Cl_AccessLevel(E_AccessLevels.EditAssistantRecords)]
		Assistant,
		/// <summary>
		/// Эксперт
		/// </summary>
		[Description("Эксперт")]
		[Cl_AccessLevel(E_AccessLevels.ReadAllRecords | E_AccessLevels.EditAllRatings | E_AccessLevels.EditMegaTemplates | E_AccessLevels.EditTemplates | E_AccessLevels.EditAllRatings | E_AccessLevels.IsShowDeleted)]
		Expert,
		/// <summary>
		/// Архивариус
		/// </summary>
		[Description("Архивариус")]
		[Cl_AccessLevel(E_AccessLevels.EditAllRecords)]
		Archivarius,
		/// <summary>
		/// Проверяющий С/К
		/// </summary>
		[Description("Проверяющий С/К")]
		[Cl_AccessLevel(E_AccessLevels.ReadSelectedRecords)]
		Inspector,
		/// <summary>
		/// Регистратор
		/// </summary>
		[Description("Регистратор")]
		[Cl_AccessLevel(E_AccessLevels.ReadAllRecords)]
		Registrator,
		/// <summary>
		/// Гость
		/// </summary>
		[Description("Гость")]
		[Cl_AccessLevel(E_AccessLevels.EditSelfRecords)]
		Guest
	}
}
