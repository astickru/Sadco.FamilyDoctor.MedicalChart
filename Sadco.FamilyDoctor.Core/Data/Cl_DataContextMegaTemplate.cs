using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.Core.EntityLogs;
using System;
using System.Collections.Generic;
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

		public DbSet<Cl_Log> p_Logs { get; set; }

		public DbSet<Cl_Group> p_Groups { get; set; }
		public DbSet<Cl_Template> p_Templates { get; set; }
		public DbSet<Cl_TemplateElement> p_TemplatesElements { get; set; }

		public DbSet<Cl_Element> p_Elements { get; set; }
		public DbSet<Cl_ElementsParams> p_ElementsParams { get; set; }
		public DbSet<Cl_AgeNorm> p_AgeNorms { get; set; }

		public void f_Init() {
			p_Groups.Load();
			if (!p_Groups.Any(g => g.p_Type == Cl_Group.E_Type.Templates)) p_Groups.Add(new Cl_Group() { p_Type = Cl_Group.E_Type.Templates, p_Name = "Root" });
			if (!p_Groups.Any(g => g.p_Type == Cl_Group.E_Type.Elements)) p_Groups.Add(new Cl_Group() { p_Type = Cl_Group.E_Type.Elements, p_Name = "Root" });
			base.SaveChanges();
		}

        /// <summary>Сохранение изменений БД с логированием изменений</summary>
		/// <param name="obj"></param>
		public void f_SaveChanges(EntityLog a_Log, I_ELog a_Obj) {
            using (var transaction = Database.BeginTransaction())
            {
                try
                {
                    SaveChanges();
                    a_Log.SaveEntity(a_Obj);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            
        }

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Cl_Element>().Property(e => e.p_PartNorm).HasPrecision(10, 6);

			modelBuilder.Entity<Cl_AgeNorm>().Property(n => n.p_MaleMin).HasPrecision(10, 6);
			modelBuilder.Entity<Cl_AgeNorm>().Property(n => n.p_MaleMax).HasPrecision(10, 6);
			modelBuilder.Entity<Cl_AgeNorm>().Property(n => n.p_FemaleMin).HasPrecision(10, 6);
			modelBuilder.Entity<Cl_AgeNorm>().Property(n => n.p_FemaleMax).HasPrecision(10, 6);
		}
	}
}
