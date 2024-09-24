using AutoMapper;

using Store.data.Entity;
using Store.Service.ProductServices.Dto;

namespace Store.Service.Helpers
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<Product, ProductDetailsDto>()
                .ForMember(d => d.BrandName, op => op.MapFrom(x => x.Brand.Name))
                .ForMember(d => d.TypeName, op => op.MapFrom(x => x.Type.Name))
                .ForMember(d => d.PictureUrl, options => options.MapFrom<ProductImageUrlResolver>());

            CreateMap<ProductBrand, BrandTypeDetailsDto>();
            CreateMap<ProductType, BrandTypeDetailsDto>();
        }
    }
}
