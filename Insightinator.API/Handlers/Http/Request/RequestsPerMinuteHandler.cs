using Insightinator.API.Abstractions;
using Insightinator.API.Metrics;
using Insightinator.API.Metrics.Http.Request;
using MediatR;

namespace Insightinator.API.Handlers.Http.Request;

public record RequestsPerMinuteRequest(double upTimeSeconds) : IRequest<MetricResponse<double>>;

public sealed class RequestsPerMinuteHandler
    : IRequestHandler<RequestsPerMinuteRequest, MetricResponse<double>>
{
    private readonly IStoreService _storeService;

    public RequestsPerMinuteHandler(IStoreService storeService) => _storeService = storeService;

    public async Task<MetricResponse<double>> Handle(
        RequestsPerMinuteRequest request,
        CancellationToken cancellationToken
    )
    {
        var elapsedMinutes = request.upTimeSeconds / 60;
        var metric = await _storeService.GetAsync<RequestsPerMinuteMetric>(
            RequestsPerMinuteMetric.Id
        );
        var totalRequestNumber = await _storeService.GetAsync<TotalRequestNumberMetric>(
            TotalRequestNumberMetric.Id
        );

        if (metric is not null)
        {
            metric.Value =
                (elapsedMinutes > 0 ? totalRequestNumber?.Value / elapsedMinutes : 0) ?? 0;

            await _storeService.SaveAsync(RequestsPerMinuteMetric.Id, metric);
        }
        else
        {
            metric = RequestsPerMinuteMetric.Build();
            metric.Value =
                (elapsedMinutes > 0 ? totalRequestNumber?.Value / elapsedMinutes : 0) ?? 0;

            await _storeService.SaveAsync(RequestsPerMinuteMetric.Id, metric);
        }

        return new MetricResponse<double> { Value = metric.Value, Metric = metric };
    }
}
