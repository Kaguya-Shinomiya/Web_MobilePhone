using Microsoft.EntityFrameworkCore;
using WebDoDienTu.Data;
using WebDoDienTu.Models;

namespace WebDoDienTu.Repository
{
    public class ProductViewRepository : IProductViewRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductViewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProductView?> GetProductViewAsync(string userId, int productId)
        {
            return await _context.ProductViews.FirstOrDefaultAsync(pv => pv.UserId == userId && pv.ProductId == productId);
        }

        public async Task AddProductViewAsync(ProductView productView)
        {
            await _context.ProductViews.AddAsync(productView);
        }

        public Task UpdateProductViewAsync(ProductView productView)
        {
            _context.ProductViews.Update(productView);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
