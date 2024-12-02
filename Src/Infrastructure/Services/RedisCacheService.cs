// Ignore Spelling: Redis

using Contract.Services.V1;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StackExchange.Redis;
using System.Text;

namespace Infrastructure.Service;
public class RedisCacheService : IRedisCacheService
{
    private readonly IDatabase redisDatabase;

    public RedisCacheService(IDatabase redisDatabase)
    {
        this.redisDatabase = redisDatabase;
    }

    public async Task<bool> IsUserRevokedAsync(string userId)
    {
        return await redisDatabase.KeyExistsAsync(userId);
    }
    public async Task<T> GetAsync<T>(string key)
    {
        return Deserialize<T>(await redisDatabase.StringGetAsync(key));
    }

    public async Task<object> GetAsync(string key)
    {
        return Deserialize<object>(await redisDatabase.StringGetAsync(key));
    }

    public async Task<string> GetCacheResponseAsync(string cacheKey)
    {
        var cacheResponse = await redisDatabase.StringGetAsync(cacheKey);
        return cacheResponse.IsNullOrEmpty ? null : cacheResponse.ToString();
    }

    public async Task<bool> SetAsync(string key, object value, TimeSpan? expiresIn = null)
    {
        if (value is null)
            return false;
        return await redisDatabase.StringSetAsync(key, Serialize(value), expiresIn);
    }

    public async Task RemoveCacheResponseAsync(string pattern)
    {
        if (string.IsNullOrWhiteSpace(pattern))
            throw new ArgumentNullException("Pattern cannot be null or whitespace");

        await foreach (var key in GetKeyAsync(pattern + "*"))
        {
            await redisDatabase.KeyDeleteAsync(key);
        }
    }



    private async IAsyncEnumerable<string> GetKeyAsync(string pattern)
    {
        foreach (var endPoint in redisDatabase.Multiplexer.GetEndPoints())
        {
            var server = redisDatabase.Multiplexer.GetServer(endPoint);
            foreach (var key in server.Keys(pattern: pattern))
            {
                yield return key.ToString();
            }
        }
    }

    private byte[] Serialize(object o)
    {
        byte[] objectDataAsStream = null;

        if (o != null)
        {
            var jsonString = JsonConvert.SerializeObject(o, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            objectDataAsStream = Encoding.ASCII.GetBytes(jsonString);
        }

        return objectDataAsStream;
    }

    private T Deserialize<T>(byte[] stream)
    {
        T result = default;

        if (stream != null)
        {
            var jsonString = Encoding.ASCII.GetString(stream);
            result = JsonConvert.DeserializeObject<T>(jsonString);
        }

        return result;
    }
}
