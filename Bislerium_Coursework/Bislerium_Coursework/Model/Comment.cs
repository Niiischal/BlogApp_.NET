using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bislerium_Coursework.Model
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        public string Content { get; set; }

        // Foreign key to Blog table
        [ForeignKey("Blog")]
        public int BlogId { get; set; }

        // Navigation property to access the blog associated with this comment
        public virtual Blog Blog { get; set; }

        public DateTime CommentPostedDate = DateTime.Now;

       
    }
}
