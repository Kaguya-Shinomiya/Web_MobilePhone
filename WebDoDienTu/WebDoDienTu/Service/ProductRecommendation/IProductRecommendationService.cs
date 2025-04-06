using WebDoDienTu.Models;

namespace WebDoDienTu.Service.ProductRecommendation
{ 
    public interface IProductRecommendationService
    {
        List<Product> GetRecommendedProducts(int productId);
    }
}
