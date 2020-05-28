namespace IssueTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IssueModel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProjID = c.Int(nullable: false),
                        IssName = c.String(nullable: false, maxLength: 50),
                        ReportDate = c.DateTime(nullable: false),
                        IssDescription = c.String(maxLength: 350),
                        IssStatus = c.Int(nullable: false),
                        IssPriority = c.Int(nullable: false),
                        IssAssignee_ID = c.Int(),
                        IssReporter_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UserModel", t => t.IssAssignee_ID)
                .ForeignKey("dbo.UserModel", t => t.IssReporter_ID, cascadeDelete: true)
                .ForeignKey("dbo.ProjectModel", t => t.ProjID, cascadeDelete: true)
                .Index(t => t.ProjID)
                .Index(t => t.IssAssignee_ID)
                .Index(t => t.IssReporter_ID);
            
            CreateTable(
                "dbo.UserModel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProjectModel",
                c => new
                    {
                        ProjectID = c.Int(nullable: false, identity: true),
                        ProjName = c.String(nullable: false, maxLength: 60),
                    })
                .PrimaryKey(t => t.ProjectID);
            
            CreateTable(
                "dbo.ProjectModelUserModel",
                c => new
                    {
                        ProjectID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProjectID, t.UserID })
                .ForeignKey("dbo.ProjectModel", t => t.ProjectID, cascadeDelete: true)
                .ForeignKey("dbo.UserModel", t => t.UserID, cascadeDelete: true)
                .Index(t => t.ProjectID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IssueModel", "ProjID", "dbo.ProjectModel");
            DropForeignKey("dbo.IssueModel", "IssReporter_ID", "dbo.UserModel");
            DropForeignKey("dbo.IssueModel", "IssAssignee_ID", "dbo.UserModel");
            DropForeignKey("dbo.ProjectModelUserModel", "UserID", "dbo.UserModel");
            DropForeignKey("dbo.ProjectModelUserModel", "ProjectID", "dbo.ProjectModel");
            DropIndex("dbo.ProjectModelUserModel", new[] { "UserID" });
            DropIndex("dbo.ProjectModelUserModel", new[] { "ProjectID" });
            DropIndex("dbo.IssueModel", new[] { "IssReporter_ID" });
            DropIndex("dbo.IssueModel", new[] { "IssAssignee_ID" });
            DropIndex("dbo.IssueModel", new[] { "ProjID" });
            DropTable("dbo.ProjectModelUserModel");
            DropTable("dbo.ProjectModel");
            DropTable("dbo.UserModel");
            DropTable("dbo.IssueModel");
        }
    }
}
