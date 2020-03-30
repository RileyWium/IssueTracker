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
            var users = new List<UserModel>
            {
                new UserModel {ID = 1111, UserName = "Riley"},
                new UserModel {ID = 2222, UserName = "Ray"},
                new UserModel {ID = 3333, UserName = "Tina"},
                new UserModel {ID = 4444, UserName = "Sarah"}
            };
            users.ForEach(s => context.Users.AddOrUpdate(p => p.ID, s));
            context.SaveChanges();

            var projects = new List<ProjectModel>
            {
                new ProjectModel {CreatorID = users.Single(s => s.UserName =="Riley").ID,
                    ProjName = "ProjRi1"
                },
                new ProjectModel {CreatorID = users.Single(s => s.UserName =="Riley").ID,
                    ProjName = "ProjRi2"
                },
                new ProjectModel {CreatorID = users.Single(s => s.UserName =="Ray").ID,
                    ProjName = "ProjRay1"
                },
                new ProjectModel {CreatorID = users.Single(s => s.UserName =="Ray").ID,
                    ProjName = "ProjRay2"
                },
                new ProjectModel {CreatorID = users.Single(s => s.UserName =="Tina").ID,
                    ProjName = "ProjT1"
                }
            };
            projects.ForEach(s => context.Projects.AddOrUpdate(p => p.ID, s));
            context.SaveChanges();

            var issues = new List<IssueModel>
            {
                new IssueModel {ProjID = projects.Single(s => s.ProjName =="ProjRi1").ID,
                    IssName = "PRi1I1", CreationDate = DateTime.Parse("2005-05-05"),
                    IssDescription ="Alexander", IssStatus = Status.Open, IssPriority = Priority.Medium
                },
                new IssueModel {ProjID = projects.Single(s => s.ProjName =="ProjRi1").ID,
                    IssName = "PRi1I2", CreationDate = DateTime.Parse("2005-05-05"),
                    IssDescription ="Alexander", IssStatus = Status.Open, IssPriority = Priority.Medium
                },
                new IssueModel {ProjID = projects.Single(s => s.ProjName =="ProjRi2").ID,
                    IssName = "PRi2I1", CreationDate = DateTime.Parse("2005-05-05"),
                    IssDescription ="Alexander", IssStatus = Status.Open, IssPriority = Priority.Medium
                },
                new IssueModel {ProjID = projects.Single(s => s.ProjName =="ProjRi1").ID,
                    IssName = "PRi1I3", CreationDate = DateTime.Parse("2005-05-05"),
                    IssDescription ="Alexander",  IssStatus = Status.Open, IssPriority = Priority.Medium
                },
                new IssueModel {ProjID = projects.Single(s => s.ProjName =="ProjRay1").ID,
                    IssName = "PRay1I1", CreationDate = DateTime.Parse("2005-05-05"),
                    IssDescription ="Alexander", IssStatus = Status.Open, IssPriority = Priority.Medium
                },
                new IssueModel {ProjID = projects.Single(s => s.ProjName =="ProjRay2").ID,
                    IssName = "PRay2I1", CreationDate = DateTime.Parse("2005-05-05"),
                    IssDescription ="Alexander",  IssStatus = Status.Open, IssPriority = Priority.Medium
                }
            };
            issues.ForEach(s => context.Issues.AddOrUpdate(p => p.ID, s));
            context.SaveChanges();
        }

    }
}
