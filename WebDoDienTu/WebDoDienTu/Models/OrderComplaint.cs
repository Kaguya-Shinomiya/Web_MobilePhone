using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDoDienTu.Models
{
    public class OrderComplaint
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Order")]
        [DisplayName("Mã đơn hàng")]
        public int OrderId { get; set; }

        [StringLength(450)]
        [DisplayName("Mã người dùng")]
        public string? UserId { get; set; }

        [Required]  
        [StringLength(1000)]  
        [DisplayName("Mô tả khiếu nại")]
        public string ComplaintDescription { get; set; } = string.Empty;

        [Required]
        [DisplayName("Ngày khiếu nại")]
        public DateTime ComplaintDate { get; set; } = DateTime.Now;

        [Required]
        [DisplayName("Trạng thái khiếu nại")]
        public OrderComplaintStatus Status { get; set; } = OrderComplaintStatus.Pending;


        public Order? Order { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
