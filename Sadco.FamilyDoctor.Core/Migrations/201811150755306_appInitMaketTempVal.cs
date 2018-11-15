namespace Sadco.FamilyDoctor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appInitMaketTempVal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_TEMPLATESELEMENTS", "F_VALUE", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.T_TEMPLATESELEMENTS", "F_VALUE");
        }
    }
}
