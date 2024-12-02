using Infrastructure.BlobStorage.DependencyInjection.Options;
using Infrastructure.BlobStorage.Services;
using Infrastructure.BlobStorage.Services.Abstraction;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.BlobStorage.DependencyInjection.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfigInfrastructureBlobStorage(this IServiceCollection services)
        => services.AddTransient(typeof(IBlobStorageService), typeof(BlobStorageService));
    public static OptionsBuilder<BlobStorageOptions> ConfigureBlobStorageOptions(this IServiceCollection services, IConfigurationSection section)
        => services.AddOptions<BlobStorageOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();
}
