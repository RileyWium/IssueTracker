using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IssueTracker.Models
{
    public class UserModel
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        [Required(ErrorMessage = "User Name required.")]
        [Display(Name = "User Name")]
        [StringLength(50, ErrorMessage = "The {0} cannot exceed {1} characters. ", MinimumLength = 1)]
        public string UserName { get; set; }
        public virtual ICollection<ProjectModel> Projects { get; set; }

        public virtual ICollection<ProjectModel> MasterPermProjects { get; set; }
    }
}