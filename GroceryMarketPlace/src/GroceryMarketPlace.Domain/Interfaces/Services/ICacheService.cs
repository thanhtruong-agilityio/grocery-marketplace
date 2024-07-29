namespace GroceryMarketPlace.Domain.Interfaces.Services
{
    public interface ICacheService
    {
        Task<T?> GetDataAsync<T>(string key);
        Task SetDataAsync<T>(string key, T value);
        Task RemoveCachedDataByKeyAsync(string key);
        Task RemoveCachedDataByKeyPrefixAsync(string prefix);
    }
}
