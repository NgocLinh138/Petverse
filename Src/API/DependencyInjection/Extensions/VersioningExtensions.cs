using MicroElements.Swashbuckle.FluentValidation.AspNetCore;

namespace API.DependencyInjection.Extensions;

public static class VersioningExtensions
{
    public static void AddVersioningConfiguration(this IServiceCollection services)
    {
        services
            .AddSwaggerGenNewtonsoftSupport()
            .AddFluentValidationRulesToSwagger()
            .AddEndpointsApiExplorer()
            .AddSwagger();
        services
            .AddApiVersioning(options => options.ReportApiVersions = true)
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
    }
}
