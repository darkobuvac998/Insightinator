using Insightinator.API.Abstractions;
using Insightinator.API.Middlewares;
using Insightinator.API.Services;
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

        services.AddScoped<IStoreService, StoreService>();
        services.AddScoped<IConnectionMultiplexer>(
            opt => ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis"))
        );

        services.AddScoped<IMonitoringService, MonitoringService>();

        return services;
    }

    public static IServiceCollection RegisterHostedServices(this IServiceCollection services)
    {
        services.AddHostedService<RedisCleanupService>();

        return services;
    }
}
