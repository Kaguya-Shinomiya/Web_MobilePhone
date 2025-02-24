namespace WebDoDienTu.Service
{
    public interface IProductViewService
    {
        Task RecordProductViewAsync(string userId, int productId);
    }
}
