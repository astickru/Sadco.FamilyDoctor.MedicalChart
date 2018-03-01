using Sadco.FamilyDoctor.MedicalChart.Data;
using System;
using System.Configuration;
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

			Cl_App.m_DataContext = new Cl_DataContextMegaTemplate(ConfigurationManager.ConnectionStrings["MedicalChart"].ConnectionString);
			Cl_App.m_DataContext.f_Init();


			Application.Run(new F_Main());
		}
	}
}
