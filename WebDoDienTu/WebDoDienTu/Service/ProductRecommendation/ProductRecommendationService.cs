using Microsoft.ML;
using Microsoft.ML.Data;
using WebDoDienTu.Data;
using WebDoDienTu.Models;

namespace WebDoDienTu.Service.ProductRecommendation
{
    public class ProductRecommendationService
    {
        private readonly MLContext _mlContext;
        private readonly ApplicationDbContext _dbContext;
        private IDataView _productDataView;
        private List<Product> _products;

        public ProductRecommendationService(ApplicationDbContext dbContext)
        {
            _mlContext = new MLContext();
            _dbContext = dbContext;
            LoadProductData();
        }

        private void LoadProductData()
        {
            _products = _dbContext.Products.ToList();

            // Tạo DataView từ danh sách sản phẩm
            _productDataView = _mlContext.Data.LoadFromEnumerable(
                _products.Select(p => new ProductData
                {
                    ProductId = p.ProductId,
                    Description = p.Description,
                    CategoryId = p.CategoryId
                }));
        }

        public List<Product> GetRecommendedProducts(int productId)
        {
            var product = _products.FirstOrDefault(p => p.ProductId == productId);
            if (product == null) return new List<Product>();

            var pipeline = _mlContext.Transforms.Text.FeaturizeText("DescriptionFeatures", nameof(ProductData.Description))
                            .Append(_mlContext.Transforms.Concatenate("Features", "DescriptionFeatures"))
                            .Append(_mlContext.Transforms.NormalizeMinMax("Features"));

            var model = pipeline.Fit(_productDataView);
            var transformedData = model.Transform(_productDataView);

            var productFeatures = transformedData.GetColumn<float[]>("Features").ToArray();
            var productIndex = _products.FindIndex(p => p.ProductId == productId);
            var similarities = productFeatures
                .Select((features, index) => new { Index = index, Similarity = CosineSimilarity(productFeatures[productIndex], features) })
                .OrderByDescending(s => s.Similarity)
                .Where(s => s.Index != productIndex)
                .Take(5);

            return similarities.Select(s => _products[s.Index]).ToList();
        }

        private float CosineSimilarity(float[] vectorA, float[] vectorB)
        {
            float dotProduct = 0;
            float magnitudeA = 0;
            float magnitudeB = 0;

            for (int i = 0; i < vectorA.Length; i++)
            {
                dotProduct += vectorA[i] * vectorB[i];
                magnitudeA += vectorA[i] * vectorA[i];
                magnitudeB += vectorB[i] * vectorB[i];
            }

            return dotProduct / (float)(Math.Sqrt(magnitudeA) * Math.Sqrt(magnitudeB));
        }
    }
}
