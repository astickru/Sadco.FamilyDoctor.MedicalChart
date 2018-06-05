namespace Sadco.FamilyDoctor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appReportMedicCardID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_RECORDS", "F_PATIENT_DATEBIRTH", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.T_RECORDS", "F_PATIENT_DATEBIRTH");
        }
    }
}
