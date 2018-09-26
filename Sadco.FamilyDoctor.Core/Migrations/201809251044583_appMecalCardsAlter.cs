namespace Sadco.FamilyDoctor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appMecalCardsAlter : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.T_MEDICALCARD", "F_NUMBER", c => c.String(maxLength: 50, unicode: false));
            CreateIndex("dbo.T_MEDICALCARD", new[] { "F_PATIENT_ID", "F_NUMBER" }, unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.T_MEDICALCARD", new[] { "F_PATIENT_ID", "F_NUMBER" });
            AlterColumn("dbo.T_MEDICALCARD", "F_NUMBER", c => c.String());
        }
    }
}
