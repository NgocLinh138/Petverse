using Application.Behaviors;
using Application.Mapper;
using Application.Usecases.V1.VaccineRecommendation.Commands;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfigureMediatR(this IServiceCollection services)
        => services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly))
        .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>))
        .AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionPipelineBehavior<,>))
        .AddTransient<UpdateVaccineRecommendationCommandHandler>()
        //.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformancePipelineBehavior<,>))
        //.AddTransient(typeof(IPipelineBehavior<,>), typeof(TracingPipelineBehavior<,>))
        .AddValidatorsFromAssembly(Contract.AssemblyReference.Assembly, includeInternalTypes: true);

    public static IServiceCollection AddConfigurationMapper(this IServiceCollection services)
        => services.AddAutoMapper(typeof(ServiceProfile));
}
