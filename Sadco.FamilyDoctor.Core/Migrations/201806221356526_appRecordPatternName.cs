namespace Sadco.FamilyDoctor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appRecordPatternName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_RECORDS_PATTERNS", "F_NAME", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.T_RECORDS_PATTERNS", "F_NAME");
        }
    }
}
