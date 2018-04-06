namespace Sadco.FamilyDoctor.Core.Entities
{
	/// <summary>
	/// Интерфейс ведения логов объекта
	/// </summary>
	public interface I_ELog : I_Version
    {
        /// <summary>
        /// Возвращает уникальный ID элемента
        /// </summary>
        int p_GetLogEntityID { get; }
    }
}
