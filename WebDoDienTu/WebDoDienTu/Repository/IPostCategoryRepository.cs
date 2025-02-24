using WebDoDienTu.Models;

namespace WebDoDienTu.Repository
{
    public interface IPostCategoryRepository
    {
        Task<IEnumerable<PostCategory>> GetAllCategoriesAsync();
        Task<PostCategory> GetCategoryByIdAsync(int id);
        Task AddAsync(PostCategory category);
        Task UpdateAsync(PostCategory category);
        Task DeleteAsync(int id);
    }
}
