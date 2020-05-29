namespace IssueTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NonNullPriorityAndStatus : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.IssueModel", "IssStatus", c => c.Int(nullable: false));
            AlterColumn("dbo.IssueModel", "IssPriority", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.IssueModel", "IssPriority", c => c.Int());
            AlterColumn("dbo.IssueModel", "IssStatus", c => c.Int());
        }
    }
}
