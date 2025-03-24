using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
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
		private readonly IServiceScopeFactory _serviceScopeFactory;

		public ProductReviewController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IServiceScopeFactory serviceScopeFactory)
		{
			_context = context;
			_userManager = userManager;
			_serviceScopeFactory = serviceScopeFactory; // Inject IServiceScopeFactory
		}

		[HttpPost]
		public async Task<IActionResult> AddReview(int productId, int rating, string comment)
		{
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

			// Tạo và lưu đánh giá ngay lập tức
			var review = new ProductReview
			{
				ProductId = productId,
				UserId = user.Id,
				YourName = user.UserName,
				YourEmail = user.Email,
				Rating = rating,
				Comment = comment,
				Emotion	 = "Pending",
				ReviewDate = DateTime.Now
			};

			_context.ProductReviews.Add(review);
			await _context.SaveChangesAsync();

			var reviewId = review.Id;

			// Gọi API cảm xúc không đồng bộ
			_ = Task.Run(async () =>
			{
				const int maxRetries = 3;
				for (int attempt = 1; attempt <= maxRetries; attempt++)
				{
					try
					{
						using (HttpClient client = new HttpClient())
						{
							client.Timeout = TimeSpan.FromSeconds(5);
							var requestData = new { Comment = comment };
							var jsonRequest = JsonSerializer.Serialize(requestData);
							var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

							var response = await client.PostAsync("http://127.0.0.1:5000/insert", content);

							if (response.IsSuccessStatusCode)
							{
								var jsonResponse = await response.Content.ReadAsStringAsync();
								var data = JsonSerializer.Deserialize<JsonElement>(jsonResponse);
								var emotion = data.GetProperty("message").GetString();

								// Tạo scope mới để lấy ApplicationDbContext
								using (var scope = _serviceScopeFactory.CreateScope())
								{
									var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
									var updatedReview = await dbContext.ProductReviews.FindAsync(reviewId);
									if (updatedReview != null)
									{
										updatedReview.Emotion = emotion;
										await dbContext.SaveChangesAsync();
										Console.WriteLine($"Cập nhật cảm xúc thành công: {emotion} cho review {reviewId}");
									}
									else
									{
										Console.WriteLine($"Không tìm thấy review {reviewId} để cập nhật");
									}
								}
								break;
							}
							else
							{
								var errorMessage = await response.Content.ReadAsStringAsync();
								Console.WriteLine($"Lỗi khi gọi API cảm xúc (lần {attempt}/{maxRetries}): {response.StatusCode} - {errorMessage}");
								if (attempt == maxRetries)
								{
									using (var scope = _serviceScopeFactory.CreateScope())
									{
										var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
										var updatedReview = await dbContext.ProductReviews.FindAsync(reviewId);
										if (updatedReview != null)
										{
											updatedReview.Emotion = "Failed";
											await dbContext.SaveChangesAsync();
											Console.WriteLine($"Cập nhật trạng thái Failed cho review {reviewId}");
										}
									}
								}
							}
						}
					}
					catch (Exception ex)
					{
						Console.WriteLine($"Lỗi khi gọi API cảm xúc (lần {attempt}/{maxRetries}): {ex.Message}");
						if (attempt == maxRetries)
						{
							using (var scope = _serviceScopeFactory.CreateScope())
							{
								var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
								var updatedReview = await dbContext.ProductReviews.FindAsync(reviewId);
								if (updatedReview != null)
								{
									updatedReview.Emotion = "Failed";
									await dbContext.SaveChangesAsync();
									Console.WriteLine($"Cập nhật trạng thái Failed cho review {reviewId}");
								}
							}
						}
					}
					if (attempt < maxRetries) await Task.Delay(1000);
				}
			}, CancellationToken.None);

			return Json(new { success = true, message = "Đánh giá thành công!", reviewId = reviewId });
		}

		[HttpGet]
		public IActionResult GetReviewById(int reviewId)
		{
			var review = _context.ProductReviews.Find(reviewId);
			if (review == null)
			{
				return Json(null);
			}

			return Json(new
			{
				yourName = review.YourName,
				rating = review.Rating,
				comment = review.Comment,
				reviewDate = review.ReviewDate,
				emotions = review.Emotion
			});
		}
	}
}