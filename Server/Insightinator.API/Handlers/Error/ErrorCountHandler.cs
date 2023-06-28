using Insightinator.API.Abstractions;
using Insightinator.API.Metrics;
using Insightinator.API.Metrics.Error;
using MediatR;

namespace Insightinator.API.Handlers.Error;

public record ErrorCountRequest() : IRequest<MetricResponse<long>>;

public class ErrorCountHandler : IRequestHandler<ErrorCountRequest, MetricResponse<long>>
{
    private readonly IStoreService _storeService;

    public ErrorCountHandler(IStoreService storeService)
    {
        _storeService = storeService;
    }

    public async Task<MetricResponse<long>> Handle(
        ErrorCountRequest request,
        CancellationToken cancellationToken
    )
    {
        var metric = await _storeService.GetAsync<ErrorCountMetric>(ErrorCountMetric.Id);

        if (metric is not null)
        {
            metric.Value++;
        }
        else
        {
            metric = ErrorCountMetric.Build();
            metric.Value++;
        }

        await _storeService.SaveAsync(ErrorCountMetric.Id, metric);

        return new MetricResponse<long> { Value = metric.Value, Metric = metric };
    }
}
