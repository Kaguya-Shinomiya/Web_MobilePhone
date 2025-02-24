namespace WebDoDienTu.Models
{
    public class ProductRecommendation
    {
        public int ProductRecommendationId { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public int PurchaseCount { get; set; }
        public int ViewCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
