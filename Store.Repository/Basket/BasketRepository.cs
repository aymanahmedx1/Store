using Store.Repository.Basket.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace Store.Repository.Basket
{
    public class BasketRepository : IBasketRepository
    {

        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string BasketId)
        => await _database.KeyDeleteAsync(BasketId);
        public  async Task<CustomerBasket> GetBasketAsync(string BasketId)
        {
            var basket =await _database.StringGetAsync(BasketId);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket customerBasket)
        {
            var created =await _database.StringSetAsync(customerBasket.Id, JsonSerializer.Serialize(customerBasket), TimeSpan.FromDays(60));
            if (!created)
                return null;
            return await GetBasketAsync(customerBasket.Id);
        }
    }
}
