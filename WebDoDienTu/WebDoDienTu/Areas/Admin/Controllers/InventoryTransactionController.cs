using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebDoDienTu.Data;
using WebDoDienTu.Models;

namespace WebDoDienTu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class InventoryTransactionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InventoryTransactionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hiển thị danh sách giao dịch kho
        public async Task<IActionResult> Index()
        {
            var transactions = await _context.InventoryTransactions
                .Include(t => t.Product)
                .ToListAsync();
            return View(transactions);
        }

        // Hiển thị form thêm mới giao dịch kho
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName");
            return View();
        }

        // Xử lý thêm mới giao dịch kho
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Quantity,TransactionType")] InventoryTransaction transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.TransactionDate = DateTime.Now;
                _context.Add(transaction);

                var product = await _context.Products.FindAsync(transaction.ProductId);

                if (transaction.TransactionType == TransactionType.Import)
                {
                    product.StockQuantity += transaction.Quantity;
                }
                else if (transaction.TransactionType == TransactionType.Export && product.StockQuantity >= transaction.Quantity)
                {
                    product.StockQuantity -= transaction.Quantity;
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", transaction.ProductId);
            return View(transaction);
        }
    }
}
