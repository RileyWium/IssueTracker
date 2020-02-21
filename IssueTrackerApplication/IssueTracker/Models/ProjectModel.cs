using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IssueTracker.Models
{
    public class ProjectModel
    {
        
        //[DisplayAttribute(Name = "Project ID")]
        //[Range(100000,999999, ErrorMessage = "Need valid Project ID of six numbers.")]
        public int ID { get; set; }
        public int IssID { get; set; }
        public int UserID { get; set; }

        //[Display(Name ="Project Name")]
        //[StringLength(50, ErrorMessage = "The {0} cannot exceed {1} characters. ", MinimumLength =1)]
        //[Required(ErrorMessage ="You need a name for the Project.")]
        public string ProjName { get; set; }        
        public virtual ICollection<UserModel> Users { get; set; }
        public virtual ICollection<IssueModel> Issues { get; set; }
    }
}