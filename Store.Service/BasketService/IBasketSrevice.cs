using Store.Repository.Basket.Models;
using Store.Service.BasketService.Dtos;

namespace Store.Service.BasketService
{
    public interface IBasketSrevice
    {
        Task<CustomerBasketDto> GetBasketAsync(string BasketId);
        Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto customerBasket);
        Task<bool> DeleteBasketAsync(string BasketId);
    }
}
