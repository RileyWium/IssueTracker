using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IssueTracker.Models
{

    public enum Status
    {
        Open, Closed
    }
    public enum Priority
    {
        Low, Medium, High
    }
    public class IssueModel
    {
        [DisplayAttribute(Name = "Issue ID")]
        public int ID { get; set; }

        [DisplayAttribute(Name = "Project ID")]
        //[Range(100000, 999999, ErrorMessage = "Need valid Project ID of six numbers.")]
        public int ProjID { get; set; }

        [Display(Name ="Issue Name")]
        [StringLength(50, ErrorMessage ="The {0} cannot exceed {1} characters. ", MinimumLength =1)]
        [Required(ErrorMessage ="Issue Name required.")]
        public string IssName { get; set; }

        [Display(Name ="Creation Date")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Issue Description")]
        [StringLength(350, ErrorMessage = "The {0} cannot exceed {1} characters. ")]
        public string IssDescription { get; set; }

        //public string IssueAssignee { get; set; } put this in later
        //public string IssueReporter { get; set; } auto set
        //question mark after Status(?) means its nullable
        public Status? IssStatus { get; set; }//probably shouldn't be nullable
        public Priority? IssPriority { get; set; }

        public virtual ProjectModel Project { get; set; }
    }
}