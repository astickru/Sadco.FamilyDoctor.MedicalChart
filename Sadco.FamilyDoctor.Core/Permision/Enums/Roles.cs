using System.ComponentModel;

namespace Sadco.FamilyDoctor.Core.Permision.Enums
{
	public enum Roles
	{
		/// <summary>
		/// Главный врач
		/// </summary>
		[Description("Главный врач")]
		[AccessLevel(AccessLevels.EditAllRecords | AccessLevels.EditAllRatings)]
		ChiefDoctor,
		/// <summary>
		/// Врач - заведующий отделением
		/// </summary>
		[Description("Врач - заведующий отделением")]
		[AccessLevel(AccessLevels.EditMegaTemplates | AccessLevels.EditTemplates)]
		ChiefUnitDoctor,
		/// <summary>
		/// Врач
		/// </summary>
		[Description("Врач")]
		[AccessLevel(AccessLevels.EditAllRecords)]
		Doctor,
		/// <summary>
		/// Ассистент врача
		/// </summary>
		[Description("Ассистент врача")]
		[AccessLevel(AccessLevels.EditAssistantRecords)]
		Assistant,
		/// <summary>
		/// Эксперт
		/// </summary>
		[Description("Эксперт")]
		[AccessLevel(AccessLevels.ReadAllRecords | AccessLevels.EditAllRatings | AccessLevels.EditMegaTemplates | AccessLevels.EditTemplates | AccessLevels.EditAllRatings)]
		Expert,
		/// <summary>
		/// Архивариус
		/// </summary>
		[Description("Архивариус")]
		[AccessLevel(AccessLevels.EditAllRecords)]
		Archivarius,
		/// <summary>
		/// Проверяющий С/К
		/// </summary>
		[Description("Проверяющий С/К")]
		[AccessLevel(AccessLevels.ReadSelectedRecords)]
		Inspector,
		/// <summary>
		/// Регистратор
		/// </summary>
		[Description("Регистратор")]
		[AccessLevel(AccessLevels.ReadAllRecords)]
		Registrator,
		/// <summary>
		/// Гость
		/// </summary>
		[Description("Гость")]
		[AccessLevel(AccessLevels.EditSelfRecords)]
		Guest
	}
}
