namespace Sadco.FamilyDoctor.Core.Entities
{
	/// <summary>
	/// Интерфейс ведения логов объекта
	/// </summary>
	public interface I_ELog : I_Version
    {
        /// <summary>
        /// ИД объекта
        /// </summary>
        int p_ID { get; set; }
    }
}
