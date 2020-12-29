using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IssueTracker.Models;


namespace IssueTracker.ViewModels
{
    public class IssueCreationViewModel
    {
        public IssueModel Issue { get; set; }
        public int ProjectID { get; set; }
        public string ReporterMainName { get; set; }
        public DateTime CurrentDate { get; set; }
        public IEnumerable<SelectListItem> AssigneeUsers { get; set; }
    }
}