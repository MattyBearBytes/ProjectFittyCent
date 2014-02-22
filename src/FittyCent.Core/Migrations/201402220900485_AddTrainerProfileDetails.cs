namespace FittyCent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTrainerProfileDetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserAccounts", "FirstName", c => c.String(maxLength: 255));
            AddColumn("dbo.UserAccounts", "Surname", c => c.String(maxLength: 255));
            AddColumn("dbo.UserAccounts", "Postcode", c => c.String(maxLength: 20));
            AddColumn("dbo.UserAccounts", "TrainerProfile_Summary", c => c.String());
            AddColumn("dbo.UserAccounts", "TrainerProfile_CompanyName", c => c.String(maxLength: 255));
            AddColumn("dbo.UserAccounts", "TrainerProfile_IsInsured", c => c.Boolean());
            AddColumn("dbo.UserAccounts", "TrainerProfile_Registrations", c => c.String());
            AddColumn("dbo.UserAccounts", "TrainerProfile_Specialisations", c => c.String());
            AddColumn("dbo.UserAccounts", "TrainerProfile_Qualifications", c => c.String());
            AddColumn("dbo.UserAccounts", "TrainerProfile_HasMobileServiceAvailable", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserAccounts", "TrainerProfile_HasMobileServiceAvailable");
            DropColumn("dbo.UserAccounts", "TrainerProfile_Qualifications");
            DropColumn("dbo.UserAccounts", "TrainerProfile_Specialisations");
            DropColumn("dbo.UserAccounts", "TrainerProfile_Registrations");
            DropColumn("dbo.UserAccounts", "TrainerProfile_IsInsured");
            DropColumn("dbo.UserAccounts", "TrainerProfile_CompanyName");
            DropColumn("dbo.UserAccounts", "TrainerProfile_Summary");
            DropColumn("dbo.UserAccounts", "Postcode");
            DropColumn("dbo.UserAccounts", "Surname");
            DropColumn("dbo.UserAccounts", "FirstName");
        }
    }
}
