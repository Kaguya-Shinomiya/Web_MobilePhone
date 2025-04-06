using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDoDienTu.Models
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        [Required] 
        [ForeignKey("Order")]
        [DisplayName("Mã đơn hàng")]
        public int OrderId { get; set; }

        [Required] 
        [ForeignKey("Product")]
        [DisplayName("Mã sản phẩm")]
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0.")]
        [DisplayName("Số lượng")]
        public int Quantity { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Giá phải là số dương.")]
        [DisplayName("Giá")]
        public decimal Price { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Giá giảm phải là số dương.")]
        [DisplayName("Giá giảm")]
        public decimal? DiscountedPrice { get; set; }


        public Order? Order { get; set; }
        public Product? Product { get; set; }
    }
}
