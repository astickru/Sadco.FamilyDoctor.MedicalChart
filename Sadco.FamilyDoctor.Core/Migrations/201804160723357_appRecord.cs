namespace Sadco.FamilyDoctor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appRecord : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.T_RECORDS",
                c => new
                    {
                        F_ID = c.Int(nullable: false, identity: true),
                        F_RECORD_ID = c.Int(nullable: false),
                        F_VERSION = c.Int(nullable: false),
                        F_ISDEL = c.Boolean(nullable: false),
                        F_SEX = c.Byte(nullable: false),
                        F_DATEBIRTH = c.DateTime(nullable: false, storeType: "date"),
                        F_DATEFORMING = c.DateTime(nullable: false),
                        F_DATECREATE = c.DateTime(nullable: false),
                        F_CARD_ID = c.Int(nullable: false),
                        F_ISARCHIVE = c.Boolean(nullable: false),
                        F_ISPRINT = c.Boolean(nullable: false),
                        F_ISAUTIMATIC = c.Boolean(nullable: false),
                        F_USER_ID = c.Int(nullable: false),
                        F_USER_NAME = c.String(),
                        F_USER_SURNAME = c.String(),
                        F_USER_LASTNAME = c.String(),
                        F_PATIENT_ID = c.Int(nullable: false),
                        F_PATIENT_NAME = c.String(),
                        F_PATIENT_SURNAME = c.String(),
                        F_PATIENT_LASTNAME = c.String(),
                        F_TEMPLATE_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.F_ID)
                .ForeignKey("dbo.T_TEMPLATES", t => t.F_TEMPLATE_ID, cascadeDelete: true)
                .Index(t => t.F_TEMPLATE_ID);
            
            CreateTable(
                "dbo.T_RECORDSVALUES",
                c => new
                    {
                        F_ID = c.Int(nullable: false, identity: true),
                        F_RECORD_ID = c.Int(nullable: false),
                        F_LOCATION_ID = c.Int(),
                        F_LOCATION = c.String(),
                        F_VALUE_ID = c.Int(),
                        F_VALUE = c.String(),
                        F_VALUEDOP_ID = c.Int(),
                        F_VALUEDOP = c.String(),
                    })
                .PrimaryKey(t => t.F_ID)
                .ForeignKey("dbo.T_ELEMENTSPRMS", t => t.F_LOCATION_ID)
                .ForeignKey("dbo.T_RECORDS", t => t.F_RECORD_ID, cascadeDelete: true)
                .ForeignKey("dbo.T_ELEMENTSPRMS", t => t.F_VALUE_ID)
                .ForeignKey("dbo.T_ELEMENTSPRMS", t => t.F_VALUEDOP_ID)
                .Index(t => t.F_RECORD_ID)
                .Index(t => t.F_LOCATION_ID)
                .Index(t => t.F_VALUE_ID)
                .Index(t => t.F_VALUEDOP_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.T_RECORDSVALUES", "F_VALUEDOP_ID", "dbo.T_ELEMENTSPRMS");
            DropForeignKey("dbo.T_RECORDSVALUES", "F_VALUE_ID", "dbo.T_ELEMENTSPRMS");
            DropForeignKey("dbo.T_RECORDSVALUES", "F_RECORD_ID", "dbo.T_RECORDS");
            DropForeignKey("dbo.T_RECORDSVALUES", "F_LOCATION_ID", "dbo.T_ELEMENTSPRMS");
            DropForeignKey("dbo.T_RECORDS", "F_TEMPLATE_ID", "dbo.T_TEMPLATES");
            DropIndex("dbo.T_RECORDSVALUES", new[] { "F_VALUEDOP_ID" });
            DropIndex("dbo.T_RECORDSVALUES", new[] { "F_VALUE_ID" });
            DropIndex("dbo.T_RECORDSVALUES", new[] { "F_LOCATION_ID" });
            DropIndex("dbo.T_RECORDSVALUES", new[] { "F_RECORD_ID" });
            DropIndex("dbo.T_RECORDS", new[] { "F_TEMPLATE_ID" });
            DropTable("dbo.T_RECORDSVALUES");
            DropTable("dbo.T_RECORDS");
        }
    }
}
