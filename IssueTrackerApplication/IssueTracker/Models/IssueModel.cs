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
        [DisplayAttribute(Name = "Issue ID")]
        //auto
        public int ID { get; set; }

        [DisplayAttribute(Name = "Project ID")]
        [ForeignKey("Project")]
        //[Range(100000, 999999, ErrorMessage = "Need valid Project ID of six numbers.")]
        //auto
        public int ProjID { get; set; }

        [Display(Name ="Issue Name")]
        [StringLength(50, ErrorMessage ="The {0} cannot exceed {1} characters. ", MinimumLength =1)]
        [Required(ErrorMessage ="Issue Name required.")]
        public string IssName { get; set; }

        [Display(Name ="Report Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode =true)]
        //auto
        public DateTime CreationDate { get; set; }

        [Display(Name = "Issue Description")]
        [StringLength(350, ErrorMessage = "The {0} cannot exceed {1} characters. ")]
        public string IssDescription { get; set; }
        public UserModel IssueAssignee { get; set; }
        public UserModel IssueReporter { get; set; }

        //question mark after Status(?) means its nullable
        public Status IssStatus { get; set; }
        public Priority IssPriority { get; set; }
        public virtual ProjectModel Project { get; set; }
    }
}