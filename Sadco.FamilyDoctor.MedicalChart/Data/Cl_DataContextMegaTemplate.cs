using Sadco.FamilyDoctor.Core.Entities.Controls;
using System.Data.Entity;

namespace Sadco.FamilyDoctor.MedicalChart.Data
{
	public class Cl_DataContextMegaTemplate : Core.Data.Cl_DataContextMegaTemplate
	{
		public Cl_DataContextMegaTemplate()
			: base("MedicalChart") {
		}
		public Cl_DataContextMegaTemplate(string a_ConnectionPath)
		: base(a_ConnectionPath) {
		}
	}
}
