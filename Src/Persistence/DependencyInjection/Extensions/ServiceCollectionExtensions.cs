using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Domain.Abstractions.Repositories.Base;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Persistence.DependencyInjection.Options;
using Persistence.Repositories;
using Persistence.Repositories.Base;

namespace Persistence.DependencyInjection.Extensions;
public static class ServiceCollectionExtensions
{
    public static void AddSqlConfiguration(this IServiceCollection services)
    {
        services.AddDbContextPool<DbContext, ApplicationDbContext>((provider, builder) =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            var options = provider.GetRequiredService<IOptions<SqlServerRetryOptions>>();

            builder
            .EnableDetailedErrors(true)
            .EnableSensitiveDataLogging(true)
            .UseLazyLoadingProxies(true) // => If UseLazyLoadingProxies, all of the navigation fields should be VIRTUAL
            .UseSqlServer(
                connectionString: configuration.GetConnectionString("ConnectionStrings"),
                sqlServerOptionsAction: optionsBuilder
                        => optionsBuilder.ExecutionStrategy(
                                dependencies => new SqlServerRetryingExecutionStrategy(
                                    dependencies: dependencies,
                                    maxRetryCount: options.Value.MaxRetryCount,
                                    maxRetryDelay: options.Value.MaxRetryDelay,
                                    errorNumbersToAdd: options.Value.ErrorNumbersToAdd))
                            .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.GetName().Name))
            //            .LogTo(Console.WriteLine, LogLevel.Information)
            ;
        });

        services.AddIdentityCore<AppUser>()
            .AddRoles<AppRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            // Lockout settings.
            options.Lockout.AllowedForNewUsers = true; // Default true
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2); // Default 5
            options.Lockout.MaxFailedAccessAttempts = 3; // Default 5

            // Password settings.
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;
            options.Lockout.AllowedForNewUsers = true;

            // User settings.
            options.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = false;
        });
    }

    public static void AddRepositoryBaseConfiguration(this IServiceCollection services)
        => services.AddTransient(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>))
            .AddTransient(typeof(IAuditRepositoryBase<,>), typeof(AuditRepositoryBase<,>))
            .AddScoped(typeof(IUnitOfWork), typeof(EFUnitOfWork))
            .AddScoped(typeof(IApplicationRepository), typeof(ApplicationRepository))
            .AddScoped(typeof(IPetRepository), typeof(PetRepository))
            .AddScoped(typeof(IScheduleRepository), typeof(ScheduleRepository))
            .AddScoped(typeof(IPetVaccinatedRepository), typeof(PetVaccinatedRepository))
            .AddScoped(typeof(IPetCenterRepository), typeof(PetCenterRepository))
            .AddScoped(typeof(IPetCenterServiceRepository), typeof(PetCenterServiceRepository))
            .AddScoped(typeof(IBreedRepository), typeof(BreedRepository))
            .AddScoped(typeof(IPhotoRepository), typeof(PhotoRepository))
            .AddScoped(typeof(IPetServiceRepository), typeof(PetServiceRepository))
            .AddScoped(typeof(IBreedRepository), typeof(BreedRepository))
            .AddScoped(typeof(ISpeciesRepository), typeof(SpeciesRepository))
            .AddScoped(typeof(IJobRepository), typeof(JobRepository))
            .AddScoped(typeof(IAppointmentRateRepository), typeof(AppointmentRateRepository))
            .AddScoped(typeof(IServiceAppointmentRepository), typeof(ServiceAppointmentRepository))
            .AddScoped(typeof(IReportRepository), typeof(ReportRepository))
            .AddScoped(typeof(ICertificationRepository), typeof(CertificationRepository))
            .AddScoped(typeof(ISpeciesJobRepository), typeof(SpeciesJobRepository))
            .AddScoped(typeof(IVaccineRepository), typeof(VaccineRepository))
            .AddScoped(typeof(IBreedAppointmentRepository), typeof(BreedAppointmentRepository))
            .AddScoped(typeof(ICenterBreedRepository), typeof(CenterBreedRepository))
            .AddScoped(typeof(IAppointmentRepository), typeof(AppointmentRepository))
            .AddScoped(typeof(ITransactionRepository), typeof(TransactionRepository))
            .AddScoped(typeof(ICenterBreedImageRepository), typeof(CenterBreedImageRepository))
            .AddScoped(typeof(ITrackingRepository), typeof(TrackingRepository))
            .AddScoped(typeof(IVaccineReccomendationRepository), typeof(VaccineReccomendationRepository))
            .AddScoped(typeof(IReportImageRepository), typeof(ReportImageRepository))
            .AddScoped(typeof(IPlaceRepository), typeof(PlaceRepository))
            ;

    public static OptionsBuilder<SqlServerRetryOptions> ConfigureSqlServerRetryOptions(this IServiceCollection services, IConfigurationSection section)
        => services.AddOptions<SqlServerRetryOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();
}
