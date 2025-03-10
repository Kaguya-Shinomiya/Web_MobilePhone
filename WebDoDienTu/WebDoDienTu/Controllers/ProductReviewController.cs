using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using WebDoDienTu.Data;
using WebDoDienTu.Models;

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
    public async Task<IActionResult> AddReview(int productId, int rating, string comment)
    {
        var emotion = "";
        using (HttpClient client = new HttpClient())
        {
            var requestData = new { Comment = comment };
            var jsonRequest = JsonSerializer.Serialize(requestData);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://127.0.0.1:5000/insert", content);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var data = JsonSerializer.Deserialize<JsonElement>(jsonResponse);


                    emotion = data.GetProperty("message").GetString();

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
            return Json(new { success = false, message = "Vui lòng đăng nhập!" });
        }


        if (rating < 1 || rating > 5)
        {
            return Json(new { success = false, message = "Đánh giá phải từ 1-5 sao!" });
        }


        if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.UserName))
        {
            return Json(new { success = false, message = "Thông tin người dùng không hợp lệ!" });
        }

        var review = new ProductReview
        {
            ProductId = productId,
            UserId = user.Id,
            YourName = user.UserName, 
            YourEmail = user.Email,
            Rating = rating,
            Comment = comment,       
            Emotions = emotion     
        };

        _context.ProductReviews.Add(review);
        await _context.SaveChangesAsync();

        return Json(new { success = true, message = "Đánh giá thành công!" });
    }
}