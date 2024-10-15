using AutoMapper;

using Microsoft.Extensions.Configuration;

using Store.data.Entity.OrderEntity;

namespace Store.Service.OrderServices.Dto
{
    internal class OrderItemImageUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        public OrderItemImageUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration { get; }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ProductItem.ImageUrl) || !source.ProductItem.ImageUrl.Contains(source.ProductItem.ImageUrl))
            {
                return $"{_configuration["BaseUrl"]}{source.ProductItem.ImageUrl}";
            }
            return "";
        }
    }
}
