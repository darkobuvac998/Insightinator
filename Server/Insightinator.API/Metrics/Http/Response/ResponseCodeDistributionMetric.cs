using Insightinator.API.Abstractions;
using System.Collections.Concurrent;

namespace Insightinator.API.Metrics.Http.Response;

public class ResponseCodeDistributionMetric : IMetric<ConcurrentDictionary<string, int>>
{
    public static string Id => "ResponseCodeDistributionMetric";
    public ConcurrentDictionary<string, int> Value { get; set; }
    public string Name { get; set; }
    public string Unit { get; set; }
    public string Description { get; set; }

    public static ResponseCodeDistributionMetric Build()
    {
        return new ResponseCodeDistributionMetric
        {
            Value = new(),
            Name = "HTTP response code distribution",
            Unit =
                "Key/value pairs. Keys are HTTP response codes and values are number of returned request",
            Description = "Shows HTTP response code distribution"
        };
    }
}
