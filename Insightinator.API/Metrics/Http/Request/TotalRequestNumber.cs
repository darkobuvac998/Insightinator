using Insightinator.API.Abstractions;

namespace Insightinator.API.Metrics.Http.Request;

public sealed class TotalRequestNumber : IMetric<long>
{
    public long Value { get; set; }
    public string Name { get; set; }
    public string Unit { get; set; }
    public string Description { get; set; }

    public IMetric<long> Initialize()
    {
        return new TotalRequestNumber
        {
            Value = 0,
            Name = "Total number of requests",
            Unit = "Request Count",
            Description = "Request volume: This metric indicates the total number of HTTP requests",
        };
    }
}
