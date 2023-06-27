using Insightinator.API.Abstractions;

namespace Insightinator.API.Metrics;

public sealed class MetricResponse<T>
{
    public T Value { get; set; }
    public IMetric<T> Metric { get; set; }
}
