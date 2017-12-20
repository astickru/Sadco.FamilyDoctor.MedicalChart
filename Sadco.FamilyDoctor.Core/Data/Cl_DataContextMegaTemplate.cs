using Sadco.FamilyDoctor.Core.Entities;
using System.Data.Entity;
using System.Linq;

namespace Sadco.FamilyDoctor.Core.Data {
	public class Cl_DataContextMegaTemplate : DbContext {
		public Cl_DataContextMegaTemplate()
			: base("MedicalChart") {
		}
		public Cl_DataContextMegaTemplate(string a_ConnectionPath)
		: base(a_ConnectionPath) {
		}

		public DbSet<Cl_GroupTemplates> p_GroupsTepmlates { get; set; }
		public DbSet<Cl_Template> p_Teplates { get; set; }

		public void f_Init() {
			p_GroupsTepmlates.Load();
			if (!p_GroupsTepmlates.Any()) {
				p_GroupsTepmlates.Add(new Cl_GroupTemplates() { p_Name = "Неопределено" });
			}
			SaveChanges();
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);

		}
	}
}
