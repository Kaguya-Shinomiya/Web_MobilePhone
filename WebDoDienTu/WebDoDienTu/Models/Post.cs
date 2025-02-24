using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDoDienTu.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        [Required(ErrorMessage = "Tiêu đề bài viết là bắt buộc.")]
        [StringLength(200, ErrorMessage = "Tiêu đề không được vượt quá 200 ký tự.")]
        [DisplayName("Tiêu đề bài viết")]
        public string Title { get; set; }


        [StringLength(int.MaxValue, ErrorMessage = "Nội dung không được vượt quá độ dài cho phép.")]
        [DisplayName("Nội dung bài viết")]
        public string Content { get; set; }


        [Url(ErrorMessage = "Đường dẫn ảnh không hợp lệ.")]
        [DisplayName("Ảnh minh họa")]
        public string? ImageUrl { get; set; }


        [Required]
        [DisplayName("Ngày tạo")]
        public DateTime CreatedAt { get; set; }

        [Required]
        [DisplayName("Ngày cập nhật")]
        public DateTime UpdatedAt { get; set; }

        [Required]
        [DisplayName("Trạng thái bài viết")]
        public bool IsPublished { get; set; }



        [StringLength(450)]
        [DisplayName("Mã tác giả")]
        public string? AuthorId { get; set; }

        [Required]
        [ForeignKey("PostCategory")]
        [DisplayName("Danh mục bài viết")]
        public int CategoryId { get; set; }


        public virtual ApplicationUser? Author { get; set; }
        public virtual PostCategory? Category { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
        public virtual ICollection<ActionPost>? ActionPosts {  get; set; }    
    }

}
