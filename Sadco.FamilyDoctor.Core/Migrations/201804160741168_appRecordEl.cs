namespace Sadco.FamilyDoctor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appRecordEl : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.T_RECORDSVALUES", "F_RECORD_ID", "dbo.T_RECORDS");
            AddColumn("dbo.T_RECORDSVALUES", "F_ELEMENT_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.T_RECORDSVALUES", "F_ELEMENT_ID");
            AddForeignKey("dbo.T_RECORDSVALUES", "F_ELEMENT_ID", "dbo.T_ELEMENTS", "F_ID", cascadeDelete: true);
            AddForeignKey("dbo.T_RECORDSVALUES", "F_RECORD_ID", "dbo.T_RECORDS", "F_ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.T_RECORDSVALUES", "F_RECORD_ID", "dbo.T_RECORDS");
            DropForeignKey("dbo.T_RECORDSVALUES", "F_ELEMENT_ID", "dbo.T_ELEMENTS");
            DropIndex("dbo.T_RECORDSVALUES", new[] { "F_ELEMENT_ID" });
            DropColumn("dbo.T_RECORDSVALUES", "F_ELEMENT_ID");
            AddForeignKey("dbo.T_RECORDSVALUES", "F_RECORD_ID", "dbo.T_RECORDS", "F_ID", cascadeDelete: true);
        }
    }
}
