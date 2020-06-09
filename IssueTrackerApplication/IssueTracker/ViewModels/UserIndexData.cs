using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IssueTracker.Models;


namespace IssueTracker.ViewModels
{
    public class UserIndexData
    {
        public IEnumerable<UserModel> Users { get; set; }
        public IEnumerable<ProjectModel> Projects { get; set; }
        public IEnumerable<IssueModel> Issues { get; set; }
    }
}