using WebDoDienTu.Models;
using WebDoDienTu.Repository;

namespace WebDoDienTu.Service
{
    public class ProductViewService : IProductViewService
    {
        private readonly IProductViewRepository _productViewRepository;

        public ProductViewService(IProductViewRepository productViewRepository)
        {
            _productViewRepository = productViewRepository;
        }

        public async Task RecordProductViewAsync(string userId, int productId)
        {
            var productView = await _productViewRepository.GetProductViewAsync(userId, productId);

            if (productView != null)
            {
                productView.ViewCount++;
                productView.LastViewedDate = DateTime.Now;
                await _productViewRepository.UpdateProductViewAsync(productView);
            }
            else
            {
                productView = new ProductView
                {
                    UserId = userId,
                    ProductId = productId,
                    ViewCount = 1,
                    LastViewedDate = DateTime.Now
                };
                await _productViewRepository.AddProductViewAsync(productView);
            }

            await _productViewRepository.SaveChangesAsync();
        }
    }
}
