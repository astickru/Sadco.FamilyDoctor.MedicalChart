namespace Sadco.FamilyDoctor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appLocations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.T_RECORDSVALUES", "F_LOCATION_ID", "dbo.T_ELEMENTSPRMS");
            DropForeignKey("dbo.T_RECORDSVALUES", "F_VALUE_ID", "dbo.T_ELEMENTSPRMS");
            DropForeignKey("dbo.T_RECORDSVALUES", "F_VALUEDOP_ID", "dbo.T_ELEMENTSPRMS");
            DropIndex("dbo.T_RECORDSVALUES", new[] { "F_LOCATION_ID" });
            DropIndex("dbo.T_RECORDSVALUES", new[] { "F_VALUE_ID" });
            DropIndex("dbo.T_RECORDSVALUES", new[] { "F_VALUEDOP_ID" });
            CreateTable(
                "dbo.T_RECORDSPRMS",
                c => new
                    {
                        F_ID = c.Int(nullable: false, identity: true),
                        F_RECORDVAL_ID = c.Int(nullable: false),
                        F_ELPRM_ID = c.Int(nullable: false),
                        F_ISDOP = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.F_ID)
                .ForeignKey("dbo.T_ELEMENTSPRMS", t => t.F_ELPRM_ID, cascadeDelete: true)
                .ForeignKey("dbo.T_RECORDSVALUES", t => t.F_RECORDVAL_ID)
                .Index(t => t.F_RECORDVAL_ID)
                .Index(t => t.F_ELPRM_ID);
            
            AddColumn("dbo.T_ELEMENTS", "F_DEFAULT_ID", c => c.Int());
            AddColumn("dbo.T_RECORDS", "F_DATELASTCHANGE", c => c.DateTime(nullable: false));
            AddColumn("dbo.T_RECORDSVALUES", "F_VALUEIMAGE", c => c.Binary());
            CreateIndex("dbo.T_ELEMENTS", "F_DEFAULT_ID");
            AddForeignKey("dbo.T_ELEMENTS", "F_DEFAULT_ID", "dbo.T_ELEMENTSPRMS", "F_ID");
            DropColumn("dbo.T_ELEMENTS", "F_DEFAULT");
            DropColumn("dbo.T_RECORDSVALUES", "F_LOCATION_ID");
            DropColumn("dbo.T_RECORDSVALUES", "F_LOCATION");
            DropColumn("dbo.T_RECORDSVALUES", "F_VALUE_ID");
            DropColumn("dbo.T_RECORDSVALUES", "F_VALUEDOP_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.T_RECORDSVALUES", "F_VALUEDOP_ID", c => c.Int());
            AddColumn("dbo.T_RECORDSVALUES", "F_VALUE_ID", c => c.Int());
            AddColumn("dbo.T_RECORDSVALUES", "F_LOCATION", c => c.String());
            AddColumn("dbo.T_RECORDSVALUES", "F_LOCATION_ID", c => c.Int());
            AddColumn("dbo.T_ELEMENTS", "F_DEFAULT", c => c.String(maxLength: 100, unicode: false));
            DropForeignKey("dbo.T_RECORDSPRMS", "F_RECORDVAL_ID", "dbo.T_RECORDSVALUES");
            DropForeignKey("dbo.T_RECORDSPRMS", "F_ELPRM_ID", "dbo.T_ELEMENTSPRMS");
            DropForeignKey("dbo.T_ELEMENTS", "F_DEFAULT_ID", "dbo.T_ELEMENTSPRMS");
            DropIndex("dbo.T_RECORDSPRMS", new[] { "F_ELPRM_ID" });
            DropIndex("dbo.T_RECORDSPRMS", new[] { "F_RECORDVAL_ID" });
            DropIndex("dbo.T_ELEMENTS", new[] { "F_DEFAULT_ID" });
            DropColumn("dbo.T_RECORDSVALUES", "F_VALUEIMAGE");
            DropColumn("dbo.T_RECORDS", "F_DATELASTCHANGE");
            DropColumn("dbo.T_ELEMENTS", "F_DEFAULT_ID");
            DropTable("dbo.T_RECORDSPRMS");
            CreateIndex("dbo.T_RECORDSVALUES", "F_VALUEDOP_ID");
            CreateIndex("dbo.T_RECORDSVALUES", "F_VALUE_ID");
            CreateIndex("dbo.T_RECORDSVALUES", "F_LOCATION_ID");
            AddForeignKey("dbo.T_RECORDSVALUES", "F_VALUEDOP_ID", "dbo.T_ELEMENTSPRMS", "F_ID");
            AddForeignKey("dbo.T_RECORDSVALUES", "F_VALUE_ID", "dbo.T_ELEMENTSPRMS", "F_ID");
            AddForeignKey("dbo.T_RECORDSVALUES", "F_LOCATION_ID", "dbo.T_ELEMENTSPRMS", "F_ID");
        }
    }
}
