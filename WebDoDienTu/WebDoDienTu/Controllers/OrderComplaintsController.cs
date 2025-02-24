using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebDoDienTu.Data;
using WebDoDienTu.Models;
using WebDoDienTu.Service.MailKit;

namespace WebDoDienTu.Controllers
{
    public class OrderComplaintsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public OrderComplaintsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        // Hiển thị form khiếu nại cho đơn hàng
        public async Task<IActionResult> Report(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }

            var model = new OrderComplaint
            {
                OrderId = orderId,
                Order = order
            };
            return View(model);
        }

        // Xử lý khi người dùng gửi khiếu nại cho đơn hàng
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SubmitComplaint(OrderComplaint complaint)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                complaint.UserId = user.Id;
                complaint.ComplaintDate = DateTime.Now;
                complaint.Status = OrderComplaintStatus.Pending;

                _context.OrderComplaints.Add(complaint);
                await _context.SaveChangesAsync();

                // Gửi email thông báo cho người dùng
                var emailContent = GenerateComplaintSubmittedEmailContent(complaint);
                await _emailSender.SendEmailAsync(user.Email, "Thông báo gửi khiếu nại thành công", emailContent);

                TempData["Message"] = "Your complaint has been submitted successfully.";
                return RedirectToAction("Report", new { orderId = complaint.OrderId });
            }

            var order = await _context.Orders.FindAsync(complaint.OrderId);
            complaint.Order = order;
            return View("Report", complaint);
        }

        private string GenerateComplaintSubmittedEmailContent(OrderComplaint complaint)
        {
            string template = System.IO.File.ReadAllText("Templates/OrderComplaints/ComplaintSubmittedEmailTemplate.html");
            var user = _userManager.FindByIdAsync(complaint.UserId).Result;
            string fullName = user.LastName;

            template = template.Replace("{{UserName}}", fullName);
            template = template.Replace("{{OrderId}}", complaint.OrderId.ToString());

            return template;
        }
    }
}
