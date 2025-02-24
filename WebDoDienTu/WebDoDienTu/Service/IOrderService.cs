using WebDoDienTu.Models;

namespace WebDoDienTu.Service
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrdersByUserIdAsync(string userId);
    }
}
