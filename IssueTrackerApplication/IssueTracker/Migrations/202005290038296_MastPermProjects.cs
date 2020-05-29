namespace IssueTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MastPermProjects : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProjectModel", "UserModel_ID", c => c.Int());
            CreateIndex("dbo.ProjectModel", "UserModel_ID");
            AddForeignKey("dbo.ProjectModel", "UserModel_ID", "dbo.UserModel", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectModel", "UserModel_ID", "dbo.UserModel");
            DropIndex("dbo.ProjectModel", new[] { "UserModel_ID" });
            DropColumn("dbo.ProjectModel", "UserModel_ID");
        }
    }
}
