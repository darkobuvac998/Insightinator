using Insightinator.API.Abstractions;
using Insightinator.API.Metrics.Http.Request;
using Insightinator.API.MetricsComputation.Http.Request;
using Insightinator.API.Middlewares;

namespace Insightinator.API.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSingleton<MonitoringMiddleware>();
        services.AddTransient<
            IMetricCompute<TotalRequestNumber, long>,
            TotalRequestNumberCompute
        >();
        services.AddTransient<
            IMetricCompute<AvgRequestProcessingTime, double>,
            AvgRequestProcessingTimeCompute
        >();

        return services;
    }
}
