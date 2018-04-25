namespace Sadco.FamilyDoctor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appRecordCatalogNew : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.T_RECORDSVALUES", "F_VALUE_ID", "dbo.T_ELEMENTSPRMS");
            DropForeignKey("dbo.T_RECORDSVALUES", "F_VALUEDOP_ID", "dbo.T_ELEMENTSPRMS");
            DropIndex("dbo.T_RECORDSVALUES", new[] { "F_VALUE_ID" });
            DropIndex("dbo.T_RECORDSVALUES", new[] { "F_VALUEDOP_ID" });
            AddColumn("dbo.T_RECORDSPRMS", "F_ISDOP", c => c.Boolean(nullable: false));
            DropColumn("dbo.T_RECORDSVALUES", "F_VALUE_ID");
            DropColumn("dbo.T_RECORDSVALUES", "F_VALUEDOP_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.T_RECORDSVALUES", "F_VALUEDOP_ID", c => c.Int());
            AddColumn("dbo.T_RECORDSVALUES", "F_VALUE_ID", c => c.Int());
            DropColumn("dbo.T_RECORDSPRMS", "F_ISDOP");
            CreateIndex("dbo.T_RECORDSVALUES", "F_VALUEDOP_ID");
            CreateIndex("dbo.T_RECORDSVALUES", "F_VALUE_ID");
            AddForeignKey("dbo.T_RECORDSVALUES", "F_VALUEDOP_ID", "dbo.T_ELEMENTSPRMS", "F_ID");
            AddForeignKey("dbo.T_RECORDSVALUES", "F_VALUE_ID", "dbo.T_ELEMENTSPRMS", "F_ID");
        }
    }
}
