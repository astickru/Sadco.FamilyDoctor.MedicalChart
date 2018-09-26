namespace Sadco.FamilyDoctor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appMecalCardsDel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.T_MEDICALCARD", "F_ISARCHIVE");
            DropColumn("dbo.T_MEDICALCARD", "F_ISDEL");
        }
        
        public override void Down()
        {
            AddColumn("dbo.T_MEDICALCARD", "F_ISDEL", c => c.Boolean(nullable: false));
            AddColumn("dbo.T_MEDICALCARD", "F_ISARCHIVE", c => c.Boolean(nullable: false));
        }
    }
}
