using FD.dat.mon.stb.lib;
using Sadco.FamilyDoctor.Core.Entities;
using Sadco.FamilyDoctor.Core.EntityLogs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using static Sadco.FamilyDoctor.Core.Entities.Cl_Element;

namespace Sadco.FamilyDoctor.Core.Data
{
    public class Cl_DataContextMegaTemplate : DbContext
    {
        public Cl_DataContextMegaTemplate()
             : base("name=MedicalChartDatabase")
        {
        }
        public Cl_DataContextMegaTemplate(string a_ConnectionPath)
            : base(a_ConnectionPath)
        {
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

        public DbSet<Cl_MedicalCard> p_MedicalCards { get; set; }
        public DbSet<Cl_Record> p_Records { get; set; }
        public DbSet<Cl_RecordValue> p_RecordsValues { get; set; }
        public DbSet<Cl_RecordParam> p_RecordsParams { get; set; }

        public DbSet<Cl_RecordPattern> p_RecordsPatterns { get; set; }
        public DbSet<Cl_RecordPatternValue> p_RecordsPatternsValues { get; set; }
        public DbSet<Cl_RecordPatternParam> p_RecordsPatternsParams { get; set; }

        public void f_Init()
        {
            p_Groups.Load();
            p_Elements.Load();
            var dbChanged = false;
            if (!p_Groups.Any(g => g.p_Type == Cl_Group.E_Type.Templates))
            {
                p_Groups.Add(new Cl_Group() { p_Type = Cl_Group.E_Type.Templates, p_Name = "\\" });
                dbChanged = true;
            }
            if (!p_Groups.Any(g => g.p_Type == Cl_Group.E_Type.Elements))
            {
                p_Groups.Add(new Cl_Group() { p_Type = Cl_Group.E_Type.Elements, p_Name = "\\" });
                dbChanged = true;
            }
            if (!p_Elements.Any(e => e.p_ElementType == E_ElementsTypes.Header))
            {
                using (var transaction = Cl_App.m_DataContext.Database.BeginTransaction())
                {
                    try
                    {
                        var groupHeaders = p_Groups.FirstOrDefault(g => g.p_Type == Cl_Group.E_Type.Elements && g.p_Name == "Заголовки");
                        if (groupHeaders == null)
                            groupHeaders = new Cl_Group() { p_Type = Cl_Group.E_Type.Elements, p_Name = "Заголовки" };
                        var elHeaders = new List<Cl_Element>();
                        for (int i = 1; i <= 6; i++)
                            elHeaders.Add(new Cl_Element() { p_ParentGroupID = groupHeaders.p_ID, p_ParentGroup = groupHeaders, p_Version = 1, p_Name = "Заголовок " + i, p_ElementType = Cl_Element.E_ElementsTypes.Header });
                        p_Elements.AddRange(elHeaders);
                        base.SaveChanges();
                        foreach (var el in elHeaders)
                        {
                            el.p_ElementID = el.p_ID;
                        }
                        base.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MonitoringStub.Error("Error_Editor", "При сохранении изменений произошла ошибка", ex, null, null);
                    }
                }
            }
            if (dbChanged)
                base.SaveChanges();
        }

        /// <summary>Сохранение изменений БД с логированием изменений</summary>
		/// <param name="obj"></param>
		public void f_SaveChanges(Cl_EntityLog a_Log, I_ELog a_Obj)
        {
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Cl_Element>().Property(e => e.p_PartNorm).HasPrecision(10, 6);

            modelBuilder.Entity<Cl_AgeNorm>().Property(n => n.p_MaleMin).HasPrecision(10, 6);
            modelBuilder.Entity<Cl_AgeNorm>().Property(n => n.p_MaleMax).HasPrecision(10, 6);
            modelBuilder.Entity<Cl_AgeNorm>().Property(n => n.p_FemaleMin).HasPrecision(10, 6);
            modelBuilder.Entity<Cl_AgeNorm>().Property(n => n.p_FemaleMax).HasPrecision(10, 6);

            modelBuilder.Entity<Cl_RecordValue>().HasRequired(rv => rv.p_Record).WithMany(r => r.p_Values).WillCascadeOnDelete(false);
            modelBuilder.Entity<Cl_RecordParam>().HasRequired(rv => rv.p_RecordValue).WithMany(r => r.p_Params).WillCascadeOnDelete(false);
            modelBuilder.Entity<Cl_RecordPatternValue>().HasRequired(rv => rv.p_RecordPattern).WithMany(r => r.p_Values).WillCascadeOnDelete(false);
            modelBuilder.Entity<Cl_RecordPatternParam>().HasRequired(rv => rv.p_RecordPatternValue).WithMany(r => r.p_Params).WillCascadeOnDelete(false);

            modelBuilder.Entity<Cl_MedicalCard>().HasIndex(mc => new { mc.p_PatientID, mc.p_Number }).IsUnique();
        }
    }
}
