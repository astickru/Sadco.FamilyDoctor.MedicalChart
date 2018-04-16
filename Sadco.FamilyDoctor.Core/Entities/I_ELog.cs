namespace Sadco.FamilyDoctor.Core.Entities
{
	/// <summary>
	/// Интерфейс ведения логов объекта
	/// </summary>
	public interface I_ELog : I_Version
    {
        /// <summary>Возвращает уникальный ID сущности</summary>
        int p_GetLogEntityID { get; }
    }
}
