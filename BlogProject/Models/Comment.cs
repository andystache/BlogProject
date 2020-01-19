using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogProject.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int BlogPostID { get; set; }
        public string AuthorId { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        [Display(Name="Comment Body")]
        [AllowHtml]
        public string CommentBody { get; set; }
        public string UpdateReason { get; set; }
        
        //Virtual Navigation Section
        public virtual BlogPost BlogPost { get; set; }
        public virtual ApplicationUser Author { get; set; }
    }
}