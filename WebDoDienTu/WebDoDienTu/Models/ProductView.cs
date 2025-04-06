namespace WebDoDienTu.Models
{
    public class ProductView
    {
        public int ProductViewId { get; set; }

        public string UserId { get; set; }
        public int ProductId { get; set; }

        public int ViewCount { get; set; }
        public DateTime LastViewedDate { get; set; } 


        public virtual ApplicationUser User { get; set; }
        public virtual Product Product { get; set; }
    }
}
