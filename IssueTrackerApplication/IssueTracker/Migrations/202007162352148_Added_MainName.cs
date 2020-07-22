namespace IssueTracker.Migrations.WitConfiguration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_MainName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserModel", "MainName", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.UserModel", "UserName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserModel", "UserName", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.UserModel", "MainName");
        }
    }
}
