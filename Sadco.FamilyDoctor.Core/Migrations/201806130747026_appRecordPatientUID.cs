namespace Sadco.FamilyDoctor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appRecordPatientUID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_RECORDS", "F_PATIENT_UID", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.T_RECORDS", "F_PATIENT_UID");
        }
    }
}
