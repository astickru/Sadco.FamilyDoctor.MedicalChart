using System;
using System.Windows.Forms;

namespace Sadco.FamilyDoctor.MedicalChart
{
	static class Program
	{
		/// <summary>
		/// Главная точка входа для приложения.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Application.Run(new F_Main());
		}
	}
}
