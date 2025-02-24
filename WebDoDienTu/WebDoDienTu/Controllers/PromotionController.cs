using Microsoft.AspNetCore.Mvc;
using WebDoDienTu.Data;

namespace WebDoDienTu.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromotionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PromotionController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPromotions()
        {
            var promotions = _context.Promotions.ToList();
            return Ok(promotions);
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetPromotion(string code)
        {
            var promotion = _context.Promotions.FirstOrDefault(p => p.Code == code && p.IsActive
                                     && p.StartDate <= DateTime.Now && p.EndDate >= DateTime.Now);

            if (promotion == null)
            {
                return NotFound(new { message = "Mã giảm giá không hợp lệ hoặc đã hết hạn." });
            }

            return Ok(promotion);
        }
    }
}
