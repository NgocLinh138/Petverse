using Contract.Services.V1;
using Infrastructure.DependencyInjection.Options;
using Infrastructure.Service;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Infrastructure.DependencyInjection.Extensions;
public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureServices(this IServiceCollection services)
        => services.AddTransient<IJWTTokenService, JWTTokenService>()
            .AddSingleton<IH3Service, H3Service>();

    public static IServiceCollection AddConfigRedis(this IServiceCollection services, IConfigurationSection section)
    {
        var redisOptions = new RedisOptions();
        section.Bind(redisOptions);
        services.AddSingleton(redisOptions);

        var options = ConfigurationOptions.Parse(redisOptions.ConnectionString);
        options.SyncTimeout = 10000; // Tăng thời gian timeout đồng bộ lên 10 giây
        options.AsyncTimeout = 10000; // Tăng thời gian timeout bất đồng bộ lên 10 giây
        options.ConnectRetry = 5;    // Số lần thử lại kết nối
        options.AbortOnConnectFail = false; // Không hủy kết nối khi thất bại
        options.KeepAlive = 60;      // Giữ kết nối sống trong 60 giây

        var connectionMultiplexer = ConnectionMultiplexer.Connect(options);

        services.AddSingleton<IConnectionMultiplexer>(connectionMultiplexer);
        services.AddSingleton<IDatabase>(connectionMultiplexer.GetDatabase());
        services.AddStackExchangeRedisCache(options => options.Configuration = redisOptions.ConnectionString);

        services.AddSingleton<IRedisCacheService, RedisCacheService>();
        return services;
    }
}
