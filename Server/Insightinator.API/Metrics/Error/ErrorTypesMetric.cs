using Insightinator.API.Abstractions;

namespace Insightinator.API.Metrics.Error;

public sealed class ErrorTypesMetric : IMetric<Dictionary<string, int>>
{
    public static readonly string Id = Guid.NewGuid().ToString();
    public Dictionary<string, int> Value { get; set; }
    public string Name { get; set; }
    public string Unit { get; set; }
    public string Description { get; set; }

    public Guid Key { get; set; }

    public static ErrorTypesMetric Build()
    {
        return new ErrorTypesMetric
        {
            Key = Guid.NewGuid(),
            Value = new(),
            Name = "ErrorTypesMetric",
            Description = "Shows type of errors that occured with number",
            Unit = ""
        };
    }
}
