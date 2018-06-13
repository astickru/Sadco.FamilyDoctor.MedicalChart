using FD.dat.mon.stb.lib;
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
        public DbSet<Cl_Rating> p_Ratings { get; set; }

        public DbSet<Cl_Group> p_Groups { get; set; }
		public DbSet<Cl_Template> p_Templates { get; set; }
		public DbSet<Cl_TemplateElement> p_TemplatesElements { get; set; }

		public DbSet<Cl_Element> p_Elements { get; set; }
		public DbSet<Cl_ElementParam> p_ElementsParams { get; set; }
		public DbSet<Cl_AgeNorm> p_AgeNorms { get; set; }

        public DbSet<Cl_Category> p_Categories { get; set; }

        public DbSet<Cl_Record> p_Records { get; set; }
        public DbSet<Cl_RecordValue> p_RecordsValues { get; set; }
        public DbSet<Cl_RecordParam> p_RecordsParams { get; set; }

        public void f_Init() {
			p_Groups.Load();
			if (!p_Groups.Any(g => g.p_Type == Cl_Group.E_Type.Templates)) p_Groups.Add(new Cl_Group() { p_Type = Cl_Group.E_Type.Templates, p_Name = "Главная" });
			if (!p_Groups.Any(g => g.p_Type == Cl_Group.E_Type.Elements)) p_Groups.Add(new Cl_Group() { p_Type = Cl_Group.E_Type.Elements, p_Name = "Главная" });
            if (!p_Categories.Any())
            {
                p_Categories.AddRange(new Cl_Category[] { new Cl_Category() { p_Type = Cl_Category.E_CategoriesTypes.Total, p_Name = "Анализ" },
                    new Cl_Category() { p_Type = Cl_Category.E_CategoriesTypes.Total, p_Name = "Осмотр" },
                    new Cl_Category() { p_Type = Cl_Category.E_CategoriesTypes.Total, p_Name = "Ренген" } });
                p_Categories.AddRange(new Cl_Category[] { new Cl_Category() { p_Type = Cl_Category.E_CategoriesTypes.Clinik, p_Name = "Клиническая 1" },
                    new Cl_Category() { p_Type = Cl_Category.E_CategoriesTypes.Clinik, p_Name = "Клиническая 2" } });
            }
            base.SaveChanges();
		}

        /// <summary>Сохранение изменений БД с логированием изменений</summary>
		/// <param name="obj"></param>
		public void f_SaveChanges(Cl_EntityLog a_Log, I_ELog a_Obj) {
            using (var transaction = Database.BeginTransaction())
            {
                try
                {
                    SaveChanges();
                    a_Log.f_SaveEntity(a_Obj);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MonitoringStub.Error("Error_Tree", "Не удалось сохранить изменения в базе данных", ex, null, null);
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

            modelBuilder.Entity<Cl_RecordValue>().HasRequired(rv => rv.p_Record).WithMany(r => r.p_Values).WillCascadeOnDelete(false);
            modelBuilder.Entity<Cl_RecordParam>().HasRequired(rv => rv.p_RecordValue).WithMany(r => r.p_Params).WillCascadeOnDelete(false);
        }
	}
}
