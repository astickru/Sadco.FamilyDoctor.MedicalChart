namespace Sadco.FamilyDoctor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appTemplCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.T_CATEGORIES",
                c => new
                    {
                        F_ID = c.Int(nullable: false, identity: true),
                        F_NAME = c.String(maxLength: 100, unicode: false),
                        F_Type = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.F_ID);
            
            AddColumn("dbo.T_TEMPLATES", "F_CATEGORYTOTAL_ID", c => c.Int());
            AddColumn("dbo.T_TEMPLATES", "F_CATEGORYKLINIK_ID", c => c.Int());
            CreateIndex("dbo.T_TEMPLATES", "F_CATEGORYTOTAL_ID");
            CreateIndex("dbo.T_TEMPLATES", "F_CATEGORYKLINIK_ID");
            AddForeignKey("dbo.T_TEMPLATES", "F_CATEGORYKLINIK_ID", "dbo.T_CATEGORIES", "F_ID");
            AddForeignKey("dbo.T_TEMPLATES", "F_CATEGORYTOTAL_ID", "dbo.T_CATEGORIES", "F_ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.T_TEMPLATES", "F_CATEGORYTOTAL_ID", "dbo.T_CATEGORIES");
            DropForeignKey("dbo.T_TEMPLATES", "F_CATEGORYKLINIK_ID", "dbo.T_CATEGORIES");
            DropIndex("dbo.T_TEMPLATES", new[] { "F_CATEGORYKLINIK_ID" });
            DropIndex("dbo.T_TEMPLATES", new[] { "F_CATEGORYTOTAL_ID" });
            DropColumn("dbo.T_TEMPLATES", "F_CATEGORYKLINIK_ID");
            DropColumn("dbo.T_TEMPLATES", "F_CATEGORYTOTAL_ID");
            DropTable("dbo.T_CATEGORIES");
        }
    }
}
