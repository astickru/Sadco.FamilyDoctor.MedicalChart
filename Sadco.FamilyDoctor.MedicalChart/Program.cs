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
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			if (args.Length > 0)
				Application.Run(new F_Main(args));
			else
				Application.Run(new F_Welcome());
		}
	}
}
