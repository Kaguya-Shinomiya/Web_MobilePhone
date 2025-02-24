using WebDoDienTu.Models;
using WebDoDienTu.Repository;

namespace WebDoDienTu.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await _categoryRepository.GetByIdAsync(categoryId);
        }

        public async Task CreateCategoryAsync(Category category)
        {
            if (string.IsNullOrEmpty(category.CategoryName))
            {
                throw new ArgumentException("Tên danh mục không thể để trống.");
            }

            await _categoryRepository.AddAsync(category);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            if (string.IsNullOrEmpty(category.CategoryName))
            {
                throw new ArgumentException("Tên danh mục không thể để trống.");
            }

            await _categoryRepository.UpdateAsync(category);
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            await _categoryRepository.DeleteAsync(categoryId);
        }
    }
}
