using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class IssueModel
    {
        public int IssueID { get; set; }
        public int ProjectID { get; set; }
        public string IssueName { get; set; }
        // public int IssueAssignee { get; set; }
        // public int IssueReporter { get; set; }
        // public int IssueStatus { get; set; }
        // public int IssuePriority { get; set; }
        public string IssueDescription { get; set; }
    }
}
