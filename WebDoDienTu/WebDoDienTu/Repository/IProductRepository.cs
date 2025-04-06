using WebDoDienTu.Models;

namespace WebDoDienTu.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
        Task<IEnumerable<Product>> GetByNameAsync(string name);
        Task RemoveImagesAsync(List<ProductImage> images);
        Task RemoveAttributesAsync(List<ProductAttribute> attributes);
    }
}
