using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using WebDoDienTu.Models;
using WebDoDienTu.Repository;
using AutoMapper;
using WebDoDienTu.ViewModels;

namespace WebDoDienTu.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICloudinary _cloudinary;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, ICloudinary cloudinary, IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _cloudinary = cloudinary;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task CreateProductAsync(ProductCreateViewModel viewModel)
        {
            // Ánh xạ từ ViewModel sang Entity
            var product = _mapper.Map<Product>(viewModel);

            // Xử lý ảnh chính
            if (viewModel.ImageUrl != null)
            {
                product.ImageUrl = await SaveImageAsync(viewModel.ImageUrl);
            }

            // Xử lý video
            if (viewModel.VideoUrl != null)
            {
                product.VideoUrl = await SaveVideoAsync(viewModel.VideoUrl);
            }

            // Xử lý danh sách ảnh khác
            if (viewModel.Images != null && viewModel.Images.Any())
            {
                product.Images = new List<ProductImage>();
                foreach (var image in viewModel.Images)
                {
                    var imageUrl = await SaveImageAsync(image);
                    if (imageUrl != null)
                    {
                        product.Images.Add(new ProductImage { Url = imageUrl });
                    }
                }
            }

            // Xử lý thuộc tính
            if (viewModel.Attributes != null && viewModel.Attributes.Any())
            {
                product.Attributes = viewModel.Attributes
                    .Select(attr => new ProductAttribute
                    {
                        AttributeName = attr.AttributeName,
                        AttributeValue = attr.AttributeValue
                    })
                    .ToList();
            }

            await _productRepository.AddAsync(product);
        }

        public async Task UpdateProductAsync(ProductUpdateViewModel viewModel)
        {
            var existingProduct = await _productRepository.GetByIdAsync(viewModel.ProductId);

            if (existingProduct == null)
            {
                throw new Exception("Sản phẩm không tồn tại.");
            }

            _mapper.Map(viewModel, existingProduct);

            // Cập nhật ảnh đại diện
            if (viewModel.ImageUrl != null)
            {
                existingProduct.ImageUrl = await SaveImageAsync(viewModel.ImageUrl);
            }

            // Cập nhật video
            if (viewModel.VideoUrl != null)
            {
                existingProduct.VideoUrl = await SaveVideoAsync(viewModel.VideoUrl);
            }

            // Cập nhật danh sách ảnh khác
            if (viewModel.Images != null && viewModel.Images.Any())
            {
                // Xóa ảnh cũ
                await _productRepository.RemoveImagesAsync(existingProduct.Images);

                // Thêm ảnh mới
                existingProduct.Images = new List<ProductImage>();
                foreach (var image in viewModel.Images)
                {
                    var imageUrl = await SaveImageAsync(image);
                    if (!string.IsNullOrEmpty(imageUrl))
                    {
                        existingProduct.Images.Add(new ProductImage { Url = imageUrl });
                    }
                }
            }

            // Cập nhật thuộc tính sản phẩm
            var updatedAttributes = viewModel.Attributes
                .Where(a => !string.IsNullOrEmpty(a.AttributeName))
                .ToList();

            foreach (var attr in updatedAttributes)
            {
                var existingAttr = existingProduct.Attributes
                    .FirstOrDefault(a => a.ProductAttributeId == attr.ProductAttributeId);

                if (existingAttr != null)
                {
                    // Cập nhật thuộc tính cũ
                    existingAttr.AttributeName = attr.AttributeName;
                    existingAttr.AttributeValue = attr.AttributeValue;
                }
                else
                {
                    // Thêm thuộc tính mới
                    existingProduct.Attributes.Add(new ProductAttribute
                    {
                        AttributeName = attr.AttributeName,
                        AttributeValue = attr.AttributeValue
                    });
                }
            }

            // Xóa thuộc tính không còn
            var removedAttributes = existingProduct.Attributes
                .Where(ea => !updatedAttributes.Any(ua => ua.ProductAttributeId == ea.ProductAttributeId))
                .ToList();
            await _productRepository.RemoveAttributesAsync(removedAttributes);

            await _productRepository.UpdateAsync(existingProduct);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }

        private async Task<string?> SaveImageAsync(IFormFile image)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(image.FileName, image.OpenReadStream()),
                PublicId = $"products/{Guid.NewGuid()}"
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUrl.ToString();
            }

            return null;
        }

        private async Task<string?> SaveVideoAsync(IFormFile video)
        {
            var uploadParams = new VideoUploadParams()
            {
                File = new FileDescription(video.FileName, video.OpenReadStream()),
                PublicId = $"products/videos/{Guid.NewGuid()}"
            };

            var uploadResult = await _cloudinary.UploadLargeAsync<VideoUploadResult>(uploadParams, 6000000, 5);

            if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUrl.ToString();
            }

            return null;
        }
    }
}
