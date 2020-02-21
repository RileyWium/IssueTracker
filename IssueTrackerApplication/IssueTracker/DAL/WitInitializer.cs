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
            var issues = new List<IssueModel>
            {
            new IssueModel{ProjID=121212,IssName="Issue1",IssDescription="Alexander",CreationDate=DateTime.Parse("2005-09-01")},
            new IssueModel{ProjID=121217,IssName="Issue2",IssDescription="Alex",CreationDate=DateTime.Parse("2005-09-01")},
            new IssueModel{ProjID=121216,IssName="Issue3",IssDescription="Alexa",CreationDate=DateTime.Parse("2005-09-01")},
            new IssueModel{ProjID=121215,IssName="Issue4",IssDescription="Xander",CreationDate=DateTime.Parse("2005-09-01")},
            new IssueModel{ProjID=121214,IssName="Issue5",IssDescription="Lex",CreationDate=DateTime.Parse("2005-09-01")},
            new IssueModel{ProjID=121213,IssName="Issue6",IssDescription="Lexa",CreationDate=DateTime.Parse("2005-09-01")},
            };
            issues.ForEach(i => context.Issues.Add(i));
            context.SaveChanges();
            var users = new List<UserModel>
            {
            new UserModel{ID=105022,UserName="Riley"},
            new UserModel{ID=402222,UserName="Ray"},
            new UserModel{ID=404122,UserName="Tina"},
            new UserModel{ID=104522,UserName="Sarah"}
            };
            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();
            var projects = new List<ProjectModel>
            {
            new ProjectModel{IssID=134567,UserID=105022,ProjName="boi"},
            new ProjectModel{IssID=134345,UserID=402222,ProjName="koi"},
            new ProjectModel{IssID=145456,UserID=404122,ProjName="doi"},
            new ProjectModel{IssID=253534,UserID=104522,ProjName="soi"},
            new ProjectModel{IssID=245345,UserID=314122,ProjName="roi"}
            };
            projects.ForEach(p => context.Projects.Add(p));
            context.SaveChanges();
        }
    }
}