namespace Sadco.FamilyDoctor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appPattern : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.T_RECORDS_PATTERNS",
                c => new
                    {
                        F_ID = c.Int(nullable: false, identity: true),
                        F_TITLE = c.String(),
                        F_CATEGORYTOTAL_ID = c.Int(),
                        F_CATEGORYCLINIK_ID = c.Int(),
                        F_TEMPLATE_ID = c.Int(),
                    })
                .PrimaryKey(t => t.F_ID)
                .ForeignKey("dbo.T_CATEGORIES", t => t.F_CATEGORYCLINIK_ID)
                .ForeignKey("dbo.T_CATEGORIES", t => t.F_CATEGORYTOTAL_ID)
                .ForeignKey("dbo.T_TEMPLATES", t => t.F_TEMPLATE_ID)
                .Index(t => t.F_CATEGORYTOTAL_ID)
                .Index(t => t.F_CATEGORYCLINIK_ID)
                .Index(t => t.F_TEMPLATE_ID);
            
            CreateTable(
                "dbo.T_RECORDSPATTERNSVALUES",
                c => new
                    {
                        F_ID = c.Int(nullable: false, identity: true),
                        F_RECORDPATTERN_ID = c.Int(nullable: false),
                        F_ELEMENT_ID = c.Int(nullable: false),
                        F_VALUE = c.String(),
                        F_VALUEDOP = c.String(),
                        F_VALUEIMAGE = c.Binary(),
                    })
                .PrimaryKey(t => t.F_ID)
                .ForeignKey("dbo.T_ELEMENTS", t => t.F_ELEMENT_ID, cascadeDelete: true)
                .ForeignKey("dbo.T_RECORDS_PATTERNS", t => t.F_RECORDPATTERN_ID)
                .Index(t => t.F_RECORDPATTERN_ID)
                .Index(t => t.F_ELEMENT_ID);
            
            CreateTable(
                "dbo.T_RECORDSPATTERNSPRMS",
                c => new
                    {
                        F_ID = c.Int(nullable: false, identity: true),
                        F_RECORDPATTERNPRM_ID = c.Int(nullable: false),
                        F_ELPRM_ID = c.Int(nullable: false),
                        F_ISDOP = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.F_ID)
                .ForeignKey("dbo.T_ELEMENTSPRMS", t => t.F_ELPRM_ID, cascadeDelete: true)
                .ForeignKey("dbo.T_RECORDSPATTERNSVALUES", t => t.F_RECORDPATTERNPRM_ID)
                .Index(t => t.F_RECORDPATTERNPRM_ID)
                .Index(t => t.F_ELPRM_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.T_RECORDSPATTERNSVALUES", "F_RECORDPATTERN_ID", "dbo.T_RECORDS_PATTERNS");
            DropForeignKey("dbo.T_RECORDSPATTERNSPRMS", "F_RECORDPATTERNPRM_ID", "dbo.T_RECORDSPATTERNSVALUES");
            DropForeignKey("dbo.T_RECORDSPATTERNSPRMS", "F_ELPRM_ID", "dbo.T_ELEMENTSPRMS");
            DropForeignKey("dbo.T_RECORDSPATTERNSVALUES", "F_ELEMENT_ID", "dbo.T_ELEMENTS");
            DropForeignKey("dbo.T_RECORDS_PATTERNS", "F_TEMPLATE_ID", "dbo.T_TEMPLATES");
            DropForeignKey("dbo.T_RECORDS_PATTERNS", "F_CATEGORYTOTAL_ID", "dbo.T_CATEGORIES");
            DropForeignKey("dbo.T_RECORDS_PATTERNS", "F_CATEGORYCLINIK_ID", "dbo.T_CATEGORIES");
            DropIndex("dbo.T_RECORDSPATTERNSPRMS", new[] { "F_ELPRM_ID" });
            DropIndex("dbo.T_RECORDSPATTERNSPRMS", new[] { "F_RECORDPATTERNPRM_ID" });
            DropIndex("dbo.T_RECORDSPATTERNSVALUES", new[] { "F_ELEMENT_ID" });
            DropIndex("dbo.T_RECORDSPATTERNSVALUES", new[] { "F_RECORDPATTERN_ID" });
            DropIndex("dbo.T_RECORDS_PATTERNS", new[] { "F_TEMPLATE_ID" });
            DropIndex("dbo.T_RECORDS_PATTERNS", new[] { "F_CATEGORYCLINIK_ID" });
            DropIndex("dbo.T_RECORDS_PATTERNS", new[] { "F_CATEGORYTOTAL_ID" });
            DropTable("dbo.T_RECORDSPATTERNSPRMS");
            DropTable("dbo.T_RECORDSPATTERNSVALUES");
            DropTable("dbo.T_RECORDS_PATTERNS");
        }
    }
}
