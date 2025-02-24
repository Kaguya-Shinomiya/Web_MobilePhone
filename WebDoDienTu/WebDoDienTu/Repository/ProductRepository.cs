using Microsoft.EntityFrameworkCore;
using WebDoDienTu.Data;
using WebDoDienTu.Models;

namespace WebDoDienTu.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.Include(x => x.Category).Include(p => p.Images).Include(p => p.Attributes).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.Include(x => x.Category).Include(x => x.Images).Include(p => p.Attributes).FirstOrDefaultAsync(x => x.ProductId == id);
        }

        public async Task AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await GetByIdAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Product> GetByNameAsync(int id)
        {
            return await _context.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.ProductId == id);
        }

        public async Task<IEnumerable<Product>> GetByNameAsync(string StringName)
        {
            return await _context.Products.Include(s => s.Category).Where(x => x.ProductName.Contains(StringName)).ToListAsync();
        }

        public async Task RemoveImagesAsync(List<ProductImage> images)
        {
            _context.ProductImages.RemoveRange(images);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAttributesAsync(List<ProductAttribute> attributes)
        {
            _context.ProductAttributes.RemoveRange(attributes);
            await _context.SaveChangesAsync();
        }
    }
}
