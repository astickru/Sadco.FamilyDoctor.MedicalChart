using Sadco.FamilyDoctor.Core.Entities;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart.Entities.Controls
{
	public interface I_TreeControl
	{
		/// <summary>
		/// Получает имя элемента
		/// </summary>
		string p_TreeName { get; }

		/// <summary>
		/// Получает объект TreeNode
		/// </summary>
		TreeNode p_getTreeNode { get; }

		/// <summary>
		/// 
		/// </summary>
		void f_SetObjectControl(I_Control control);

		/// <summary>
		/// Выполняет добавление нового элемента в указанную колекцию
		/// </summary>
		/// <param name="nodes"></param>
		void f_AddToTreeNode(TreeNodeCollection nodes);
	}
}
