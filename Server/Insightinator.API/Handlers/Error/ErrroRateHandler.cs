using Insightinator.API.Abstractions;
using Insightinator.API.Extensions;
using Insightinator.API.Metrics;
using Insightinator.API.Metrics.Error;
using MediatR;

namespace Insightinator.API.Handlers.Error;

public record ErrroRateRequest(double UpTime) : IRequest<MetricResponse<double>>;

public class ErrroRateHandler : IRequestHandler<ErrroRateRequest, MetricResponse<double>>
{
    private readonly IStoreService _storeService;

    public ErrroRateHandler(IStoreService storeService) => _storeService = storeService;

    public async Task<MetricResponse<double>> Handle(
        ErrroRateRequest request,
        CancellationToken cancellationToken
    )
    {
        var metric = await _storeService.GetAsync<ErrorRateMetric>(ErrorRateMetric.Id);
        var metricCount = await _storeService.GetAsync<ErrorCountMetric>(ErrorCountMetric.Id);
        var upTimeMinutes = request.UpTime / 60;

        if (metric is not null)
        {
            var newRate = (upTimeMinutes != 0 ? metricCount!.Value / upTimeMinutes : 0).Round(2);
            metric.Value = newRate;
        }
        else
        {
            metric = ErrorRateMetric.Build();
            metric.Value = (
                upTimeMinutes != 0 ? (metricCount?.Value ?? 0) / upTimeMinutes : 0
            ).Round(2);
        }

        await _storeService.SaveAsync(ErrorRateMetric.Id, metric);

        return new MetricResponse<double> { Metric = metric, Value = metric.Value };
    }
}
