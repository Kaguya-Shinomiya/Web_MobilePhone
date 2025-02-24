using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDoDienTu.Data;
using WebDoDienTu.Models;
using WebDoDienTu.Service.MailKit;

namespace WebDoDienTu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
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

        // Danh sách khiếu nại 
        public async Task<IActionResult> ComplaintList()
        {
            var complaints = await _context.OrderComplaints
                .Include(c => c.Order)
                .Include(c => c.User)
                .ToListAsync();
            return View(complaints);
        }

        // Xử lý khiếu nại
        [HttpPost]
        public async Task<IActionResult> ResolveComplaint(int id, OrderComplaintStatus orderComplaintStatus, string adminResponse)
        {
            var complaint = await _context.OrderComplaints.FindAsync(id);
            if (complaint == null)
            {
                return NotFound();
            }

            // Gửi email thông báo cho người dùng
            var user = await _userManager.FindByIdAsync(complaint.UserId);
            var emailContent = GenerateEmailContent(complaint, adminResponse);
            await _emailSender.SendEmailAsync(user.Email, "Thông báo giải quyết khiếu nại", emailContent);

            complaint.Status = orderComplaintStatus;
            await _context.SaveChangesAsync();

            return RedirectToAction("ComplaintList");
        }

        private string GenerateEmailContent(OrderComplaint complaint, string adminResponse)
        {           
            string template = System.IO.File.ReadAllText("Templates/OrderComplaints/ResolveComplaintEmailTemplate.html");
            var user = _userManager.FindByIdAsync(complaint.UserId).Result;
            string fullName = user.LastName;
            template = template.Replace("{{UserName}}", fullName);
            template = template.Replace("{{OrderId}}", complaint.OrderId.ToString());
            template = template.Replace("{{Status}}", complaint.Status.ToString());
            template = template.Replace("{{AdminResponse}}", adminResponse);

            return template;
        }
    }
}
