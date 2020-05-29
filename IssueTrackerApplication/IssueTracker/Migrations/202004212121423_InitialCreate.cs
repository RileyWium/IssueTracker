namespace IssueTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
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
                        CreationDate = c.DateTime(nullable: false),
                        IssDescription = c.String(maxLength: 350),
                        IssStatus = c.Int(),
                        IssPriority = c.Int(),
                        Project_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProjectModel", t => t.Project_ID)
                .Index(t => t.Project_ID);
            
            CreateTable(
                "dbo.ProjectModel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreatorID = c.Int(nullable: false),
                        ProjName = c.String(),
                        UserModel_ID = c.Int(),
                        CreatorUser_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UserModel", t => t.UserModel_ID)
                .ForeignKey("dbo.UserModel", t => t.CreatorUser_ID)
                .Index(t => t.UserModel_ID)
                .Index(t => t.CreatorUser_ID);
            
            CreateTable(
                "dbo.UserModel",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        UserName = c.String(),
                        ProjectModel_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProjectModel", t => t.ProjectModel_ID)
                .Index(t => t.ProjectModel_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserModel", "ProjectModel_ID", "dbo.ProjectModel");
            DropForeignKey("dbo.IssueModel", "Project_ID", "dbo.ProjectModel");
            DropForeignKey("dbo.ProjectModel", "CreatorUser_ID", "dbo.UserModel");
            DropForeignKey("dbo.ProjectModel", "UserModel_ID", "dbo.UserModel");
            DropIndex("dbo.UserModel", new[] { "ProjectModel_ID" });
            DropIndex("dbo.ProjectModel", new[] { "CreatorUser_ID" });
            DropIndex("dbo.ProjectModel", new[] { "UserModel_ID" });
            DropIndex("dbo.IssueModel", new[] { "Project_ID" });
            DropTable("dbo.UserModel");
            DropTable("dbo.ProjectModel");
            DropTable("dbo.IssueModel");
        }
    }
}
