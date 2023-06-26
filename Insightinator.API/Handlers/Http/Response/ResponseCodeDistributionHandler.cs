using Insightinator.API.Abstractions;
using Insightinator.API.Metrics;
using Insightinator.API.Metrics.Http.Response;
using MediatR;
using System.Collections.Concurrent;

namespace Insightinator.API.Handlers.Http.Response;

public record ResponseCodeDistributionRequest(string ResponseCode)
    : IRequest<MetricResponse<ConcurrentDictionary<string, int>>>;

public class ResponseCodeDistributionHandler
    : IRequestHandler<
        ResponseCodeDistributionRequest,
        MetricResponse<ConcurrentDictionary<string, int>>
    >
{
    private readonly IStoreService _storeService;

    public ResponseCodeDistributionHandler(IStoreService storeService) =>
        _storeService = storeService;

    public async Task<MetricResponse<ConcurrentDictionary<string, int>>> Handle(
        ResponseCodeDistributionRequest request,
        CancellationToken cancellationToken
    )
    {
        var metric = await _storeService.GetAsync<ResponseCodeDistributionMetric>(
            ResponseCodeDistributionMetric.Id
        );

        if (metric != null)
        {
            if (metric.Value?.TryGetValue(request.ResponseCode, out int count) ?? false)
            {
                metric.Value?.TryUpdate(request.ResponseCode, count + 1, count);
            }
        }
        else
        {
            metric = ResponseCodeDistributionMetric.Build();
            metric.Value?.AddOrUpdate(request.ResponseCode, 1, (code, value) => value);
        }

        await _storeService.SaveAsync(ResponseCodeDistributionMetric.Id, metric);

        return new MetricResponse<ConcurrentDictionary<string, int>>
        {
            Value = metric.Value!,
            Metric = metric
        };
    }
}
