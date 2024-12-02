namespace Contract.Services.V1;
public interface IRedisCacheService
{
    Task<T> GetAsync<T>(string key);
    Task<object> GetAsync(string key);
    Task<string> GetCacheResponseAsync(string cacheKey);
    Task<bool> SetAsync(string key, object value, TimeSpan? expiresIn);
    Task RemoveCacheResponseAsync(string partern);

    Task<bool> IsUserRevokedAsync(string userId);
}
