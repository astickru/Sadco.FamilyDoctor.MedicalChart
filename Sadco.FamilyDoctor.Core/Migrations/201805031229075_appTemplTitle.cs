namespace Sadco.FamilyDoctor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appTemplTitle : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.T_RECORDS", name: "F_SEX", newName: "F_GENDER");
            AddColumn("dbo.T_TEMPLATES", "F_TITLE", c => c.String(maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.T_TEMPLATES", "F_TITLE");
            RenameColumn(table: "dbo.T_RECORDS", name: "F_GENDER", newName: "F_SEX");
        }
    }
}
