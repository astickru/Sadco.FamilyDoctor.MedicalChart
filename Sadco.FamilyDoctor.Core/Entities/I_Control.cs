namespace Sadco.FamilyDoctor.Core.Entities
{
	public interface I_Control : I_MenuIcon
	{
		// Относительно пустой интерфейс, нужен для совместимости объектов
		// что бы избежать постоянного приведения типов

		int p_ID { get; set; }
	}
}
