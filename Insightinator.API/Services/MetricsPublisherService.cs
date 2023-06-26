using Insightinator.API.Abstractions;
using Insightinator.API.Metrics.Http.Request;

namespace Insightinator.API.Services;

public class MetricsPublisherService : IMetricsPublisherService
{
    private readonly IStoreService _storeService;
    private readonly ILogger<MetricsPublisherService> _logger;

    public MetricsPublisherService(
        IStoreService storeService,
        ILogger<MetricsPublisherService> logger
    ) => (_storeService, _logger) = (storeService, logger);

    public async Task<IList<object>> PublishMetrics()
    {
        List<object> metrics = new();

        var avgRequestProcessingTimeMetric =
            await _storeService.GetAsync<AvgRequestProcessingTimeMetric>(
                AvgRequestProcessingTimeMetric.Id
            );

        var totalRequestNumberMetric = await _storeService.GetAsync<TotalRequestNumberMetric>(
            TotalRequestNumberMetric.Id
        );

        var requestsPerMinuteMetric = await _storeService.GetAsync<RequestsPerMinuteMetric>(
            RequestsPerMinuteMetric.Id
        );

        metrics.Add(avgRequestProcessingTimeMetric);
        metrics.Add(totalRequestNumberMetric);
        metrics.Add(requestsPerMinuteMetric);

        _logger.LogInformation("Metrics: {@Metrics}", metrics);

        return metrics;
    }
}
