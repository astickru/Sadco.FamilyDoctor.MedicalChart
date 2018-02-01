using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.Core.Entities.Controls;
using System.Data.Entity;
using System.Linq;

namespace Sadco.FamilyDoctor.Core.Data
{
	public class Cl_DataContextMegaTemplate : DbContext
	{
		public Cl_DataContextMegaTemplate()
			: base("MedicalChart") {
		}
		public Cl_DataContextMegaTemplate(string a_ConnectionPath)
		: base(a_ConnectionPath) {
		}

		public DbSet<Cl_GroupsTemplate> p_GroupsTemplate { get; set; }
		public DbSet<Cl_Template> p_Teplates { get; set; }

		public DbSet<Cl_GroupsControl> p_GroupsControl { get; set; }
		public DbSet<Cl_Control> p_BaseControls { get; set; }
		public DbSet<Cl_CtrlComboBox> p_Elms_ComboBox { get; set; }
		public DbSet<Cl_CtrlText> p_Elms_Text { get; set; }

		public void f_Init() {
			p_GroupsTemplate.Load();
			if (!p_GroupsTemplate.Any()) p_GroupsTemplate.Add(new Cl_GroupsTemplate() { p_Name = "Неопределено" });
			if (!p_GroupsControl.Any()) p_GroupsControl.Add(new Cl_GroupsControl() { p_Name = "Неопределено" });

			base.SaveChanges();
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);

		}
	}
}
