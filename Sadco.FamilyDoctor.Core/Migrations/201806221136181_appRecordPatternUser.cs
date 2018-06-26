namespace Sadco.FamilyDoctor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appRecordPatternUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_RECORDS_PATTERNS", "F_CLINIKNAME", c => c.String());
            AddColumn("dbo.T_RECORDS_PATTERNS", "F_USER_ID", c => c.Int(nullable: false));
            AddColumn("dbo.T_RECORDS_PATTERNS", "F_USER_NAME", c => c.String());
            AddColumn("dbo.T_RECORDS_PATTERNS", "F_USER_SURNAME", c => c.String());
            AddColumn("dbo.T_RECORDS_PATTERNS", "F_USER_LASTNAME", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.T_RECORDS_PATTERNS", "F_USER_LASTNAME");
            DropColumn("dbo.T_RECORDS_PATTERNS", "F_USER_SURNAME");
            DropColumn("dbo.T_RECORDS_PATTERNS", "F_USER_NAME");
            DropColumn("dbo.T_RECORDS_PATTERNS", "F_USER_ID");
            DropColumn("dbo.T_RECORDS_PATTERNS", "F_CLINIKNAME");
        }
    }
}
