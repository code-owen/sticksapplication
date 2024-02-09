using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SticksApplication.Models
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }

        //A Tag can be on many BlosPosts
        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}