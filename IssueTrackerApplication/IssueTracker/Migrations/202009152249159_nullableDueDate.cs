namespace IssueTracker.Migrations.WitConfiguration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullableDueDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.IssueModel", "DueDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.IssueModel", "DueDate", c => c.DateTime(nullable: false));
        }
    }
}
