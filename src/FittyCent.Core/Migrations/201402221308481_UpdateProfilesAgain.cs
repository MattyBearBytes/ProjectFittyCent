namespace FittyCent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProfilesAgain : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sessions", "Class_Id", "dbo.TrainerClasses");
            DropIndex("dbo.Sessions", new[] { "Class_Id" });
            AddColumn("dbo.Sessions", "TrainerClass_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Sessions", "TrainerClass_Id");
            AddForeignKey("dbo.Sessions", "TrainerClass_Id", "dbo.TrainerClasses", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sessions", "TrainerClass_Id", "dbo.TrainerClasses");
            DropIndex("dbo.Sessions", new[] { "TrainerClass_Id" });
            DropColumn("dbo.Sessions", "TrainerClass_Id");
            CreateIndex("dbo.Sessions", "Class_Id");
            AddForeignKey("dbo.Sessions", "Class_Id", "dbo.TrainerClasses", "Id");
        }
    }
}
