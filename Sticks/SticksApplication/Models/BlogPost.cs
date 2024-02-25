using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SticksApplication.Models
{
    public class BlogPost
    {
        [Key]
        public int BlogPostId { get; set; }

        public string Heading { get; set; }

        public string PageTitle { get; set; }

        public string Content { get; set; }

        public string ShortDescription { get; set; }

        public string FeaturedImageUrl { get; set; }

        public string UrlHandle { get; set; }

        public DateTime PublishedDate { get; set; }

        public string Author { get; set; }

        public bool Visible { get; set; }

        //A BlogPost can have many Tags
        public ICollection<Tag> Tags { get; set; }

        //A BlogPost can have many Comments
        public ICollection<Comment> Comments { get; set; }
    }

    public class BlogPostDto
    {
        public int BlogPostId { get; set; }

        public string Heading { get; set; }

        public string Content { get; set; }

        public string ShortDescription { get; set; }

        public string Author { get; set; }

        public string FeaturedImageUrl { get; set; }

        public DateTime PublishedDate { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}