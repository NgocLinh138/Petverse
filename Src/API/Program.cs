using API.DependencyInjection.Extensions;
using API.Middleware;
using Application.DependencyInjection.Extensions;
using Application.Usecases.V1.VaccineRecommendation.Commands;
using Hangfire;
using Infrastructure.BlobStorage.DependencyInjection.Extensions;
using Infrastructure.BlobStorage.DependencyInjection.Options;
using Infrastructure.DependencyInjection.Extensions;
using Infrastructure.DependencyInjection.Options;
using Infrastructure.PayOS.DependencyInjection.Extensions;
using Infrastructure.PayOS.DependencyInjection.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Persistence.DependencyInjection.Extensions;
using Persistence.DependencyInjection.Options;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

JsonConvert.DefaultSettings = () => new JsonSerializerSettings
{
    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
    NullValueHandling = NullValueHandling.Include,
    ContractResolver = new DefaultContractResolver()
};

// Hangfire
builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString("ConnectionStrings")));
builder.Services.AddHangfireServer();
builder.Services.AddScoped<IBackgroundJobClient, BackgroundJobClient>();
builder.Services.AddScoped<IRecurringJobManager, RecurringJobManager>();

// Add Serilog
Log.Logger = new LoggerConfiguration().ReadFrom
    .Configuration(builder.Configuration)
    .CreateLogger();
builder.Logging
    .ClearProviders()
    .AddSerilog();
builder.Host.UseSerilog();

// API
builder.Services.AddControllers()
    .AddApplicationPart(Presentation.AssemblyReference.Assembly);

GlobalConfiguration.Configuration
    .UseSerializerSettings(new JsonSerializerSettings
    {
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        NullValueHandling = NullValueHandling.Include,
        ContractResolver = new DefaultContractResolver()
    });

builder.Services.AddJwtAuthentication(builder.Configuration);

// Versioning
builder.Services.AddVersioningConfiguration();

//Application
builder.Services.AddConfigureMediatR();
builder.Services.AddConfigurationMapper();

//Infrastructure
builder.Services.AddInfrastructureServices();
builder.Services.AddConfigRedis(
    builder.Configuration.GetSection(nameof(RedisOptions)));

//Infrastructure.BlobStorage
builder.Services.AddConfigInfrastructureBlobStorage();
builder.Services.ConfigureBlobStorageOptions(
    builder.Configuration.GetSection(nameof(BlobStorageOptions)));

//Infrastructure.PayOS
builder.Services.AddConfigInfrastructurePayOS();
builder.Services.ConfigurePayOSOptions(
    builder.Configuration.GetSection(nameof(PayOSOptions)));

// Persistence
builder.Services.AddSqlConfiguration();
builder.Services.AddRepositoryBaseConfiguration();
builder.Services.ConfigureSqlServerRetryOptions(
    builder.Configuration.GetSection(nameof(SqlServerRetryOptions)));

builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddTransient<RevokeTokenHandlingMiddleware>();

builder.Services.AddCors(options
    => options.AddDefaultPolicy(policyBuilder
        => policyBuilder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
            ));

var app = builder.Build();

app.UseHangfireDashboard();


using (var scope = app.Services.CreateScope())
{
    try
    {
        var jobClient = scope.ServiceProvider.GetRequiredService<IBackgroundJobClient>();

        RecurringJob.AddOrUpdate<UpdateVaccineRecommendationCommandHandler>(
            "update-vaccine-recommendations",
            handler => handler.UpdateVaccineRecommendationsForAllPets(),
            Cron.Daily
        );
    }
    catch (Exception ex)
    {
        Log.Error(ex, "Error while scheduling background job");
    }
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<RevokeTokenHandlingMiddleware>();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment() || builder.Environment.IsStaging())
app.ConfigureSwagger();

app.UseCors();
app.UseHttpsRedirection();
app.UseSerilogRequestLogging();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

try
{
    await app.RunAsync();
    Log.Information("Stopped cleanly");
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occurred during bootstrapping");
    await app.StopAsync();
}
finally
{
    Log.CloseAndFlush();
    await app.DisposeAsync();
}
