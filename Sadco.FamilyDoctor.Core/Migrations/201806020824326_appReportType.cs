namespace Sadco.FamilyDoctor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appReportType : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.T_RECORDS", name: "F_CATEGORYKLINIK_ID", newName: "F_CATEGORYCLINIK_ID");
            RenameColumn(table: "dbo.T_TEMPLATES", name: "F_CATEGORYKLINIK_ID", newName: "F_CATEGORYCLINIK_ID");
            RenameIndex(table: "dbo.T_RECORDS", name: "IX_F_CATEGORYKLINIK_ID", newName: "IX_F_CATEGORYCLINIK_ID");
            RenameIndex(table: "dbo.T_TEMPLATES", name: "IX_F_CATEGORYKLINIK_ID", newName: "IX_F_CATEGORYCLINIK_ID");
            AddColumn("dbo.T_RECORDS", "F_TYPE", c => c.Byte(nullable: false));
            AddColumn("dbo.T_RECORDS", "F_CLINIKNAME", c => c.String());
            DropColumn("dbo.T_RECORDS", "F_KLINIKNAME");
        }
        
        public override void Down()
        {
            AddColumn("dbo.T_RECORDS", "F_KLINIKNAME", c => c.String());
            DropColumn("dbo.T_RECORDS", "F_CLINIKNAME");
            DropColumn("dbo.T_RECORDS", "F_TYPE");
            RenameIndex(table: "dbo.T_TEMPLATES", name: "IX_F_CATEGORYCLINIK_ID", newName: "IX_F_CATEGORYKLINIK_ID");
            RenameIndex(table: "dbo.T_RECORDS", name: "IX_F_CATEGORYCLINIK_ID", newName: "IX_F_CATEGORYKLINIK_ID");
            RenameColumn(table: "dbo.T_TEMPLATES", name: "F_CATEGORYCLINIK_ID", newName: "F_CATEGORYKLINIK_ID");
            RenameColumn(table: "dbo.T_RECORDS", name: "F_CATEGORYCLINIK_ID", newName: "F_CATEGORYKLINIK_ID");
        }
    }
}
