using System.Text.Json;

using StackExchange.Redis;

namespace Store.Service.CashingService
{
    public class CashService : ICashService
    {
        private readonly IDatabase _database;

        public CashService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }


        public async Task<string> GetCashResponseAsync(string key)
        {
            var cashedResponse = await _database.StringGetAsync(key);
            if (cashedResponse.IsNullOrEmpty)
                return null;
            return cashedResponse.ToString();

        }

        public async Task SetCashResponseAsync(string key, object response, TimeSpan timeToLive)
        {
            if (response == null)
                return;
            var option = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(response, option);
            await _database.StringSetAsync(key, json, timeToLive);
        }
    }
}
