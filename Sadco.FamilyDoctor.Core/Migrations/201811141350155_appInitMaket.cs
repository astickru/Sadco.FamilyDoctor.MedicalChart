namespace Sadco.FamilyDoctor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appInitMaket : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_TEMPLATES", "F_COUNTCOLUMN", c => c.Int(nullable: false));
            AlterColumn("dbo.T_ELEMENTS", "F_NAME", c => c.String(maxLength: 1000, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.T_ELEMENTS", "F_NAME", c => c.String(maxLength: 100, unicode: false));
            DropColumn("dbo.T_TEMPLATES", "F_COUNTCOLUMN");
        }
    }
}
