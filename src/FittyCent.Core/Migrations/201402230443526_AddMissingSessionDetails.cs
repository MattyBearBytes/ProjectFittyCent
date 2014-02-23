namespace FittyCent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMissingSessionDetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sessions", "Address", c => c.String(maxLength: 512));
            AddColumn("dbo.Sessions", "Postcode", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sessions", "Postcode");
            DropColumn("dbo.Sessions", "Address");
        }
    }
}
