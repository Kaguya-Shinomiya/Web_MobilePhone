using Microsoft.ML.Data;

namespace WebDoDienTu.Models
{
    public class ProductRecommendationModel
    {
        public string UserId { get; set; } 
        public int ProductId { get; set; } 
        public float Label { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public float Price { get; set; }
    }
}
