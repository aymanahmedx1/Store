using AutoMapper;

using Store.data.Entity.OrderEntity;

namespace Store.Service.OrderServices.Dto
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<ShippingAddress, AddressDto>().ReverseMap();
            CreateMap<Order, OrderDetailsDto>()
                .ForMember(d => d.DeliveryMethodName, op => op.MapFrom(x => x.DeliveryMethod.ShortName))
                .ForMember(d => d.ShippngPrice, op => op.MapFrom(x => x.DeliveryMethod.Price));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, op => op.MapFrom(x => x.ProductItem.ProductId))
                .ForMember(d => d.ProductName, op => op.MapFrom(x => x.ProductItem.ProductName))
                .ForMember(d => d.ImageUrl, op => op.MapFrom<OrderItemImageUrlResolver>());

        }
    }
}
