using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDoDienTu.Models
{
    public class InventoryTransaction
    {
        [Key]
        public int TransactionId { get; set; }

        [Required]
        [DisplayName("Ngày giao dịch")]
        public DateTime TransactionDate { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        [DisplayName("Số lượng")]
        public int Quantity { get; set; }

        [Required]
        [DisplayName("Loại giao dịch")]
        public TransactionType TransactionType { get; set; }

        [Required]
        [DisplayName("Mã sản phẩm")]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
    }
}
