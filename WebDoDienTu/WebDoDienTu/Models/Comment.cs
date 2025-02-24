using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebDoDienTu.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public string AuthorId { get; set; }
        public int PostId { get; set; } 

        public virtual ApplicationUser Author { get; set; } 
        public virtual Post Post { get; set; } 
    }
}
