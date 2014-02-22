namespace FittyCent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRelationshipsToClasses : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TrainerClasses", "User_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.TrainerClasses", "User_Id");
            AddForeignKey("dbo.TrainerClasses", "User_Id", "dbo.UserAccounts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrainerClasses", "User_Id", "dbo.UserAccounts");
            DropIndex("dbo.TrainerClasses", new[] { "User_Id" });
            DropColumn("dbo.TrainerClasses", "User_Id");
        }
    }
}
