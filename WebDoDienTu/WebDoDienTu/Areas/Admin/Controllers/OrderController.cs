using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDoDienTu.Data;
using WebDoDienTu.Models;

namespace WebDoDienTu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context; 

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/orders")]
        public async Task<IActionResult> GetOrders()
        {
            // Thực hiện truy vấn SQL Server để lấy dữ liệu đơn hàng
            var orders = await _context.Orders.ToListAsync();

            // Trả về dữ liệu dưới dạng phản hồi JSON
            return Ok(orders);
        }


        public IActionResult Index()
        {
            var orders = _context.Orders.ToList();
            return View(orders);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var order = _context.Orders.Find(id);
            return View(order);
        }

        public IActionResult Delete(int id)
        {
            var order = _context.Orders.Find(id);
            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var item = _context.Orders.Find(id);          
            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(int id, Order order)
        {
            if(id != order.Id)
            {
                return NotFound();
            }
            _context.Orders.Update(order);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, OrderStatus status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }
            order.Status = status;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
