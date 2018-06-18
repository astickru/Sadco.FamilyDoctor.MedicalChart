namespace Sadco.FamilyDoctor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appRecordPrint : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_RECORDS", "F_DATEPRINTDOCTOR", c => c.DateTime(nullable: false));
            AddColumn("dbo.T_RECORDS", "F_DATEPRINTPATIENT", c => c.DateTime(nullable: false));
            AddColumn("dbo.T_RECORDS", "F_DATESYNCBMK", c => c.DateTime(nullable: false));
            DropColumn("dbo.T_RECORDS", "F_ISPRINT");
        }
        
        public override void Down()
        {
            AddColumn("dbo.T_RECORDS", "F_ISPRINT", c => c.Boolean(nullable: false));
            DropColumn("dbo.T_RECORDS", "F_DATESYNCBMK");
            DropColumn("dbo.T_RECORDS", "F_DATEPRINTPATIENT");
            DropColumn("dbo.T_RECORDS", "F_DATEPRINTDOCTOR");
        }
    }
}
