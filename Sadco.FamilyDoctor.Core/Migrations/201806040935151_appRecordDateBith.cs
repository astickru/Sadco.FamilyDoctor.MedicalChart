namespace Sadco.FamilyDoctor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appRecordDateBith : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.T_RECORDS", "F_DATEBIRTH");
        }
        
        public override void Down()
        {
            AddColumn("dbo.T_RECORDS", "F_DATEBIRTH", c => c.DateTime(nullable: false, storeType: "date"));
        }
    }
}
