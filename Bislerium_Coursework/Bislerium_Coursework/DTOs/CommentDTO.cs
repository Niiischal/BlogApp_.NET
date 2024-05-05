using System.ComponentModel.DataAnnotations.Schema;

namespace Bislerium_Coursework.DTOs
{
    public class CommentDTO
    {

        public string Content { get; set; }

        public int BlogId { get; set; }
    }
}
