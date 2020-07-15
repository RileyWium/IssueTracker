namespace IssueTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_user_fields : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.IssueModel", newName: "IssueModels");
            RenameTable(name: "dbo.UserModel", newName: "UserModels");
            RenameTable(name: "dbo.ProjectModel", newName: "ProjectModels");
            DropForeignKey("dbo.ProjectModelUserModel", "ProjectID", "dbo.ProjectModel");
            DropForeignKey("dbo.ProjectModelUserModel", "UserID", "dbo.UserModel");
            DropIndex("dbo.ProjectModelUserModel", new[] { "ProjectID" });
            DropIndex("dbo.ProjectModelUserModel", new[] { "UserID" });
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
            
            AddColumn("dbo.UserModels", "ProjectModel_ProjectID", c => c.Int());
            AddColumn("dbo.ProjectModels", "UserModel_ID1", c => c.Int());
            CreateIndex("dbo.UserModels", "ProjectModel_ProjectID");
            CreateIndex("dbo.ProjectModels", "UserModel_ID1");
            AddForeignKey("dbo.UserModels", "ProjectModel_ProjectID", "dbo.ProjectModels", "ProjectID");
            AddForeignKey("dbo.ProjectModels", "UserModel_ID1", "dbo.UserModels", "ID");
            DropTable("dbo.ProjectModelUserModel");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProjectModelUserModel",
                c => new
                    {
                        ProjectID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProjectID, t.UserID });
            
            DropForeignKey("dbo.AspNetUsers", "User_ID", "dbo.UserModels");
            DropForeignKey("dbo.ProjectModels", "UserModel_ID1", "dbo.UserModels");
            DropForeignKey("dbo.UserModels", "ProjectModel_ProjectID", "dbo.ProjectModels");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.ProjectModels", new[] { "UserModel_ID1" });
            DropIndex("dbo.UserModels", new[] { "ProjectModel_ProjectID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "User_ID" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropColumn("dbo.ProjectModels", "UserModel_ID1");
            DropColumn("dbo.UserModels", "ProjectModel_ProjectID");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            CreateIndex("dbo.ProjectModelUserModel", "UserID");
            CreateIndex("dbo.ProjectModelUserModel", "ProjectID");
            AddForeignKey("dbo.ProjectModelUserModel", "UserID", "dbo.UserModel", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ProjectModelUserModel", "ProjectID", "dbo.ProjectModel", "ProjectID", cascadeDelete: true);
            RenameTable(name: "dbo.ProjectModels", newName: "ProjectModel");
            RenameTable(name: "dbo.UserModels", newName: "UserModel");
            RenameTable(name: "dbo.IssueModels", newName: "IssueModel");
        }
    }
}
