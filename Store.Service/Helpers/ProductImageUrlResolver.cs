
using AutoMapper;

using Microsoft.Extensions.Configuration;

using Store.data.Entity;
using Store.Service.ProductServices.Dto;

namespace Store.Service.Helpers
{
    public class ProductImageUrlResolver : IValueResolver<Product, ProductDetailsDto, string>
    {
        public ProductImageUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration { get; }

        public string Resolve(Product source, ProductDetailsDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_configuration["BaseUrl"]}{source.PictureUrl}";
            }
            return "";
        }
    }
}
