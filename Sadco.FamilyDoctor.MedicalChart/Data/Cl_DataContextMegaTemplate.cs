using Sadco.FamilyDoctor.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Sadco.FamilyDoctor.MedicalChart.Data {
	public class Cl_DataContextMegaTemplate : Core.Data.Cl_DataContextMegaTemplate {
		public Cl_DataContextMegaTemplate()
			: base("MedicalChart") {
		}
		public Cl_DataContextMegaTemplate(string a_ConnectionPath)
		: base(a_ConnectionPath) {
		}
	}
}
