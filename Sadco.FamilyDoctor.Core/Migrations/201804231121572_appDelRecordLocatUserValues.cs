namespace Sadco.FamilyDoctor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appDelRecordLocatUserValues : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_RECORDSVALUES", "F_VALUEIMAGE", c => c.Binary());
            DropColumn("dbo.T_RECORDSVALUES", "F_LOCATION");
        }
        
        public override void Down()
        {
            AddColumn("dbo.T_RECORDSVALUES", "F_LOCATION", c => c.String());
            DropColumn("dbo.T_RECORDSVALUES", "F_VALUEIMAGE");
        }
    }
}
