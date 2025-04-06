using WebDoDienTu.Models;

namespace WebDoDienTu.Repository
{
    public interface IProductViewRepository
    {
        Task<ProductView?> GetProductViewAsync(string userId, int productId);
        Task AddProductViewAsync(ProductView productView);
        Task UpdateProductViewAsync(ProductView productView);
        Task SaveChangesAsync();
    }
}
