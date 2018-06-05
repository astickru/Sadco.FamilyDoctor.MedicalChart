namespace Sadco.FamilyDoctor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appRecordTemplateID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.T_RECORDS", "F_TEMPLATE_ID", "dbo.T_TEMPLATES");
            DropIndex("dbo.T_RECORDS", new[] { "F_TEMPLATE_ID" });
            AlterColumn("dbo.T_RECORDS", "F_TEMPLATE_ID", c => c.Int());
            CreateIndex("dbo.T_RECORDS", "F_TEMPLATE_ID");
            AddForeignKey("dbo.T_RECORDS", "F_TEMPLATE_ID", "dbo.T_TEMPLATES", "F_ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.T_RECORDS", "F_TEMPLATE_ID", "dbo.T_TEMPLATES");
            DropIndex("dbo.T_RECORDS", new[] { "F_TEMPLATE_ID" });
            AlterColumn("dbo.T_RECORDS", "F_TEMPLATE_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.T_RECORDS", "F_TEMPLATE_ID");
            AddForeignKey("dbo.T_RECORDS", "F_TEMPLATE_ID", "dbo.T_TEMPLATES", "F_ID", cascadeDelete: true);
        }
    }
}
