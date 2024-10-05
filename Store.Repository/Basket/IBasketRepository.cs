using Store.Repository.Basket.Models;

namespace Store.Repository.Basket
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(string BasketId);
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket customerBasket);
        Task<bool> DeleteBasketAsync(string BasketId);
    }
}
