namespace Insightinator.API.Metrics;

public sealed class MetricsGroup
{
    public string Key { get; set; }
    public string Name { get; set; }
    public bool IsChartMetric { get; set; } = false;
    public IList<object> Metrics { get; set; }
}
