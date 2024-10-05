namespace Store.Service.CashingService
{
    public interface ICashService
    {
        Task<string> GetCashResponseAsync(string key);
        Task SetCashResponseAsync(string key , object response , TimeSpan timeToLive);
    }
}
