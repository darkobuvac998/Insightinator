using Insightinator.API.Abstractions;
using Insightinator.API.Metrics.Http.Request;

namespace Insightinator.API.MetricsComputation.Http.Request;

public class TotalRequestNumberCompute : IMetricCompute<TotalRequestNumber, long>
{
    public TotalRequestNumber Metric { get; } =
        (TotalRequestNumber)new TotalRequestNumber().Initialize();

    public void ComputeMetric()
    {
        Metric.Value++;
    }

    public TotalRequestNumber GetMetricValue()
    {
        return Metric;
    }

    public void ResetMetric()
    {
        Metric.Value = default;
    }
}
