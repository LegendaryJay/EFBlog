using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFBlog.Models
{
    public class Blog
    {
       [Key]
       public int BlogId { get; set; }
        public string Name { get; set; }
        
        public virtual List<Post> Posts { get; set; }
    }
}