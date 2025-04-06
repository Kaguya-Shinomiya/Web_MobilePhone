using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using WebDoDienTu.Data;
using WebDoDienTu.Models;

namespace WebDoDienTu.Service
{
    public class RecommendationService
    {
        private readonly ApplicationDbContext _context;
        private readonly MLContext _mlContext;

        public RecommendationService(ApplicationDbContext context)
        {
            _context = context;
            _mlContext = new MLContext();
        }

        // Phương thức để lấy gợi ý sản phẩm cho người dùng
        public async Task<List<ProductRecommendationModel>> GetProductRecommendations(string userId)
        {
            var orders = await _context.OrderDetails
                .Where(od => od.Order.UserId == userId)
                .GroupBy(od => od.ProductId)
                .Select(g => new { ProductId = g.Key, PurchaseCount = g.Sum(od => od.Quantity) })
                .ToListAsync();

            var views = await _context.ProductViews
                .Where(pv => pv.UserId == userId)
                .GroupBy(pv => pv.ProductId)
                .Select(g => new { ProductId = g.Key, ViewCount = g.Sum(pv => pv.ViewCount) })
                .ToListAsync();

            var recommendations = new List<ProductRecommendationModel>();

            // Tổng hợp dữ liệu từ orders và views
            foreach (var order in orders)
            {
                var view = views.FirstOrDefault(v => v.ProductId == order.ProductId);
                var product = await _context.Products.FindAsync(order.ProductId);

                recommendations.Add(new ProductRecommendationModel
                {
                    ProductId = order.ProductId,
                    UserId = userId,
                    Label = order.PurchaseCount + (view?.ViewCount ?? 0), 
                    ProductName = product?.ProductName, 
                    Description = product?.Description,
                    ImageUrl = product?.ImageUrl,       
                    Price = product?.Price ?? 0   
                });
            }

            // Thêm những sản phẩm chỉ được xem nhưng không được mua
            foreach (var view in views)
            {
                if (!recommendations.Any(r => r.ProductId == view.ProductId))
                {
                    var product = await _context.Products.FindAsync(view.ProductId);

                    recommendations.Add(new ProductRecommendationModel
                    {
                        ProductId = view.ProductId,
                        UserId = userId,
                        Label = view.ViewCount,
                        ProductName = product?.ProductName,
                        Description = product?.Description,
                        ImageUrl = product?.ImageUrl,
                        Price = product?.Price ?? 0
                    });
                }
            }

            // Huấn luyện mô hình từ dữ liệu gợi ý
            var model = TrainModel(recommendations);

            // Lấy các gợi ý sản phẩm từ mô hình
            var topRecommendations = GetRecommendations(model, userId, 5);

            return topRecommendations;
        }


        // Phương thức huấn luyện mô hình
        public ITransformer TrainModel(List<ProductRecommendationModel> data)
        {
            var trainingData = data.Select(d => new ProductRecommendationInput
            {
                UserId = (uint)(Math.Abs(d.UserId.GetHashCode()) % 100000),
                ProductId = (uint)d.ProductId,
                Label = d.Label
            }).ToList();

            var dataView = _mlContext.Data.LoadFromEnumerable(trainingData);

            if (dataView.GetRowCount() == 0)
            {
                throw new InvalidOperationException("Không có dữ liệu hợp lệ để huấn luyện.");
            }

            var pipeline = _mlContext.Recommendation().Trainers.MatrixFactorization(
                labelColumnName: nameof(ProductRecommendationInput.Label),
                matrixColumnIndexColumnName: nameof(ProductRecommendationInput.UserId),
                matrixRowIndexColumnName: nameof(ProductRecommendationInput.ProductId),
                numberOfIterations: 20,
                approximationRank: 32);

            return pipeline.Fit(dataView);
        }

        // Phương thức để dự đoán và gợi ý sản phẩm
        public List<ProductRecommendationModel> GetRecommendations(ITransformer model, string userId, int topN)
        {
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<ProductRecommendationInput, ProductRecommendationInput>(model);

            var userKey = Convert.ToUInt32(userId.GetHashCode() & 0x7FFFFFFF);
            var allProductIds = _context.Products.Select(p => (uint)p.ProductId).ToList();
            var recommendations = new List<ProductRecommendationModel>();

            foreach (var productId in allProductIds)
            {
                var prediction = predictionEngine.Predict(new ProductRecommendationInput
                {
                    UserId = userKey,
                    ProductId = productId
                });
                var p = _context.Products.FirstOrDefault(p => p.ProductId == productId);
                recommendations.Add(new ProductRecommendationModel
                {
                    ProductId = (int)productId,
                    UserId = userId,
                    Label = prediction.Label,
                    ProductName = p?.ProductName,
                    Description = p?.Description,
                    ImageUrl = p?.ImageUrl,
                    Price = p?.Price ?? 0
                });
            }

            return recommendations.OrderByDescending(r => r.Label).Take(topN).ToList();
        }


        // Phương thức lưu trữ các gợi ý sản phẩm vào database
        //public async Task SaveRecommendations(List<ProductRecommendation> recommendations)
        //{
        //    foreach (var recommendation in recommendations)
        //    {
        //        _context.ProductRecommendations.Add(recommendation);
        //    }

        //    await _context.SaveChangesAsync();
        //}
    }
}
