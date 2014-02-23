namespace FittyCent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIdToSession : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sessions", "TrainerClassId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sessions", "TrainerClassId");
        }
    }
}
