﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IssueTracker.Models
{
    public class ProjectModel
    {
        
        //[DisplayAttribute(Name = "Project ID")]
        //[Range(100000,999999, ErrorMessage = "Need valid Project ID of six numbers.")]
        [Key]
        public int ProjectID { get; set; }


        [Display(Name ="Project Name")]
        [StringLength(60, ErrorMessage = "The {0} cannot exceed {1} characters. ", MinimumLength =1)]
        [Required(ErrorMessage ="Need a name for the Project.")]
        public string ProjName { get; set; }        
       
        public virtual ICollection<UserModel> Users { get; set; }
        public virtual ICollection<IssueModel> Issues { get; set; }
    }
}