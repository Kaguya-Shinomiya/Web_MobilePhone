using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebDoDienTu.ViewModels
{
    public class ProductUpdateViewModel
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống.")]
        [StringLength(100, ErrorMessage = "Tên sản phẩm không được vượt quá 100 ký tự.")]
        [DisplayName("Tên sản phẩm")]
        public string ProductName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Giá không được để trống.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0.")]
        [DisplayName("Giá")]
        public decimal Price { get; set; } = decimal.Zero;

        [Required(ErrorMessage = "Mô tả không được để trống.")]
        [StringLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự.")]
        [DisplayName("Mô tả")]
        public string Description { get; set; } = string.Empty;

        [DisplayName("Hình ảnh")]
        public IFormFile? ImageUrl { get; set; }

        [DisplayName("Video")]
        public IFormFile? VideoUrl { get; set; }

        [DisplayName("Ảnh khác")]
        public List<IFormFile>? Images { get; set; }

        [DisplayName("Loại sản phẩm")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Ngày phát hành sản phẩm không được để trống.")]
        [DataType(DataType.Date, ErrorMessage = "Ngày phát hành không hợp lệ.")]
        [DisplayName("Ngày phát hành")]
        public DateTime ReleaseDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Thuộc tính sản phẩm không được để trống.")]
        [DisplayName("Thuộc tính")]
        public List<ProductAttributeViewModel> Attributes { get; set; } = new List<ProductAttributeViewModel>();
    }
}
