namespace Sadco.FamilyDoctor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appInitMecalCards : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.T_MEDICALCARD",
                c => new
                    {
                        F_ID = c.Int(nullable: false, identity: true),
                        F_NUMBER = c.String(),
                        F_ISARCHIVE = c.Boolean(nullable: false),
                        F_ISDEL = c.Boolean(nullable: false),
                        F_DATECREATE = c.DateTime(nullable: false),
                        F_DATEARCHIVE = c.DateTime(nullable: false),
                        F_DATEDELETE = c.DateTime(nullable: false),
                        F_DATEMERGE = c.DateTime(nullable: false),
                        F_PATIENT_ID = c.Int(nullable: false),
                        F_PATIENT_UID = c.Guid(),
                        F_GENDER = c.Byte(nullable: false),
                        F_PATIENT_NAME = c.String(),
                        F_PATIENT_SURNAME = c.String(),
                        F_PATIENT_LASTNAME = c.String(),
                        F_PATIENT_DATEBIRTH = c.DateTime(nullable: false),
                        F_COMMENT = c.String(),
                        F_DATEPRINTTITLE = c.DateTime(),
                    })
                .PrimaryKey(t => t.F_ID);
            
            DropColumn("dbo.T_RECORDS", "F_CARD_ID");
            DropColumn("dbo.T_RECORDS", "F_ISARCHIVE");
            DropColumn("dbo.T_RECORDS", "F_PATIENT_ID");
            DropColumn("dbo.T_RECORDS", "F_PATIENT_UID");
            DropColumn("dbo.T_RECORDS", "F_GENDER");
            DropColumn("dbo.T_RECORDS", "F_PATIENT_NAME");
            DropColumn("dbo.T_RECORDS", "F_PATIENT_SURNAME");
            DropColumn("dbo.T_RECORDS", "F_PATIENT_LASTNAME");
            DropColumn("dbo.T_RECORDS", "F_PATIENT_DATEBIRTH");
        }
        
        public override void Down()
        {
            AddColumn("dbo.T_RECORDS", "F_PATIENT_DATEBIRTH", c => c.DateTime(nullable: false));
            AddColumn("dbo.T_RECORDS", "F_PATIENT_LASTNAME", c => c.String());
            AddColumn("dbo.T_RECORDS", "F_PATIENT_SURNAME", c => c.String());
            AddColumn("dbo.T_RECORDS", "F_PATIENT_NAME", c => c.String());
            AddColumn("dbo.T_RECORDS", "F_GENDER", c => c.Byte(nullable: false));
            AddColumn("dbo.T_RECORDS", "F_PATIENT_UID", c => c.Guid());
            AddColumn("dbo.T_RECORDS", "F_PATIENT_ID", c => c.Int(nullable: false));
            AddColumn("dbo.T_RECORDS", "F_ISARCHIVE", c => c.Boolean(nullable: false));
            AddColumn("dbo.T_RECORDS", "F_CARD_ID", c => c.Int(nullable: false));
            DropTable("dbo.T_MEDICALCARD");
        }
    }
}
