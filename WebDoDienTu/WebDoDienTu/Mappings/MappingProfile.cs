using AutoMapper;
using WebDoDienTu.Models;
using WebDoDienTu.ViewModels;

namespace WebDoDienTu.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductCreateViewModel, Product>()
                .ForMember(dest => dest.Images, opt => opt.Ignore()) 
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore()) 
                .ForMember(dest => dest.VideoUrl, opt => opt.Ignore()) 
                .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => src.Attributes));


            CreateMap<ProductAttributeViewModel, ProductAttribute>();

            CreateMap<Product, ProductUpdateViewModel>()
                .ForMember(dest => dest.Images, opt => opt.Ignore())
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.VideoUrl, opt => opt.Ignore())
                .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => src.Attributes
                    .Select(a => new ProductAttributeViewModel
                    {
                        ProductAttributeId = a.ProductAttributeId,
                        AttributeName = a.AttributeName,
                        AttributeValue = a.AttributeValue
                    }).ToList()));

            CreateMap<ProductUpdateViewModel, Product>()
                .ForMember(dest => dest.Images, opt => opt.Ignore()) 
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.VideoUrl, opt => opt.Ignore()) 
                .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => src.Attributes));
        }
    }
}
