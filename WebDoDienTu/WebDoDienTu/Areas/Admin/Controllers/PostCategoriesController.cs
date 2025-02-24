using Microsoft.AspNetCore.Mvc;
using WebDoDienTu.Models;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using WebDoDienTu.Repository;

namespace WebDoDienTu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class PostCategoriesController : Controller
    {
        private readonly IPostCategoryRepository _categoryRepository;

        public PostCategoriesController(IPostCategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: Admin/PostCategories
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return View(categories);
        }

        // POST: Admin/PostCategories/Create
        [HttpPost]
        public async Task<JsonResult> Create([FromBody] PostCategory category)
        {
            if (ModelState.IsValid)
            {
                await _categoryRepository.AddAsync(category);
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Model không hợp lệ." });
        }

        // POST: Admin/PostCategories/Edit
        [HttpPost]
        public async Task<JsonResult> Edit([FromBody] PostCategory category)
        {
            if (ModelState.IsValid)
            {
                await _categoryRepository.UpdateAsync(category);
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Model không hợp lệ." });
        }

        // POST: Admin/PostCategories/Delete
        [HttpPost]
        public async Task<JsonResult> Delete([FromBody] int id)
        {
            if (id <= 0)
            {
                return Json(new { success = false, message = "ID không hợp lệ." });
            }

            var categoryExists = await _categoryRepository.GetCategoryByIdAsync(id);
            if (categoryExists == null)
            {
                return Json(new { success = false, message = "Danh mục không tồn tại." });
            }

            await _categoryRepository.DeleteAsync(id);
            return Json(new { success = true });
        }
    }
}
