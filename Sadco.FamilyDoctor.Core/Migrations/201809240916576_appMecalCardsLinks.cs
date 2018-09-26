namespace Sadco.FamilyDoctor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appMecalCardsLinks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_RECORDS", "F_MEDICALCARD_ID", c => c.Int());
            CreateIndex("dbo.T_RECORDS", "F_MEDICALCARD_ID");
            AddForeignKey("dbo.T_RECORDS", "F_MEDICALCARD_ID", "dbo.T_MEDICALCARD", "F_ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.T_RECORDS", "F_MEDICALCARD_ID", "dbo.T_MEDICALCARD");
            DropIndex("dbo.T_RECORDS", new[] { "F_MEDICALCARD_ID" });
            DropColumn("dbo.T_RECORDS", "F_MEDICALCARD_ID");
        }
    }
}
