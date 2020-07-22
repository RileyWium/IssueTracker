namespace IssueTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserModels", t => t.User_ID)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IdentityID = c.String(),
                        UserName = c.String(nullable: false, maxLength: 50),
                        ProjectModel_ProjectID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProjectModels", t => t.ProjectModel_ProjectID)
                .Index(t => t.ProjectModel_ProjectID);
            
            CreateTable(
                "dbo.ProjectModels",
                c => new
                    {
                        ProjectID = c.Int(nullable: false, identity: true),
                        ProjName = c.String(nullable: false, maxLength: 60),
                        UserModel_ID = c.Int(),
                        UserModel_ID1 = c.Int(),
                    })
                .PrimaryKey(t => t.ProjectID)
                .ForeignKey("dbo.UserModels", t => t.UserModel_ID)
                .ForeignKey("dbo.UserModels", t => t.UserModel_ID1)
                .Index(t => t.UserModel_ID)
                .Index(t => t.UserModel_ID1);
            
            CreateTable(
                "dbo.IssueModels",
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
                .ForeignKey("dbo.UserModels", t => t.IssAssignee_ID)
                .ForeignKey("dbo.UserModels", t => t.IssReporter_ID, cascadeDelete: true)
                .ForeignKey("dbo.ProjectModels", t => t.ProjID, cascadeDelete: true)
                .Index(t => t.ProjID)
                .Index(t => t.IssAssignee_ID)
                .Index(t => t.IssReporter_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "User_ID", "dbo.UserModels");
            DropForeignKey("dbo.ProjectModels", "UserModel_ID1", "dbo.UserModels");
            DropForeignKey("dbo.ProjectModels", "UserModel_ID", "dbo.UserModels");
            DropForeignKey("dbo.UserModels", "ProjectModel_ProjectID", "dbo.ProjectModels");
            DropForeignKey("dbo.IssueModels", "ProjID", "dbo.ProjectModels");
            DropForeignKey("dbo.IssueModels", "IssReporter_ID", "dbo.UserModels");
            DropForeignKey("dbo.IssueModels", "IssAssignee_ID", "dbo.UserModels");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.IssueModels", new[] { "IssReporter_ID" });
            DropIndex("dbo.IssueModels", new[] { "IssAssignee_ID" });
            DropIndex("dbo.IssueModels", new[] { "ProjID" });
            DropIndex("dbo.ProjectModels", new[] { "UserModel_ID1" });
            DropIndex("dbo.ProjectModels", new[] { "UserModel_ID" });
            DropIndex("dbo.UserModels", new[] { "ProjectModel_ProjectID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "User_ID" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.IssueModels");
            DropTable("dbo.ProjectModels");
            DropTable("dbo.UserModels");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
