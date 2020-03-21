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
                        ProjectModel_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProjectModel", t => t.ProjectModel_ID)
                .Index(t => t.ProjectModel_ID);
            
            CreateTable(
                "dbo.ProjectModel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IssID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        ProjName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserModel",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserModelProjectModel",
                c => new
                    {
                        UserModel_ID = c.Int(nullable: false),
                        ProjectModel_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserModel_ID, t.ProjectModel_ID })
                .ForeignKey("dbo.UserModel", t => t.UserModel_ID, cascadeDelete: true)
                .ForeignKey("dbo.ProjectModel", t => t.ProjectModel_ID, cascadeDelete: true)
                .Index(t => t.UserModel_ID)
                .Index(t => t.ProjectModel_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserModelProjectModel", "ProjectModel_ID", "dbo.ProjectModel");
            DropForeignKey("dbo.UserModelProjectModel", "UserModel_ID", "dbo.UserModel");
            DropForeignKey("dbo.IssueModel", "ProjectModel_ID", "dbo.ProjectModel");
            DropIndex("dbo.UserModelProjectModel", new[] { "ProjectModel_ID" });
            DropIndex("dbo.UserModelProjectModel", new[] { "UserModel_ID" });
            DropIndex("dbo.IssueModel", new[] { "ProjectModel_ID" });
            DropTable("dbo.UserModelProjectModel");
            DropTable("dbo.UserModel");
            DropTable("dbo.ProjectModel");
            DropTable("dbo.IssueModel");
        }
    }
}
