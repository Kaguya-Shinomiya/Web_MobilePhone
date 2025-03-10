using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
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
			var emotion = "";
			using (HttpClient client = new HttpClient())
			{
				var requestData = new { Comment = comment }; // Dữ liệu gửi đi
				var jsonRequest = JsonSerializer.Serialize(requestData);
				var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

				var response = await client.PostAsync("http://127.0.0.1:5000/insert", content);

				if (response.IsSuccessStatusCode)
				{
					try
					{
						var jsonResponse = await response.Content.ReadAsStringAsync();
						var data = JsonSerializer.Deserialize<JsonElement>(jsonResponse);

						// Lấy giá trị của "message"
						emotion = data.GetProperty("message").GetString();
						//emotion = data?["message"];
						//Console.WriteLine(emotion);
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex);
					}
				}
				else
				{
					return Json(new { success = false, message = "Lỗi khi gọi API!" });
				}
			}
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
                Comment = comment,
				Emotional = emotion
				
			};

            _context.ProductReviews.Add(review);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Your review has been submitted successfully!";

            return Json(new { success = true, message = "Your review has been submitted successfully!" });

        }
    }
}
