namespace Sadco.FamilyDoctor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appClinic : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.T_RECORDS", name: "F_CATEGORYCLINIK_ID", newName: "F_CATEGORYCLINIC_ID");
            RenameColumn(table: "dbo.T_TEMPLATES", name: "F_CATEGORYCLINIK_ID", newName: "F_CATEGORYCLINIC_ID");
            RenameColumn(table: "dbo.T_RECORDS_PATTERNS", name: "F_CATEGORYCLINIK_ID", newName: "F_CATEGORYCLINIC_ID");
            RenameIndex(table: "dbo.T_RECORDS", name: "IX_F_CATEGORYCLINIK_ID", newName: "IX_F_CATEGORYCLINIC_ID");
            RenameIndex(table: "dbo.T_TEMPLATES", name: "IX_F_CATEGORYCLINIK_ID", newName: "IX_F_CATEGORYCLINIC_ID");
            RenameIndex(table: "dbo.T_RECORDS_PATTERNS", name: "IX_F_CATEGORYCLINIK_ID", newName: "IX_F_CATEGORYCLINIC_ID");
            AddColumn("dbo.T_RECORDS", "F_ISAUTOMATIC", c => c.Boolean(nullable: false));
            AddColumn("dbo.T_RECORDS", "F_CLINICNAME", c => c.String());
            AddColumn("dbo.T_RECORDS_PATTERNS", "F_CLINICNAME", c => c.String());
            DropColumn("dbo.T_RECORDS", "F_ISAUTIMATIC");
            DropColumn("dbo.T_RECORDS", "F_CLINIKNAME");
            DropColumn("dbo.T_RECORDS_PATTERNS", "F_CLINIKNAME");
        }
        
        public override void Down()
        {
            AddColumn("dbo.T_RECORDS_PATTERNS", "F_CLINIKNAME", c => c.String());
            AddColumn("dbo.T_RECORDS", "F_CLINIKNAME", c => c.String());
            AddColumn("dbo.T_RECORDS", "F_ISAUTIMATIC", c => c.Boolean(nullable: false));
            DropColumn("dbo.T_RECORDS_PATTERNS", "F_CLINICNAME");
            DropColumn("dbo.T_RECORDS", "F_CLINICNAME");
            DropColumn("dbo.T_RECORDS", "F_ISAUTOMATIC");
            RenameIndex(table: "dbo.T_RECORDS_PATTERNS", name: "IX_F_CATEGORYCLINIC_ID", newName: "IX_F_CATEGORYCLINIK_ID");
            RenameIndex(table: "dbo.T_TEMPLATES", name: "IX_F_CATEGORYCLINIC_ID", newName: "IX_F_CATEGORYCLINIK_ID");
            RenameIndex(table: "dbo.T_RECORDS", name: "IX_F_CATEGORYCLINIC_ID", newName: "IX_F_CATEGORYCLINIK_ID");
            RenameColumn(table: "dbo.T_RECORDS_PATTERNS", name: "F_CATEGORYCLINIC_ID", newName: "F_CATEGORYCLINIK_ID");
            RenameColumn(table: "dbo.T_TEMPLATES", name: "F_CATEGORYCLINIC_ID", newName: "F_CATEGORYCLINIK_ID");
            RenameColumn(table: "dbo.T_RECORDS", name: "F_CATEGORYCLINIC_ID", newName: "F_CATEGORYCLINIK_ID");
        }
    }
}
