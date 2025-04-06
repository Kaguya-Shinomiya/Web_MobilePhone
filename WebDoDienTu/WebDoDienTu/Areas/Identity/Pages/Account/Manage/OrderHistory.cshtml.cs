using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebDoDienTu.Models;
using WebDoDienTu.Service;

namespace WebDoDienTu.Areas.Identity.Pages.Account.Manage
{
    public class OrderHistoryModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrderService _orderService;

        public OrderHistoryModel(UserManager<ApplicationUser> userManager, IOrderService orderService)
        {
            _userManager = userManager;
            _orderService = orderService;
        }

        public IList<Order> Orders { get; set; }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                Orders = await _orderService.GetOrdersByUserIdAsync(user.Id);
            }
        }
    }
}
