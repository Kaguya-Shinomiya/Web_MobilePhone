using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebDoDienTu.Models
{
    public class WishListItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Required]
        public int WishListId { get; set; }

        [ForeignKey("WishListId")]
        public WishList WishList { get; set; }

        public DateTime AddedDate { get; set; } = DateTime.UtcNow;
    }
}
