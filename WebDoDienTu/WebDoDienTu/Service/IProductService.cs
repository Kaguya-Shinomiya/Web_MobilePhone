using WebDoDienTu.Models;
using WebDoDienTu.ViewModels;

namespace WebDoDienTu.Service
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task CreateProductAsync(ProductCreateViewModel viewModel);
        Task UpdateProductAsync(ProductUpdateViewModel viewModel);
        Task DeleteProductAsync(int id);
    }
}
