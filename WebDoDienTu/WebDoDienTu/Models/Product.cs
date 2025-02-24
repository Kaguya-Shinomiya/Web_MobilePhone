using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebDoDienTu.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống.")]
        [StringLength(100, ErrorMessage = "Tên sản phẩm không được vượt quá 100 ký tự.")]
        [DisplayName("Tên sản phẩm")]
        public string ProductName { get; set; } = String.Empty;

        [Required(ErrorMessage = "Giá không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "Giá phải lớn hơn 0.")]
        [DisplayName("Giá")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Mô tả không được để trống.")]
        [StringLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự.")]
        [DisplayName("Mô tả")]
        public string Description { get; set; } = String.Empty;

        [Required(ErrorMessage = "Hình ảnh sản phẩm không được để trống.")]
        [DisplayName("Hình ảnh")]
        public string ImageUrl { get; set; } = String.Empty;

        [DisplayName("Video")]
        public string? VideoUrl { get; set; }
        
        [DisplayName("Ảnh khác")]
        public List<ProductImage>? Images { get; set; }

        [Required]
        [DisplayName("Sản phẩm hot")]
        public bool IsHoted { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn kho phải lớn hơn hoặc bằng 0.")]
        [DisplayName("Số lượng tồn kho")]
        public int StockQuantity { get; set; }

        [DisplayName("Đặt trước")]
        public bool IsPreOrder { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Ngày phát hành không hợp lệ.")]
        [DisplayName("Ngày phát hành")]
        public DateTime? ReleaseDate { get; set; }

        [ForeignKey("Category")]
        [DisplayName("Loại sản phẩm")]
        public int CategoryId { get; set; }

        [DisplayName("Loại sản phẩm")]
        public Category? Category { get; set; }

        public ICollection<ProductReview>? Reviews { get; set; }
        public ICollection<InventoryTransaction>? InventoryTransactions { get; set; }

        [JsonIgnore]
        [DisplayName("Thuộc tính")]
        public ICollection<ProductAttribute> Attributes { get; set; } = new List<ProductAttribute>();
    }
}
