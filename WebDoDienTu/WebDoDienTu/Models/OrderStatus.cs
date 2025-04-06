namespace WebDoDienTu.Models
{
    public enum OrderStatus
    {
        Pending,     // Đang chờ xử lý
        PreOrder,    // Đặt hàng trước
        Processing,  // Đang xử lý
        Shipped,     // Đã giao hàng
        Delivered,   // Đã giao
        Completed,   // Đã hoàn thành
        Cancelled    // Đã hủy
    }
}
