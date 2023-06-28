using Insightinator.API.Abstractions;

namespace Insightinator.API.Metrics.Http.Request;

public class RequestsPerMinuteMetric : IMetric<double>
{
    public static readonly string Id = Guid.NewGuid().ToString();
    public double Value { get; set; }
    public string Name { get; set; }
    public string Unit { get; set; }
    public string Description { get; set; }
    public Guid Key { get; set; }

    public static RequestsPerMinuteMetric Build()
    {
        return new RequestsPerMinuteMetric
        {
            Key = Guid.NewGuid(),
            Value = 0,
            Name = "Avg number of HTTP request per minute",
            Description = "Shows average HTTP reqeust per munite",
            Unit = "Request Count"
        };
    }
}
