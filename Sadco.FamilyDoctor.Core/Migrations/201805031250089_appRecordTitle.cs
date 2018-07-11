namespace Sadco.FamilyDoctor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appRecordTitle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_RECORDS", "F_KLINIKNAME", c => c.String());
            AddColumn("dbo.T_RECORDS", "F_TITLE", c => c.String());
            AddColumn("dbo.T_RECORDS", "F_CATEGORYTOTAL_ID", c => c.Int());
            AddColumn("dbo.T_RECORDS", "F_CATEGORYKLINIK_ID", c => c.Int());
            CreateIndex("dbo.T_RECORDS", "F_CATEGORYTOTAL_ID");
            CreateIndex("dbo.T_RECORDS", "F_CATEGORYKLINIK_ID");
            AddForeignKey("dbo.T_RECORDS", "F_CATEGORYKLINIK_ID", "dbo.T_CATEGORIES", "F_ID");
            AddForeignKey("dbo.T_RECORDS", "F_CATEGORYTOTAL_ID", "dbo.T_CATEGORIES", "F_ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.T_RECORDS", "F_CATEGORYTOTAL_ID", "dbo.T_CATEGORIES");
            DropForeignKey("dbo.T_RECORDS", "F_CATEGORYKLINIK_ID", "dbo.T_CATEGORIES");
            DropIndex("dbo.T_RECORDS", new[] { "F_CATEGORYKLINIK_ID" });
            DropIndex("dbo.T_RECORDS", new[] { "F_CATEGORYTOTAL_ID" });
            DropColumn("dbo.T_RECORDS", "F_CATEGORYKLINIK_ID");
            DropColumn("dbo.T_RECORDS", "F_CATEGORYTOTAL_ID");
            DropColumn("dbo.T_RECORDS", "F_TITLE");
            DropColumn("dbo.T_RECORDS", "F_CLINIKNAME");
        }
    }
}
