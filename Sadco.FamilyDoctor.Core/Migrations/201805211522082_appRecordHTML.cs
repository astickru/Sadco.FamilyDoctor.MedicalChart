namespace Sadco.FamilyDoctor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appRecordHTML : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_RECORDS", "F_HTMLPATIENT", c => c.String());
            AddColumn("dbo.T_RECORDS", "F_HTMLUSER", c => c.String());
            AddColumn("dbo.T_RECORDS", "F_FILE", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.T_RECORDS", "F_FILE");
            DropColumn("dbo.T_RECORDS", "F_HTMLUSER");
            DropColumn("dbo.T_RECORDS", "F_HTMLPATIENT");
        }
    }
}
