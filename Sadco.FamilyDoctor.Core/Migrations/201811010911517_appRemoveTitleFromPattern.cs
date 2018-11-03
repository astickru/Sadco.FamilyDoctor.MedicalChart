namespace Sadco.FamilyDoctor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appRemoveTitleFromPattern : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.T_RECORDS_PATTERNS", "F_TITLE");
        }
        
        public override void Down()
        {
            AddColumn("dbo.T_RECORDS_PATTERNS", "F_TITLE", c => c.String());
        }
    }
}
