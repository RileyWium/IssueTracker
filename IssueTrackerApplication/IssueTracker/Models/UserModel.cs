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
        [Key]
        //auto
        public int ID { get; set; }
        [Required]
        [Display(Name = "User Name")]
        [StringLength(50)]
        public string UserName { get; set; }
        public virtual ICollection<ProjectModel> Projects { get; set; }
    }
}