using Insightinator.API.Abstractions;
using System.Collections.Concurrent;

namespace Insightinator.API.Metrics.Error;

public sealed class ErrroRateMetric : IMetric<ConcurrentDictionary<string, double>>
{
    public static readonly string Id = "ErrroRateMetric";

    public ConcurrentDictionary<string, double> Value { get; set; }
    public string Name { get; set; }
    public string Unit { get; set; }
    public string Description { get; set; }

    public static ErrroRateMetric Build()
    {
        return new ErrroRateMetric
        {
            Value = InitializeValue(),
            Name = "ErrroRateMetric",
            Description = "Represents reate of errors that are occured in minute",
            Unit = "#"
        };
    }

    private static ConcurrentDictionary<string, double> InitializeValue()
    {
        var result = new ConcurrentDictionary<string, double>();
        result[Constants.ErrorsConstants.ErrorCount] = 0;
        result[Constants.ErrorsConstants.ErrorRate] = 0;

        return result;
    }
}
