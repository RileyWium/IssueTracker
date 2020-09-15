namespace IssueTracker.Migrations.WitConfiguration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class twoContextReset : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IdenProjModel",
                c => new
                    {
                        IdenProjID = c.Int(nullable: false, identity: true),
                        ProjID = c.Int(nullable: false),
                        UserID = c.String(nullable: false),
                        MainName = c.String(),
                        Master = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdenProjID)
                .ForeignKey("dbo.ProjectModel", t => t.ProjID, cascadeDelete: true)
                .Index(t => t.ProjID);
            
            CreateTable(
                "dbo.ProjectModel",
                c => new
                    {
                        ProjectID = c.Int(nullable: false, identity: true),
                        ProjName = c.String(nullable: false, maxLength: 60),
                    })
                .PrimaryKey(t => t.ProjectID);
            
            CreateTable(
                "dbo.IssueModel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProjID = c.Int(nullable: false),
                        IssName = c.String(nullable: false, maxLength: 50),
                        ReportDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        IssDescription = c.String(maxLength: 350),
                        IssAssigneeName = c.String(),
                        IssReporterName = c.String(nullable: false),
                        IssStatus = c.Int(nullable: false),
                        IssPriority = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProjectModel", t => t.ProjID, cascadeDelete: true)
                .Index(t => t.ProjID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdenProjModel", "ProjID", "dbo.ProjectModel");
            DropForeignKey("dbo.IssueModel", "ProjID", "dbo.ProjectModel");
            DropIndex("dbo.IssueModel", new[] { "ProjID" });
            DropIndex("dbo.IdenProjModel", new[] { "ProjID" });
            DropTable("dbo.IssueModel");
            DropTable("dbo.ProjectModel");
            DropTable("dbo.IdenProjModel");
        }
    }
}
