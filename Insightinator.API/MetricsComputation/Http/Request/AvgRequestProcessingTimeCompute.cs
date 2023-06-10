using Insightinator.API.Abstractions;
using Insightinator.API.Metrics.Http.Request;
using Newtonsoft.Json;
using System.Collections.Concurrent;

namespace Insightinator.API.MetricsComputation.Http.Request;

public sealed class AvgRequestProcessingTimeCompute
    : IMetricCompute<AvgRequestProcessingTime, double>
{
    public AvgRequestProcessingTime Metric { get; } =
        (AvgRequestProcessingTime)new AvgRequestProcessingTime().Initialize();

    private readonly ConcurrentBag<double> _requestProcessingTimes = new ConcurrentBag<double>();
    private readonly ILogger<AvgRequestProcessingTimeCompute> _logger;

    public AvgRequestProcessingTimeCompute(ILogger<AvgRequestProcessingTimeCompute> logger)
    {
        _logger = logger;
    }

    public void ComputeMetric()
    {
        var timeSum = _requestProcessingTimes.ToList()?.Sum();
        if (!_requestProcessingTimes.IsEmpty)
        {
            Metric.Value = timeSum / _requestProcessingTimes.Count ?? 0;
        }
    }

    public AvgRequestProcessingTime GetMetricValue()
    {
        _logger.LogInformation(
            "Request processing times: {0}",
            JsonConvert.SerializeObject(_requestProcessingTimes.ToList())
        );
        return Metric;
    }

    public void ResetMetric()
    {
        Metric.Value = default;
    }

    public void AddValueToBuffer(params double[] values)
    {
        foreach (var item in values)
        {
            _requestProcessingTimes.Add(item);
        }
    }
}
