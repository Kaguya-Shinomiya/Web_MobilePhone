using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace WebDoDienTu.Models
{
    public class ProductAttribute
    {
        [Key]
        public int ProductAttributeId { get; set; }

        [Required]
        [ForeignKey("Product")]
        [DisplayName("Mã sản phẩm")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Tên thuộc tính là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên thuộc tính không được vượt quá 100 ký tự.")]
        [DisplayName("Tên thuộc tính")]
        public string AttributeName { get; set; } = String.Empty;

        [Required(ErrorMessage = "Giá trị thuộc tính là bắt buộc.")]
        [StringLength(200, ErrorMessage = "Giá trị thuộc tính không được vượt quá 200 ký tự.")]
        [DisplayName("Giá trị thuộc tính")]
        public string AttributeValue { get; set; } = String.Empty;


        [JsonIgnore] 
        public virtual Product? Product { get; set; }
    }
}
