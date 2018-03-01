namespace Sadco.FamilyDoctor.Core.Entities
{
	public interface I_Group
	{
		/// <summary>
		/// Номер группы
		/// </summary>
		int p_ID { get; set; }

		/// <summary>
		/// Название группы
		/// </summary>
		string p_Name { get; set; }

		/// <summary>
		/// Номер родительской группы
		/// </summary>
		int? p_ParentID { get; set; }

		/// <summary>
		/// Возвращает имя родительской группы
		/// </summary>
		string f_GetParentName<T>(T parent) where T : I_Group;

		/// <summary>
		/// Возвращает полный путь родительской группы
		/// </summary>
		string f_GetFullName();
	}
}
