using Microsoft.EntityFrameworkCore;
using WebDoDienTu.Data;
using WebDoDienTu.Models;

namespace WebDoDienTu.Repository
{
    public class PostCategoryRepository : IPostCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public PostCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PostCategory>> GetAllCategoriesAsync()
        {
            return await _context.PostCategories.ToListAsync();
        }

        public async Task<PostCategory> GetCategoryByIdAsync(int id)
        {
            return await _context.PostCategories.FindAsync(id);
        }

        public async Task AddAsync(PostCategory category)
        {
            _context.PostCategories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PostCategory category)
        {
            _context.PostCategories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _context.PostCategories.FindAsync(id);
            if (category != null)
            {
                _context.PostCategories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
