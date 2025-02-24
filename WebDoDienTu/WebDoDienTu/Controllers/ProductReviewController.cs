using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebDoDienTu.Data;
using WebDoDienTu.Models;

namespace WebDoDienTu.Controllers
{
    public class ProductReviewController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductReviewController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(int productId,string name, string email, int rating, string comment)
        {
            var user = await _userManager.GetUserAsync(User);
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { success = false, message = "Vui lòng đăng nhập để thực hiện đánh giá!" });
            }

            var review = new ProductReview
            {
                ProductId = productId,
                UserId = user.Id,
                YourName = name,
                YourEmail = email,
                Rating = rating,
                Comment = comment
            };

            _context.ProductReviews.Add(review);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Your review has been submitted successfully!";

            return Json(new { success = true, message = "Your review has been submitted successfully!" });
        }
    }
}
