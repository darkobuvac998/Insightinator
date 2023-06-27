using Insightinator.API.Abstractions;
using Insightinator.API.Middlewares;
using Insightinator.API.OptionsSetup;
using Insightinator.API.Services;
using Insightinator.API.Services.Background;
using StackExchange.Redis;

namespace Insightinator.API.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddMediatR(
            config => config.RegisterServicesFromAssembly(AssemblyReference.Assembly)
        );

        services.AddSingleton<MonitoringMiddleware>();
        services.AddSingleton<ErrorHandlingMiddleware>();

        services.AddScoped<IStoreService, StoreService>();
        services.AddScoped<IConnectionMultiplexer>(
            opt => ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis"))
        );

        services.AddScoped<IMonitoringService, MonitoringService>();
        services.AddScoped<IMetricsPublisherService, MetricsPublisherService>();

        services
            .AddSignalR(opt =>
            {
                opt.EnableDetailedErrors = true;
                opt.HandshakeTimeout = TimeSpan.FromSeconds(5);
            })
            .AddNewtonsoftJsonProtocol(
                opt =>
                    opt.PayloadSerializerSettings = new Newtonsoft.Json.JsonSerializerSettings
                    {
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    }
            );

        services.ConfigureOptions<PublishingOptionsSetup>();

        services.AddRouting(opt => opt.LowercaseUrls = true);

        services.AddCors(
            opt =>
                opt.AddPolicy(
                    "CorsPolicy",
                    builder =>
                    {
                        builder
                            .WithOrigins("https://localhost:5544")
                            .AllowAnyHeader()
                            .WithMethods("GET", "PUT", "POST", "DELETE", "PATCH", "OPTIONS")
                            .AllowCredentials()
                        //.AllowAnyMethod()
                        ;
                    }
                )
        );

        return services;
    }

    public static IServiceCollection RegisterHostedServices(this IServiceCollection services)
    {
        services.AddHostedService<RedisCleanupService>();
        services.AddHostedService<PubliserBackgroundService>();

        return services;
    }
}
