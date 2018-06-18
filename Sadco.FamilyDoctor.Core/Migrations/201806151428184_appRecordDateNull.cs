namespace Sadco.FamilyDoctor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appRecordDateNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.T_RECORDS", "F_DATEPRINTDOCTOR", c => c.DateTime());
            AlterColumn("dbo.T_RECORDS", "F_DATEPRINTPATIENT", c => c.DateTime());
            AlterColumn("dbo.T_RECORDS", "F_DATESYNCBMK", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.T_RECORDS", "F_DATESYNCBMK", c => c.DateTime(nullable: false));
            AlterColumn("dbo.T_RECORDS", "F_DATEPRINTPATIENT", c => c.DateTime(nullable: false));
            AlterColumn("dbo.T_RECORDS", "F_DATEPRINTDOCTOR", c => c.DateTime(nullable: false));
        }
    }
}
