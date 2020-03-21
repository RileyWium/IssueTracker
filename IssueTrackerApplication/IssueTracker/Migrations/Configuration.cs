namespace IssueTracker.Migrations
{
    using System;
    using IssueTracker.Models;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<IssueTracker.DAL.WitContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(IssueTracker.DAL.WitContext context)
        {
            var issues = new List<IssueModel>
            {
                //test data
                new IssueModel {IssName = "Workload1", 
                    CreationDate = DateTime.Parse("2020-03-03"), IssDescription = "a"},
                new IssueModel {IssName = "Workload2",
                    CreationDate = DateTime.Parse("2020-03-03"), IssDescription = "a"},
                new IssueModel {IssName = "Workload3",
                    CreationDate = DateTime.Parse("2020-03-03"), IssDescription = "a"},
                new IssueModel {IssName = "Workload4",
                    CreationDate = DateTime.Parse("2020-04-03"), IssDescription = "a"},
                new IssueModel {IssName = "Workload5",
                    CreationDate = DateTime.Parse("2020-03-03"), IssDescription = "a"},
                new IssueModel {IssName = "Workload6",
                    CreationDate = DateTime.Parse("2020-03-03"), IssDescription = "nb"},
                new IssueModel {IssName = "Workload7",
                    CreationDate = DateTime.Parse("2020-03-03"), IssDescription = ""},
            };
            issues.ForEach(i => context.Issues.AddOrUpdate(x => x.IssName, i));
            context.SaveChanges();

            var projects = new List<ProjectModel> 
            {
                new ProjectModel {IssID = issues.Single(i => i.IssName =="Workload1" ).ID,
                UserID = users.Single(u => u.UserName =="Bill").UserID,
                ProjName = "Proj1"},
                new ProjectModel {IssID = issues.Single(i => i.IssName =="Workload1" ).ID,
                UserID = users.Single(u => u.UserName =="Bill").UserID,
                ProjName = "Proj1"},
            };

            foreach(ProjectModel p in projects)
            {
                var projectInDataBase = context.Projects.Where(
                    s =>
                        s.Issues. == p.IssID)
            }
        }

    }
}
