using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDoDienTu.Data;
using WebDoDienTu.Models;

namespace WebDoDienTu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult ManageReviews()
        {
            var reviews = _context.ProductReviews.Include(r => r.Product).ToList();
            return View(reviews);
        }

        [HttpPost]
        public IActionResult ToggleReviewVisibility(int reviewId)
        {
            var review = _context.ProductReviews.Find(reviewId);
            if (review != null)
            {
                review.IsHidden = !review.IsHidden;
                _context.SaveChanges();
            }
            return RedirectToAction("ManageReviews");
        }
    }
}
