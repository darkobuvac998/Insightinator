using Insightinator.API.Abstractions;
using Insightinator.API.Metrics;
using Insightinator.API.Metrics.Error;
using MediatR;

namespace Insightinator.API.Handlers.Error;

public record ErrorTypesRequest(object Error) : IRequest<MetricResponse<Dictionary<string, int>>>;

public sealed class ErrorTypesHandler
    : IRequestHandler<ErrorTypesRequest, MetricResponse<Dictionary<string, int>>>
{
    private readonly IStoreService _storeService;

    public ErrorTypesHandler(IStoreService storeService) => _storeService = storeService;

    public async Task<MetricResponse<Dictionary<string, int>>> Handle(
        ErrorTypesRequest request,
        CancellationToken cancellationToken
    )
    {
        var metric = await _storeService.GetAsync<ErrorTypesMetric>(ErrorTypesMetric.Id);
        var errorType = request.Error.GetType().Name;

        if (metric is not null)
        {
            if (metric.Value.TryGetValue(errorType, out int count))
            {
                metric.Value[errorType] = count + 1;
            }
            else
            {
                metric.Value[errorType] = 1;
            }
        }
        else
        {
            metric = ErrorTypesMetric.Build();
            metric.Value[errorType] = 1;
        }

        await _storeService.SaveAsync(ErrorTypesMetric.Id, metric);

        return new MetricResponse<Dictionary<string, int>>
        {
            Metric = metric,
            Value = metric.Value
        };
    }
}
