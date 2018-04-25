namespace Sadco.FamilyDoctor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appElementDefault : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_ELEMENTS", "F_DEFAULT_ID", c => c.Int());
            CreateIndex("dbo.T_ELEMENTS", "F_DEFAULT_ID");
            AddForeignKey("dbo.T_ELEMENTS", "F_DEFAULT_ID", "dbo.T_ELEMENTSPRMS", "F_ID");
            DropColumn("dbo.T_ELEMENTS", "F_DEFAULT");
        }
        
        public override void Down()
        {
            AddColumn("dbo.T_ELEMENTS", "F_DEFAULT", c => c.String(maxLength: 100, unicode: false));
            DropForeignKey("dbo.T_ELEMENTS", "F_DEFAULT_ID", "dbo.T_ELEMENTSPRMS");
            DropIndex("dbo.T_ELEMENTS", new[] { "F_DEFAULT_ID" });
            DropColumn("dbo.T_ELEMENTS", "F_DEFAULT_ID");
        }
    }
}
