using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebDoDienTu.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [DisplayName("Họ")]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [DisplayName("Tên")]
        public string LastName { get; set; } = string.Empty;
        public string? Address { get; set; }

        [DisplayName("Tuổi")]
        public string? Age { get; set; }
        public bool? IsBlocked { get; set; }
    }
}
