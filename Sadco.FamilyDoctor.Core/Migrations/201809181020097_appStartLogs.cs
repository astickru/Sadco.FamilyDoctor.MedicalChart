namespace Sadco.FamilyDoctor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appStartLogs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_LOGS", "F_SESSION_ID", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.T_LOGS", "F_SESSION_ID");
        }
    }
}
