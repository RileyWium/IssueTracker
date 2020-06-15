using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssueTracker.ViewModels
{
    public class AssignedProjectData
    {
        public int ProjectID { get; set; }
        public string Name { get; set; }
        public bool Assigned { get; set; }
    }
}