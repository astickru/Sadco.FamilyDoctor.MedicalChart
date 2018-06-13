namespace Sadco.FamilyDoctor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appRatings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.T_RATINGS",
                c => new
                    {
                        F_ID = c.Int(nullable: false, identity: true),
                        F_RECORD_ID = c.Int(nullable: false),
                        F_TIME = c.DateTime(nullable: false),
                        F_VALUE = c.Int(nullable: false),
                        F_COMMENT = c.String(),
                        F_USER_ID = c.Int(nullable: false),
                        F_USER_NAME = c.String(),
                    })
                .PrimaryKey(t => t.F_ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.T_RATINGS");
        }
    }
}
