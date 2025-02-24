using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDoDienTu.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [StringLength(450)]
        [DisplayName("Mã người dùng")]
        public string? UserId { get; set; }

        [Required]
        [DisplayName("Ngày đặt")]
        public DateTime OrderDate { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Tổng tiền phải lớn hơn hoặc bằng 0")]
        [DisplayName("Tổng tiền")]
        public decimal TotalPrice { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Số tiền giảm giá phải lớn hơn hoặc bằng 0")]
        [DisplayName("Số tiền giảm giá")]
        public decimal DiscountAmount { get; set; } = 0;

        [Required]
        [DisplayName("Trạng thái")]
        public OrderStatus Status { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Họ phải có ít nhất 1 ký tự")]
        [DisplayName("Họ")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Tên phải có ít nhất 1 ký tự")]
        [DisplayName("Tên")]
        public string LastName { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Số điện thoại phải từ 10 đến 15 ký tự")]
        [DisplayName("Số điện thoại")]
        public string Phone { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(100)]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "Địa chỉ phải có ít nhất 5 ký tự")]
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }

        [StringLength(500)]
        [DisplayName("Ghi chú")]
        public string? Notes { get; set; }

        public int? PromotionId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Số tiền đặt cọc phải lớn hơn hoặc bằng 0")]
        [DisplayName("Số tiền đặt cọc")]
        public decimal DepositAmount { get; set; }

        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser? ApplicationUser { get; set; }

        [JsonIgnore]
        public List<OrderDetail>? OrderDetails { get; set; }     

        public Promotion? Promotion { get; set; }

        public ICollection<OrderComplaint>? Complaints { get; set; }
    }
}
