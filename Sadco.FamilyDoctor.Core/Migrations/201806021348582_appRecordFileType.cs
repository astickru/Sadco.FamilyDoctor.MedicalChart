namespace Sadco.FamilyDoctor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appRecordFileType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_RECORDS", "F_FILETYPE", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.T_RECORDS", "F_FILETYPE");
        }
    }
}
