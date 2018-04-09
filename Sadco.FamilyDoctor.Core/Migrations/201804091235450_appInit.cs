namespace Sadco.FamilyDoctor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appInit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.T_AGENORMS",
                c => new
                    {
                        F_ID = c.Int(nullable: false, identity: true),
                        F_ELEMENT_ID = c.Int(nullable: false),
                        F_AGEFROM = c.Byte(nullable: false),
                        F_AGETO = c.Byte(nullable: false),
                        F_MALEMIN = c.Decimal(nullable: false, precision: 10, scale: 6),
                        F_MALEMAX = c.Decimal(nullable: false, precision: 10, scale: 6),
                        F_FEMALEMIN = c.Decimal(nullable: false, precision: 10, scale: 6),
                        F_FEMALEMAX = c.Decimal(nullable: false, precision: 10, scale: 6),
                    })
                .PrimaryKey(t => t.F_ID)
                .ForeignKey("dbo.T_ELEMENTS", t => t.F_ELEMENT_ID, cascadeDelete: true)
                .Index(t => t.F_ELEMENT_ID);
            
            CreateTable(
                "dbo.T_ELEMENTS",
                c => new
                    {
                        F_ID = c.Int(nullable: false, identity: true),
                        F_ELEMENT_ID = c.Int(nullable: false),
                        F_NAME = c.String(maxLength: 100, unicode: false),
                        F_TAG = c.String(maxLength: 60, unicode: false),
                        F_HANDLERID = c.String(maxLength: 100),
                        F_HANDLER_ARGS = c.String(maxLength: 200),
                        F_ELEMENT_TYPE = c.Byte(nullable: false),
                        F_VERSION = c.Int(nullable: false),
                        F_REQUIRED = c.Boolean(nullable: false),
                        F_EDITING = c.Boolean(nullable: false),
                        F_ISMULTI = c.Boolean(nullable: false),
                        F_ISNUMBER = c.Boolean(nullable: false),
                        F_NUMROUND = c.Byte(nullable: false),
                        F_NUMFORMULA = c.String(maxLength: 300, unicode: false),
                        F_VISIBLE = c.Boolean(nullable: false),
                        F_VISIBLEPATIENT = c.Boolean(nullable: false),
                        F_HELP = c.String(maxLength: 500, unicode: false),
                        F_SYMMETRICAL = c.Boolean(nullable: false),
                        F_SYMMETRYPARAMLEFT = c.String(maxLength: 50, unicode: false),
                        F_SYMMETRYPARAMRIGHT = c.String(maxLength: 50, unicode: false),
                        F_DEFAULT = c.String(maxLength: 100, unicode: false),
                        F_ISPARTPRE = c.Boolean(nullable: false),
                        F_PARTPRE = c.String(maxLength: 100, unicode: false),
                        F_ISPARTPOST = c.Boolean(nullable: false),
                        F_PARTPOST = c.String(maxLength: 100, unicode: false),
                        F_ISPARTLOCATIONS = c.Boolean(nullable: false),
                        F_ISPARTLOCATIONSMULTI = c.Boolean(nullable: false),
                        F_ISPARTNORM = c.Boolean(nullable: false),
                        F_PARTNORM = c.Decimal(nullable: false, precision: 10, scale: 6),
                        F_ISPARTNORMRANGE = c.Boolean(nullable: false),
                        F_ISCHANGENOTNORM = c.Boolean(nullable: false),
                        F_VISIBILITYFORMULA = c.String(maxLength: 1000, unicode: false),
                        F_COMMENT = c.String(maxLength: 100, unicode: false),
                        F_GROUP_ID = c.Int(nullable: false),
                        F_ISARHIVE = c.Boolean(nullable: false),
                        F_IMAGE = c.Binary(),
                    })
                .PrimaryKey(t => t.F_ID)
                .ForeignKey("dbo.T_GROUPS", t => t.F_GROUP_ID, cascadeDelete: true)
                .Index(t => t.F_GROUP_ID);
            
            CreateTable(
                "dbo.T_ELEMENTSPRMS",
                c => new
                    {
                        F_ID = c.Int(nullable: false, identity: true),
                        F_ELEMENT_ID = c.Int(nullable: false),
                        F_TYPEPARAM = c.Byte(nullable: false),
                        F_VALUE = c.String(),
                    })
                .PrimaryKey(t => t.F_ID)
                .ForeignKey("dbo.T_ELEMENTS", t => t.F_ELEMENT_ID, cascadeDelete: true)
                .Index(t => t.F_ELEMENT_ID);
            
            CreateTable(
                "dbo.T_GROUPS",
                c => new
                    {
                        F_ID = c.Int(nullable: false, identity: true),
                        F_TYPE = c.Byte(nullable: false),
                        F_NAME = c.String(maxLength: 100, unicode: false),
                        F_PARENT_ID = c.Int(),
                        F_ISARHIVE = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.F_ID)
                .ForeignKey("dbo.T_GROUPS", t => t.F_PARENT_ID)
                .Index(t => t.F_PARENT_ID);
            
            CreateTable(
                "dbo.T_LOGS",
                c => new
                    {
                        F_ID = c.Int(nullable: false, identity: true),
                        F_ELEMENT_ID = c.Int(nullable: false),
                        F_TYPE = c.Int(nullable: false),
                        F_TIME = c.DateTime(nullable: false),
                        F_VERSION = c.Int(nullable: false),
                        F_EVENT = c.String(),
                        F_USER = c.String(),
                    })
                .PrimaryKey(t => t.F_ID);
            
            CreateTable(
                "dbo.T_TEMPLATES",
                c => new
                    {
                        F_ID = c.Int(nullable: false, identity: true),
                        F_TEMPLATE_ID = c.Int(nullable: false),
                        F_GROUP_ID = c.Int(nullable: false),
                        F_TYPE = c.Byte(nullable: false),
                        F_NAME = c.String(maxLength: 100, unicode: false),
                        F_DESC = c.String(maxLength: 1000, unicode: false),
                        F_VERSION = c.Int(nullable: false),
                        F_ISCONFLICT = c.Boolean(nullable: false),
                        F_ISARHIVE = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.F_ID)
                .ForeignKey("dbo.T_GROUPS", t => t.F_GROUP_ID, cascadeDelete: true)
                .Index(t => t.F_GROUP_ID);
            
            CreateTable(
                "dbo.T_TEMPLATESELEMENTS",
                c => new
                    {
                        F_ID = c.Int(nullable: false, identity: true),
                        F_TEMPLATE_ID = c.Int(nullable: false),
                        F_CHILDELEMENT_ID = c.Int(),
                        F_CHILDTEMPLATE_ID = c.Int(),
                        F_INDEX = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.F_ID)
                .ForeignKey("dbo.T_ELEMENTS", t => t.F_CHILDELEMENT_ID)
                .ForeignKey("dbo.T_TEMPLATES", t => t.F_CHILDTEMPLATE_ID)
                .ForeignKey("dbo.T_TEMPLATES", t => t.F_TEMPLATE_ID, cascadeDelete: true)
                .Index(t => t.F_TEMPLATE_ID)
                .Index(t => t.F_CHILDELEMENT_ID)
                .Index(t => t.F_CHILDTEMPLATE_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.T_TEMPLATESELEMENTS", "F_TEMPLATE_ID", "dbo.T_TEMPLATES");
            DropForeignKey("dbo.T_TEMPLATESELEMENTS", "F_CHILDTEMPLATE_ID", "dbo.T_TEMPLATES");
            DropForeignKey("dbo.T_TEMPLATESELEMENTS", "F_CHILDELEMENT_ID", "dbo.T_ELEMENTS");
            DropForeignKey("dbo.T_TEMPLATES", "F_GROUP_ID", "dbo.T_GROUPS");
            DropForeignKey("dbo.T_AGENORMS", "F_ELEMENT_ID", "dbo.T_ELEMENTS");
            DropForeignKey("dbo.T_ELEMENTS", "F_GROUP_ID", "dbo.T_GROUPS");
            DropForeignKey("dbo.T_GROUPS", "F_PARENT_ID", "dbo.T_GROUPS");
            DropForeignKey("dbo.T_ELEMENTSPRMS", "F_ELEMENT_ID", "dbo.T_ELEMENTS");
            DropIndex("dbo.T_TEMPLATESELEMENTS", new[] { "F_CHILDTEMPLATE_ID" });
            DropIndex("dbo.T_TEMPLATESELEMENTS", new[] { "F_CHILDELEMENT_ID" });
            DropIndex("dbo.T_TEMPLATESELEMENTS", new[] { "F_TEMPLATE_ID" });
            DropIndex("dbo.T_TEMPLATES", new[] { "F_GROUP_ID" });
            DropIndex("dbo.T_GROUPS", new[] { "F_PARENT_ID" });
            DropIndex("dbo.T_ELEMENTSPRMS", new[] { "F_ELEMENT_ID" });
            DropIndex("dbo.T_ELEMENTS", new[] { "F_GROUP_ID" });
            DropIndex("dbo.T_AGENORMS", new[] { "F_ELEMENT_ID" });
            DropTable("dbo.T_TEMPLATESELEMENTS");
            DropTable("dbo.T_TEMPLATES");
            DropTable("dbo.T_LOGS");
            DropTable("dbo.T_GROUPS");
            DropTable("dbo.T_ELEMENTSPRMS");
            DropTable("dbo.T_ELEMENTS");
            DropTable("dbo.T_AGENORMS");
        }
    }
}
