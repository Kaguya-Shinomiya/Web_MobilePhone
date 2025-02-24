using Microsoft.AspNetCore.Mvc;
using WebDoDienTu.Repository;

namespace WebDoDienTu.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public HomeController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;   
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync();

            foreach(var pro in products)
            {
                if (pro.ReleaseDate > DateTime.UtcNow)
                {
                    ViewData["ProductRelease"] = pro;
                }
            }

            ViewData["Categories"] = await _categoryRepository.GetAllAsync();        
            return View(products);
        } 
    }
}
