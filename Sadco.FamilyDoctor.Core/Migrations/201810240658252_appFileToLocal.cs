namespace Sadco.FamilyDoctor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appFileToLocal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_RECORDS", "F_PATHFILE", c => c.String());
            DropColumn("dbo.T_RECORDS", "F_DATEFORMING");
            DropColumn("dbo.T_RECORDS", "F_FILE");
        }
        
        public override void Down()
        {
            AddColumn("dbo.T_RECORDS", "F_FILE", c => c.Binary());
            AddColumn("dbo.T_RECORDS", "F_DATEFORMING", c => c.DateTime(nullable: false));
            DropColumn("dbo.T_RECORDS", "F_PATHFILE");
        }
    }
}
