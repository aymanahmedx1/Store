using AutoMapper;

using Store.Repository.Basket.Models;

namespace Store.Service.BasketService.Dtos
{
    public class BasketProfile:Profile
    {
        public BasketProfile()
        {
                CreateMap<BasketItem,BasketItemDto>().ReverseMap();
                CreateMap<CustomerBasket,CustomerBasketDto>().ReverseMap();
        }
    }
}
