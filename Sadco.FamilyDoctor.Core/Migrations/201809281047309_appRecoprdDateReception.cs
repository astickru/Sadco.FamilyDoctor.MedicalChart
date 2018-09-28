namespace Sadco.FamilyDoctor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appRecoprdDateReception : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_RECORDS", "F_DATERECEPTION", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.T_RECORDS", "F_DATERECEPTION");
        }
    }
}
