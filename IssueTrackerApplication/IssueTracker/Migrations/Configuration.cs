namespace IssueTracker.Migrations
{
    using IssueTracker.Models;
    using IssueTracker.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WitContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WitContext context)
        {
            var users = new List<UserModel>
            {
                new UserModel{UserName ="Riley"},
                new UserModel{UserName = "Sarah"}
            };
            users.ForEach(u => context.Users.AddOrUpdate(x => x.UserName, u));
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
                IssDescription="Ri", IssAssignee = users.Single(u =>u.ID ==1),
                IssReporter = users.Single(u =>u.ID ==1),
                IssStatus=Status.Open, IssPriority=Priority.Medium}
            };
            issues.ForEach(i => context.Issues.AddOrUpdate(x => x.IssName, i));
            context.SaveChanges();

        }
        void AddOrUpdateUser(WitContext context, string projectName, string userName)
        {
            var prj = context.Projects.SingleOrDefault(p => p.ProjName == projectName);
            var usr = prj.Users.SingleOrDefault(u => u.UserName == userName);
            if (usr == null)
            {
                prj.Users.Add(context.Users.Single(u => u.UserName == userName));
            }
        }
    }
}
