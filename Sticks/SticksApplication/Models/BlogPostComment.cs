using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SticksApplication.Models
{
    public class BlogPostComment
    {
        [Key]
        public int CommentId { get; set; }

        public string Description { get; set; }

        //A Comment can be on one blog
        public int BlogPostId { get; set; }

        public DateTime DateAdded { get; set; }
    }
}