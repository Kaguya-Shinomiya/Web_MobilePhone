using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDoDienTu.Models
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "URL của ảnh là bắt buộc.")]
        [Url(ErrorMessage = "URL của ảnh không hợp lệ.")]
        [DisplayName("Đường dẫn ảnh")]
        public string Url { get; set; }

        [Required]
        [ForeignKey("Product")]
        [DisplayName("Mã sản phẩm")]
        public int ProductId { get; set; }


        public Product? Product { get; set; }
    }
}
