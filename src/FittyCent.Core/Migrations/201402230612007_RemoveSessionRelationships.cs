namespace FittyCent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveSessionRelationships : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sessions", "Class_Id", "dbo.TrainerClasses");
            DropForeignKey("dbo.Sessions", "TrainerClass_Id", "dbo.TrainerClasses");
            DropIndex("dbo.Sessions", new[] { "Class_Id" });
            DropIndex("dbo.Sessions", new[] { "TrainerClass_Id" });
            DropColumn("dbo.Sessions", "Class_Id");
            DropColumn("dbo.Sessions", "TrainerClass_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sessions", "TrainerClass_Id", c => c.Int(nullable: false));
            AddColumn("dbo.Sessions", "Class_Id", c => c.Int());
            CreateIndex("dbo.Sessions", "TrainerClass_Id");
            CreateIndex("dbo.Sessions", "Class_Id");
            AddForeignKey("dbo.Sessions", "TrainerClass_Id", "dbo.TrainerClasses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Sessions", "Class_Id", "dbo.TrainerClasses", "Id");
        }
    }
}
