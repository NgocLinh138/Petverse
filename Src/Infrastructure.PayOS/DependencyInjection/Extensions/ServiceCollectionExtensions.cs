using Infrastructure.PayOS.DependencyInjection.Options;
using Infrastructure.PayOS.Service;
using Infrastructure.PayOS.Service.IService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.PayOS.DependencyInjection.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfigInfrastructurePayOS(this IServiceCollection services)
         => services.AddTransient<IPayOSService, PayOSService>();

    public static OptionsBuilder<PayOSOptions> ConfigurePayOSOptions(this IServiceCollection services, IConfigurationSection section)
        => services.AddOptions<PayOSOptions>()
                       .Bind(section)
                       .ValidateOnStart();
}
