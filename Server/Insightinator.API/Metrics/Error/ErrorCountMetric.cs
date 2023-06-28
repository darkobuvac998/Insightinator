using Insightinator.API.Abstractions;

namespace Insightinator.API.Metrics.Error;

public sealed class ErrorCountMetric : IMetric<long>
{
    public static readonly string Id = Guid.NewGuid().ToString();
    public Guid Key { get; set; }
    public long Value { get; set; }
    public string Name { get; set; }
    public string Unit { get; set; }
    public string Description { get; set; }

    public static ErrorCountMetric Build()
    {
        return new ErrorCountMetric
        {
            Value = 0,
            Name = "Error count",
            Unit = "errors",
            Description = "Shows total number of errors occured",
            Key = Guid.NewGuid(),
        };
    }
}
