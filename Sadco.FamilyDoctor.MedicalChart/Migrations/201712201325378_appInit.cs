namespace Sadco.FamilyDoctor.MedicalChart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appInit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.T_GROUPS_TEMPLATES",
                c => new
                    {
                        F_ID = c.Int(nullable: false, identity: true),
                        F_NAME = c.String(),
                        F_PARENT_ID = c.Int(),
                    })
                .PrimaryKey(t => t.F_ID)
                .ForeignKey("dbo.T_GROUPS_TEMPLATES", t => t.F_PARENT_ID)
                .Index(t => t.F_PARENT_ID);
            
            CreateTable(
                "dbo.T_TEMPLATES",
                c => new
                    {
                        F_ID = c.Int(nullable: false, identity: true),
                        F_GROUP_ID = c.Int(nullable: false),
                        F_NAME = c.String(),
                        F_DESC = c.String(),
                    })
                .PrimaryKey(t => t.F_ID)
                .ForeignKey("dbo.T_GROUPS_TEMPLATES", t => t.F_GROUP_ID, cascadeDelete: true)
                .Index(t => t.F_GROUP_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.T_TEMPLATES", "F_GROUP_ID", "dbo.T_GROUPS_TEMPLATES");
            DropForeignKey("dbo.T_GROUPS_TEMPLATES", "F_PARENT_ID", "dbo.T_GROUPS_TEMPLATES");
            DropIndex("dbo.T_TEMPLATES", new[] { "F_GROUP_ID" });
            DropIndex("dbo.T_GROUPS_TEMPLATES", new[] { "F_PARENT_ID" });
            DropTable("dbo.T_TEMPLATES");
            DropTable("dbo.T_GROUPS_TEMPLATES");
        }
    }
}
