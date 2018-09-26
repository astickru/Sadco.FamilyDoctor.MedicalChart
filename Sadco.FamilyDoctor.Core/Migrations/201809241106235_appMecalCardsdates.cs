namespace Sadco.FamilyDoctor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appMecalCardsdates : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.T_MEDICALCARD", "F_DATEARCHIVE", c => c.DateTime());
            AlterColumn("dbo.T_MEDICALCARD", "F_DATEDELETE", c => c.DateTime());
            AlterColumn("dbo.T_MEDICALCARD", "F_DATEMERGE", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.T_MEDICALCARD", "F_DATEMERGE", c => c.DateTime(nullable: false));
            AlterColumn("dbo.T_MEDICALCARD", "F_DATEDELETE", c => c.DateTime(nullable: false));
            AlterColumn("dbo.T_MEDICALCARD", "F_DATEARCHIVE", c => c.DateTime(nullable: false));
        }
    }
}
