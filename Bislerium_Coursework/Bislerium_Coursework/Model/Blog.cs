using System.ComponentModel.DataAnnotations;

namespace Bislerium_Coursework.Model
{
    public class Blog
    {
        [Key]
        public int BlogId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ImageURL { get; set; }

        public DateTime CreationDate = DateTime.Now;

        public string AuthorID { get; set; }
       
    }
}
