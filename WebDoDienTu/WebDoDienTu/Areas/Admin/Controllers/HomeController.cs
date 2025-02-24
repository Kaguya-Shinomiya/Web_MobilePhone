using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDoDienTu.Data;
using WebDoDienTu.Models;

namespace WebDoDienTu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var revenueData = await _context.Orders
                .GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Revenue = g.Sum(o => o.TotalPrice)
                })
                .ToListAsync();

            var revenueByMonth = new Dictionary<int, decimal>();
            for (int i = 1; i <= 12; i++)
            {
                revenueByMonth[i] = revenueData
                    .Where(x => x.Month == i)
                    .Sum(x => x.Revenue);
            }

            ViewBag.RevenueByMonth = revenueByMonth;
            var orders = _context.Orders.ToList();


            var tongDoanhSo = _context.Orders.Sum(order => order.TotalPrice);
            var tongSoLuongSP = _context.Products.Count();
            var tongDonHang = _context.Orders.Count();
            var adminRoleId = _context.Roles
                                .Where(role => role.Name == SD.Role_Admin)
                                .Select(role => role.Id)
                                .FirstOrDefault();
            var soLuongUser = _context.Users
                                .Where(user => !_context.UserRoles
                                .Where(role => role.RoleId == adminRoleId) 
                                .Select(role => role.UserId)
                                .Contains(user.Id))
                                .Count();
            ViewBag.tongDoanhSo = tongDoanhSo;
            ViewBag.tongSoLuongSP = tongSoLuongSP;
            ViewBag.tongDonHang = tongDonHang;
            ViewBag.soLuongUser = soLuongUser;
            return View(orders);
        }
    }
}
