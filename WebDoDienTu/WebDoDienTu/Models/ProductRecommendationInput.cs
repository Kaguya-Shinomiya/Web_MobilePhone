using Microsoft.ML.Data;

namespace WebDoDienTu.Models
{
    public class ProductRecommendationInput
    {
        [KeyType(100000)] 
        public uint UserId { get; set; }

        [KeyType(100000)] 
        public uint ProductId { get; set; }

        public float Label { get; set; }
    }
}
