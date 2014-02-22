namespace FittyCent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserAccount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserAccounts", "Email", c => c.String(nullable: false, maxLength: 512));
            DropColumn("dbo.UserAccounts", "FirstName");
            DropColumn("dbo.UserAccounts", "Surname");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserAccounts", "Surname", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.UserAccounts", "FirstName", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.UserAccounts", "Email");
        }
    }
}
