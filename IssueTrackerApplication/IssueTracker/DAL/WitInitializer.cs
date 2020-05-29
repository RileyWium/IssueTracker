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
            new UserModel{ID=1111,UserName="Riley"},
            new UserModel{ID=2222,UserName="Ray"},
            new UserModel{ID=3333,UserName="Tina"},
            new UserModel{ID=4444,UserName="Sarah"}
            };
            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();
            var projects = new List<ProjectModel>
            {
            new ProjectModel{CreatorID=1111,ProjName="ProjRi1"},
            new ProjectModel{CreatorID=1111,ProjName="ProjRi2"},
            new ProjectModel{CreatorID=2222,ProjName="ProjRay1"},
            new ProjectModel{CreatorID=2222,ProjName="ProjRay2"},
            new ProjectModel{CreatorID=3333,ProjName="ProjT1"}
            };
            projects.ForEach(p => context.Projects.Add(p));
            context.SaveChanges();
            var issues = new List<IssueModel>
            {
            new IssueModel{ProjID=1,IssName="PRi1I1",IssDescription="Alexander",
                CreationDate=DateTime.Parse("2005-05-05"),IssStatus=Status.Open,IssPriority=Priority.Medium},
            new IssueModel{ProjID=1,IssName="PRi1I2",IssDescription="Alexander",
                CreationDate=DateTime.Parse("2005-05-05"),IssStatus=Status.Open,IssPriority=Priority.Medium},
            new IssueModel{ProjID=2,IssName="PRi2I1",IssDescription="Alexander",
                CreationDate=DateTime.Parse("2005-05-05"),IssStatus=Status.Open,IssPriority=Priority.Medium},
            new IssueModel{ProjID=1,IssName="PRi1I3",IssDescription="Alexander",
                CreationDate=DateTime.Parse("2005-05-05"),IssStatus=Status.Open,IssPriority=Priority.Medium},
            new IssueModel{ProjID=3,IssName="PRay1I1",IssDescription="Alexander",
                CreationDate=DateTime.Parse("2005-05-05"),IssStatus=Status.Open,IssPriority=Priority.Medium},
            new IssueModel{ProjID=4,IssName="PRay2I1",IssDescription="Alexander",
                CreationDate=DateTime.Parse("2005-05-05"),IssStatus=Status.Open,IssPriority=Priority.Medium}
            };
            issues.ForEach(i => context.Issues.Add(i));
            context.SaveChanges();            
        }
    }
}