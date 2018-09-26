namespace Sadco.FamilyDoctor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appRecordMKB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_RECORDS", "F_MKB1", c => c.String(maxLength: 50));
            AddColumn("dbo.T_RECORDS", "F_MKB2", c => c.String(maxLength: 50));
            AddColumn("dbo.T_RECORDS", "F_MKB3", c => c.String(maxLength: 50));
            AddColumn("dbo.T_RECORDS", "F_MKB4", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.T_RECORDS", "F_MKB4");
            DropColumn("dbo.T_RECORDS", "F_MKB3");
            DropColumn("dbo.T_RECORDS", "F_MKB2");
            DropColumn("dbo.T_RECORDS", "F_MKB1");
        }
    }
}
