namespace FittyCent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProfiles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TrainerClasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 512),
                        Summary = c.String(),
                        Type = c.String(nullable: false),
                        Keywords = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        ClassLimit = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        VenueType = c.Int(nullable: false),
                        Audience = c.Int(nullable: false),
                        Class_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TrainerClasses", t => t.Class_Id)
                .Index(t => t.Class_Id);
            
            AddColumn("dbo.UserAccounts", "TrainerProfile_CompanyWebsite", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sessions", "Class_Id", "dbo.TrainerClasses");
            DropIndex("dbo.Sessions", new[] { "Class_Id" });
            DropColumn("dbo.UserAccounts", "TrainerProfile_CompanyWebsite");
            DropTable("dbo.Sessions");
            DropTable("dbo.TrainerClasses");
        }
    }
}
