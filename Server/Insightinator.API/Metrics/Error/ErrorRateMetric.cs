using Insightinator.API.Abstractions;

namespace Insightinator.API.Metrics.Error;

public sealed class ErrorRateMetric : IMetric<double>
{
    public static readonly string Id = Guid.NewGuid().ToString();
    public double Value { get; set; }
    public string Name { get; set; }
    public string Unit { get; set; }
    public string Description { get; set; }
    public Guid Key { get; set; }

    public static ErrorRateMetric Build()
    {
        return new ErrorRateMetric
        {
            Key = Guid.NewGuid(),
            Value = 0,
            Name = "Error rate",
            Description =
                "Shows errors rate. Average number of errors that are occured in one minute",
            Unit = "errors/minute"
        };
    }
}
