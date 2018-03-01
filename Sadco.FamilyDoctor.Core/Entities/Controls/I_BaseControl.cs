namespace Sadco.FamilyDoctor.Core.Entities.Controls
{
	public interface I_BaseControl : I_Control
	{
		/// <summary>Объект базового контрола</summary>
		Cl_Control p_BaseControl { get; set; }
	}
}
