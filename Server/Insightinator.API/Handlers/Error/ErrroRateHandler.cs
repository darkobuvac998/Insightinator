using Insightinator.API.Abstractions;
using Insightinator.API.Extensions;
using Insightinator.API.Metrics;
using Insightinator.API.Metrics.Error;
using MediatR;
using System.Collections.Concurrent;

namespace Insightinator.API.Handlers.Error;

public record ErrroRateRequest(double UpTime)
    : IRequest<MetricResponse<ConcurrentDictionary<string, double>>>;

public class ErrroRateHandler
    : IRequestHandler<ErrroRateRequest, MetricResponse<ConcurrentDictionary<string, double>>>
{
    private readonly IStoreService _storeService;

    public ErrroRateHandler(IStoreService storeService) => _storeService = storeService;

    public async Task<MetricResponse<ConcurrentDictionary<string, double>>> Handle(
        ErrroRateRequest request,
        CancellationToken cancellationToken
    )
    {
        var metric = await _storeService.GetAsync<ErrroRateMetric>(ErrroRateMetric.Id);
        var upTimeMinutes = request.UpTime / 60;

        if (metric is not null)
        {
            if (metric.Value.TryGetValue(Constants.ErrorsConstants.ErrorCount, out double count))
            {
                metric.Value.AddOrUpdate(
                    Constants.ErrorsConstants.ErrorCount,
                    0,
                    (key, value) => count + 1
                );
            }

            if (metric.Value.TryGetValue(Constants.ErrorsConstants.ErrorRate, out double rate))
            {
                var newRate = upTimeMinutes > 0 ? count / upTimeMinutes : 0;
                metric.Value.AddOrUpdate(
                    Constants.ErrorsConstants.ErrorRate,
                    0,
                    (key, value) => newRate.Round(2)
                );
            }

            metric.Value.AddOrUpdate(
                Constants.UpTime,
                upTimeMinutes,
                (key, value) => upTimeMinutes
            );
        }
        else
        {
            metric = ErrroRateMetric.Build();
            metric.Value.AddOrUpdate(
                Constants.UpTime,
                upTimeMinutes,
                (key, value) => upTimeMinutes
            );
            metric.Value[Constants.ErrorsConstants.ErrorCount] = 1;
            metric.Value[Constants.ErrorsConstants.ErrorRate] = (
                upTimeMinutes > 0
                    ? metric.Value[Constants.ErrorsConstants.ErrorCount] / upTimeMinutes
                    : 0
            ).Round(2);
        }

        await _storeService.SaveAsync(ErrroRateMetric.Id, metric);

        return new MetricResponse<ConcurrentDictionary<string, double>>
        {
            Metric = metric,
            Value = metric.Value
        };
    }
}
