using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SticksApplication.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        public string Description { get; set; }

        // Foreign key property
        public int BlogPostId { get; set; }

        public DateTime DateAdded { get; set; }
    }
}