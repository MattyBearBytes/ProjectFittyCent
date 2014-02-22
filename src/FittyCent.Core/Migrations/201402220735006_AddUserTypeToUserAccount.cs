namespace FittyCent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserTypeToUserAccount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserAccounts", "UserType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserAccounts", "UserType");
        }
    }
}
