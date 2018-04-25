namespace Sadco.FamilyDoctor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appRecordDateChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_RECORDS", "F_DATELASTCHANGE", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.T_RECORDS", "F_DATELASTCHANGE");
        }
    }
}
