
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Key]
        public int ID { get; set; }
        [ForeignKey("Project")]
        //[Range(100000, 999999, ErrorMessage = "Need valid Project ID of six numbers.")]
        public int ProjID { get; set; }
        [Display(Name = "Issue Name")]
        [StringLength(50, ErrorMessage = "The {0} cannot exceed {1} characters. ", MinimumLength = 1)]
        [Required(ErrorMessage = "Issue Name required.")]
        public string IssName { get; set; }
        [Required]
        [Display(Name = "Report Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReportDate { get; set; }
        [Display(Name = "Issue Description")]
        [StringLength(350, ErrorMessage = "The {0} cannot exceed {1} characters. ")]
        public string IssDescription { get; set; }
        [Display(Name = "Assignee")]
        public UserModel IssAssignee { get; set; }
        [Required]
        [Display(Name = "Reporter")]
        public UserModel IssReporter { get; set; }
        //question mark after Status(?) means its nullable
        [Required]
        [Display(Name = "Status")]
        public Status IssStatus { get; set; }
        [Required]
        [Display(Name = "Priority")]
        public Priority IssPriority { get; set; }
        public virtual ProjectModel Project { get; set; }
    }
}