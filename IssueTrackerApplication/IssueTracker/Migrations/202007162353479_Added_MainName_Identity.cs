namespace IssueTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_MainName_Identity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "MainName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.UserModels", "MainName", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.UserModels", "UserName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserModels", "UserName", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.UserModels", "MainName");
            DropColumn("dbo.AspNetUsers", "MainName");
        }
    }
}
