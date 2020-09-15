using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IssueTracker.Models
{
    public class IdenProjModel
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int IdenProjID { get; set; }
       
        [Required]
        [ForeignKey("Project")]
        public int ProjID { get; set; }
        [Required]
        public string UserID { get; set; }
        public string MainName { get; set; }
        public bool Master { get; set; }

        public virtual ProjectModel Project { get; set; }
    }
}