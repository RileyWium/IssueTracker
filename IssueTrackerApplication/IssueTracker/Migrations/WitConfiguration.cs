namespace IssueTracker.Migrations.WitConfiguration
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using IssueTracker.Models;
    using IssueTracker.DAL;
    using System.Collections.Generic;
    internal sealed class WitConfiguration : DbMigrationsConfiguration<IssueTracker.DAL.WitContext>
    {
        public WitConfiguration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(IssueTracker.DAL.WitContext context)
        {
            var users = new List<UserModel>
            {
                new UserModel{MainName ="Riley"},
                new UserModel{MainName = "Sarah"}
            };
            users.ForEach(u => context.Users.AddOrUpdate(x => x.MainName, u));
            context.SaveChanges();

            var projects = new List<ProjectModel>
            {
                new ProjectModel{ProjName="PrjR1", Users = new List<UserModel>()}
            };
            projects.ForEach(p => context.Projects.AddOrUpdate(x => x.ProjName, p));
            context.SaveChanges();

            AddOrUpdateUser(context, "PrjR1", "Riley");
            context.SaveChanges();

            var issues = new List<IssueModel>
            {
                new IssueModel{ProjID=1, IssName ="PR1I1", ReportDate = DateTime.Parse("2004-03-02"),
                IssDescription="Ri", IssAssignee = users.Single(u =>u.MainName =="Riley"),
                IssReporter = users.Single(u =>u.MainName =="Riley"),
                IssStatus=Status.Open, IssPriority=Priority.Medium}
            };
            issues.ForEach(i => context.Issues.AddOrUpdate(x => x.IssName, i));
            context.SaveChanges();
        }
        void AddOrUpdateUser(WitContext context, string projectName, string userName)
        {
            var prj = context.Projects.SingleOrDefault(p => p.ProjName == projectName);
            var usr = prj.Users.SingleOrDefault(u => u.MainName == userName);
            if (usr == null)
            {
                prj.Users.Add(context.Users.Single(u => u.MainName == userName));
            }
        }
    }
}
