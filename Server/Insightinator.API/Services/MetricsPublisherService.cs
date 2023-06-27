using Insightinator.API.Abstractions;
using Insightinator.API.Metrics.Error;
using Insightinator.API.Metrics.Http.Request;
using Insightinator.API.Metrics.Http.Response;

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

        var responseCodeDistribution = await _storeService.GetAsync<ResponseCodeDistributionMetric>(
            ResponseCodeDistributionMetric.Id
        );

        var errorRate = await _storeService.GetAsync<ErrroRateMetric>(ErrroRateMetric.Id);

        var errorType = await _storeService.GetAsync<ErrorTypesMetric>(ErrorTypesMetric.Id);

        metrics.Add(avgRequestProcessingTimeMetric);
        metrics.Add(totalRequestNumberMetric);
        metrics.Add(requestsPerMinuteMetric);
        metrics.Add(responseCodeDistribution);
        metrics.Add(errorRate);
        metrics.Add(errorType);

        _logger.LogInformation("Metrics: {@Metrics}", metrics);

        return metrics;
    }
}
