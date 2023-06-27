using Insightinator.API.Abstractions;
using Insightinator.API.Metrics;
using Insightinator.API.Metrics.Http.Request;
using MediatR;

namespace Insightinator.API.Handlers.Http.Request;

public class TotalRequestNumberRequest : IRequest<MetricResponse<long>> { }

public class TotalRequestNumberHandler
    : IRequestHandler<TotalRequestNumberRequest, MetricResponse<long>>
{
    private readonly IStoreService _storeService;

    public TotalRequestNumberHandler(IStoreService storeService)
    {
        _storeService = storeService;
    }

    public async Task<MetricResponse<long>> Handle(
        TotalRequestNumberRequest request,
        CancellationToken cancellationToken
    )
    {
        var metric = await _storeService.GetAsync<TotalRequestNumberMetric>(
            nameof(TotalRequestNumberMetric)
        );

        if (metric != null)
        {
            metric.Value++;
        }
        else
        {
            metric = TotalRequestNumberMetric.Build();
            metric.Value++;
        }

        await _storeService.SaveAsync(nameof(TotalRequestNumberMetric), metric);

        return new MetricResponse<long> { Value = metric.Value, Metric = metric };
    }
}
