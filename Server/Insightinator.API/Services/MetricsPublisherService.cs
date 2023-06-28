using AutoMapper;
using Insightinator.API.Abstractions;
using Insightinator.API.Metrics;
using Insightinator.API.Metrics.Error;
using Insightinator.API.Metrics.Http.Request;
using Insightinator.API.Metrics.Http.Response;

namespace Insightinator.API.Services;

public class MetricsPublisherService : IMetricsPublisherService
{
    private readonly IStoreService _storeService;
    private readonly IMapper _mapper;

    public MetricsPublisherService(IStoreService storeService, IMapper mapper)
    {
        _storeService = storeService;
        _mapper = mapper;
    }

    public async Task<MetricsGroup> PublishHttpRequestMetrics()
    {
        List<object> metrics = new();

        var avgRequestProcessingTimeMetric = _storeService.GetAsync<AvgRequestProcessingTimeMetric>(
            AvgRequestProcessingTimeMetric.Id
        );

        var totalRequestNumberMetric = _storeService.GetAsync<TotalRequestNumberMetric>(
            TotalRequestNumberMetric.Id
        );

        var requestsPerMinuteMetric = _storeService.GetAsync<RequestsPerMinuteMetric>(
            RequestsPerMinuteMetric.Id
        );

        await Task.WhenAll(
            avgRequestProcessingTimeMetric,
            totalRequestNumberMetric,
            requestsPerMinuteMetric
        );

        metrics.Add(avgRequestProcessingTimeMetric.Result!);
        metrics.Add(totalRequestNumberMetric.Result!);
        metrics.Add(requestsPerMinuteMetric.Result!);

        return new MetricsGroup
        {
            Metrics = metrics,
            Name = "HTTP request metrics",
            Key = "HTTP request metrics"
        };
    }

    public async Task<MetricsGroup> PublishErrorMetrics()
    {
        List<object> metrics = new();

        var errorRate = _storeService.GetAsync<ErrorRateMetric>(ErrorRateMetric.Id);
        var errorCount = _storeService.GetAsync<ErrorCountMetric>(ErrorCountMetric.Id);

        await Task.WhenAll(errorRate, errorCount);

        metrics.Add(errorRate.Result!);
        metrics.Add(errorCount.Result!);

        return new MetricsGroup
        {
            Name = "Error metrics",
            Metrics = metrics,
            Key = "Error metrics"
        };
    }

    public async Task<IList<MetricsGroup>> PublishMetricGroups()
    {
        var httpMetrics = PublishHttpRequestMetrics();
        var errorMetrics = PublishErrorMetrics();
        var httpResponseMetrics = PublishHttpResponseMetrics();

        await Task.WhenAll(httpMetrics, errorMetrics, httpResponseMetrics);

        return new List<MetricsGroup>
        {
            httpMetrics.Result!,
            errorMetrics.Result!,
            httpResponseMetrics.Result!
        };
    }

    public async Task<MetricsGroup> PublishHttpResponseMetrics()
    {
        List<object> metrics = new();

        var httpResponseMetric = await _storeService.GetAsync<ResponseCodeDistributionMetric>(
            ResponseCodeDistributionMetric.Id
        );

        var result = _mapper.Map<ResponseCodeDistributionMetricDto>(httpResponseMetric);

        metrics.Add(result!);

        return new MetricsGroup
        {
            Name = "Http response metrics",
            Key = "Http response metrics",
            IsChartMetric = true,
            Metrics = metrics
        };
    }
}
