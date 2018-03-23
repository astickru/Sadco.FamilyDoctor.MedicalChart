using Sadco.FamilyDoctor.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Sadco.FamilyDoctor.Core.Data
{
    public class Cl_DataContextMegaTemplate : DbContext
    {
        public Cl_DataContextMegaTemplate()
            : base("MedicalChart")
        {
        }
        public Cl_DataContextMegaTemplate(string a_ConnectionPath)
        : base(a_ConnectionPath)
        {
        }

        public DbSet<Cl_Group> p_Groups { get; set; }
        public DbSet<Cl_Template> p_Templates { get; set; }
        public DbSet<Cl_TemplatesElements> p_TemplatesElements { get; set; }

        public DbSet<Cl_Element> p_Elements { get; set; }
        public DbSet<Cl_ElementsParams> p_ElementsParams { get; set; }
        public DbSet<Cl_AgeNorm> p_AgeNorms { get; set; }

        private Dictionary<string, Type> m_GetAvailableControls { get; set; }
        public Dictionary<string, Type> f_GetAvailableControls()
        {
            return new Dictionary<string, Type>(m_GetAvailableControls);
        }

        public void f_Init()
        {
            p_Groups.Load();
            if (!p_Groups.Any(g => g.p_Type == Cl_Group.E_Type.Templates)) p_Groups.Add(new Cl_Group() { p_Type = Cl_Group.E_Type.Templates, p_Name = "Root" });
            if (!p_Groups.Any(g => g.p_Type == Cl_Group.E_Type.Elements)) p_Groups.Add(new Cl_Group() { p_Type = Cl_Group.E_Type.Elements, p_Name = "Root" });

            m_GetAvailableControls = new Dictionary<string, Type>();
            Type type = typeof(Cl_Element);
            IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies()
                 .SelectMany(s => s.GetTypes())
                 .Where(p => type.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract && p.GetCustomAttributes(false).Length > 0);

            foreach (Type item in types)
            {
                m_GetAvailableControls.Add(item.Name, item);
            }

            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Cl_Element>().Property(e => e.p_PartNorm).HasPrecision(10, 6);

            modelBuilder.Entity<Cl_AgeNorm>().Property(n => n.p_MaleMin).HasPrecision(10, 6);
            modelBuilder.Entity<Cl_AgeNorm>().Property(n => n.p_MaleMax).HasPrecision(10, 6);
            modelBuilder.Entity<Cl_AgeNorm>().Property(n => n.p_FemaleMin).HasPrecision(10, 6);
            modelBuilder.Entity<Cl_AgeNorm>().Property(n => n.p_FemaleMax).HasPrecision(10, 6);
        }
    }
}
