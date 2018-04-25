namespace Sadco.FamilyDoctor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appRecordLocations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.T_RECORDSVALUES", "F_LOCATION_ID", "dbo.T_ELEMENTSPRMS");
            DropIndex("dbo.T_RECORDSVALUES", new[] { "F_LOCATION_ID" });
            CreateTable(
                "dbo.T_RECORDSPRMS",
                c => new
                    {
                        F_ID = c.Int(nullable: false, identity: true),
                        F_RECORDVAL_ID = c.Int(nullable: false),
                        F_ELPRM_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.F_ID)
                .ForeignKey("dbo.T_ELEMENTSPRMS", t => t.F_ELPRM_ID, cascadeDelete: true)
                .ForeignKey("dbo.T_RECORDSVALUES", t => t.F_RECORDVAL_ID)
                .Index(t => t.F_RECORDVAL_ID)
                .Index(t => t.F_ELPRM_ID);
            
            DropColumn("dbo.T_RECORDSVALUES", "F_LOCATION_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.T_RECORDSVALUES", "F_LOCATION_ID", c => c.Int());
            DropForeignKey("dbo.T_RECORDSPRMS", "F_RECORDVAL_ID", "dbo.T_RECORDSVALUES");
            DropForeignKey("dbo.T_RECORDSPRMS", "F_ELPRM_ID", "dbo.T_ELEMENTSPRMS");
            DropIndex("dbo.T_RECORDSPRMS", new[] { "F_ELPRM_ID" });
            DropIndex("dbo.T_RECORDSPRMS", new[] { "F_RECORDVAL_ID" });
            DropTable("dbo.T_RECORDSPRMS");
            CreateIndex("dbo.T_RECORDSVALUES", "F_LOCATION_ID");
            AddForeignKey("dbo.T_RECORDSVALUES", "F_LOCATION_ID", "dbo.T_ELEMENTSPRMS", "F_ID");
        }
    }
}
