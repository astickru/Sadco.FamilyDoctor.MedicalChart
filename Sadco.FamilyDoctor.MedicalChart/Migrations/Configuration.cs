namespace Sadco.FamilyDoctor.MedicalChart.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Sadco.FamilyDoctor.MedicalChart.Data.Cl_DataContextMegaTemplate>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Sadco.FamilyDoctor.MedicalChart.Data.Cl_DataContextMegaTemplate context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
