using Insightinator.API.Abstractions;
using Insightinator.API.Extensions;
using Insightinator.API.Metrics;
using Insightinator.API.Metrics.Http.Request;
using MediatR;
using System.Collections.Concurrent;

namespace Insightinator.API.Handlers.Http.Request;

public record AvgRequestProcessingTimeRequest(double ProcessingTime)
    : IRequest<MetricResponse<double>>;

public sealed class AvgRequestProcessingTimeHandler
    : IRequestHandler<AvgRequestProcessingTimeRequest, MetricResponse<double>>
{
    private readonly IStoreService _storeService;
    private static ConcurrentBag<double> _processingTime = new();

    private static double AvgRequestProcessingTime =>
        (!_processingTime.IsEmpty ? _processingTime.Sum() / _processingTime.Count : 0).Round(2);

    public AvgRequestProcessingTimeHandler(IStoreService storeService) =>
        (_storeService) = (storeService);

    public async Task<MetricResponse<double>> Handle(
        AvgRequestProcessingTimeRequest request,
        CancellationToken cancellationToken
    )
    {
        _processingTime.Add(request.ProcessingTime);

        var metric = await _storeService.GetAsync<AvgRequestProcessingTimeMetric>(
            AvgRequestProcessingTimeMetric.Id
        );

        if (metric != null)
        {
            metric.Value = AvgRequestProcessingTime;
        }
        else
        {
            metric = AvgRequestProcessingTimeMetric.Build();
            metric.Value = AvgRequestProcessingTime;
        }

        await _storeService.SaveAsync(AvgRequestProcessingTimeMetric.Id, metric);

        return new MetricResponse<double> { Value = metric.Value, Metric = metric };
    }
}
