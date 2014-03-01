namespace FittyCent.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserAccounts", "BirthYear", c => c.Int(nullable: false));
            AddColumn("dbo.UserAccounts", "Gender", c => c.Int(nullable: false));
            AlterColumn("dbo.UserAccounts", "Postcode", c => c.String(maxLength: 4));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserAccounts", "Postcode", c => c.String(maxLength: 20));
            DropColumn("dbo.UserAccounts", "Gender");
            DropColumn("dbo.UserAccounts", "BirthYear");
        }
    }
}
