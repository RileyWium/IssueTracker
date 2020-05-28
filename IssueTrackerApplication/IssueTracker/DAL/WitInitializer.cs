using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using IssueTracker.Models;

namespace IssueTracker.DAL
{
    public class WitInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<WitContext>
    {
        protected override void Seed(WitContext context)
        {
            var users = new List<UserModel>
            {
            new UserModel{UserName="Riley"},
            new UserModel{UserName="Sarah"}
            };
            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();
            var projects = new List<ProjectModel>
            {
            new ProjectModel{ProjName="PrjR1"},
            new ProjectModel{ProjName="PrjR2"},
            new ProjectModel{ProjName="PrjS1"}
            };
            projects.ForEach(p => context.Projects.Add(p));
            context.SaveChanges();
            var issues = new List<IssueModel>
            {
            new IssueModel{ProjID=1,IssName="PR1I1",ReportDate=DateTime.Parse("2005-05-05"),
                IssDescription="Ri",IssReporter = users.Single(u => u.ID == 1),
                IssStatus=Status.Open,IssPriority=Priority.Medium
                },                
            new IssueModel{ProjID=2,IssName="PR2I1",ReportDate=DateTime.Parse("2005-05-05"),
                IssDescription="Ri",IssReporter = users.Single(u => u.ID == 1),
                IssStatus=Status.Open,IssPriority=Priority.Medium
                },
            new IssueModel{ProjID=3,IssName="PS1I1",ReportDate=DateTime.Parse("2005-05-05"),
                IssDescription="Sa",IssReporter = users.Single(u => u.ID == 1),
                IssStatus=Status.Open,IssPriority=Priority.Medium
                },
            new IssueModel{ProjID=3,IssName="PS1I2",ReportDate=DateTime.Parse("2005-05-05"),
                IssDescription="Sa",IssReporter = users.Single(u => u.ID == 1),
                IssStatus=Status.Open,IssPriority=Priority.Medium
                },
            };
            issues.ForEach(i => context.Issues.Add(i));
            context.SaveChanges();            
        }
    }
}