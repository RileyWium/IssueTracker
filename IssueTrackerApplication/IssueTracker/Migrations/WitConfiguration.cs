namespace IssueTracker.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class WitConfiguration : DbMigrationsConfiguration<IssueTracker.DAL.WitContext>
    {
        public WitConfiguration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(IssueTracker.DAL.WitContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
