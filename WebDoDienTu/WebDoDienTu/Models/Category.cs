using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebDoDienTu.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Tên danh mục là bắt buộc.")]
        [StringLength(50, ErrorMessage = "Tên danh mục không được vượt quá 50 ký tự.")]
        [DisplayName("Tên danh mục")]
        public string CategoryName { get; set; }

        public List<Product>? Products { get; set; }
    }
}
