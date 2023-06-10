using Insightinator.API.Abstractions;

namespace Insightinator.API.Metrics.Http.Request;

public class AvgRequestProcessingTime : IMetric<double>
{
    public double Value { get; set; }
    public string Name { get; set; }
    public string Unit { get; set; }
    public string Description { get; set; }

    public IMetric<double> Initialize()
    {
        return new AvgRequestProcessingTime
        {
            Value = 0,
            Name = "Average http request processing time",
            Unit = "ms",
            Description = "Shows average time for processing http request"
        };
    }
}
