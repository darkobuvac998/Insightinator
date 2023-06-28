using Insightinator.API.Abstractions;
using System.Collections.Concurrent;

namespace Insightinator.API.Metrics.Http.Response;

public class ResponseCodeDistributionMetric : IMetric<ConcurrentDictionary<string, int>>
{
    public static readonly string Id = Guid.NewGuid().ToString();
    public ConcurrentDictionary<string, int> Value { get; set; }
    public string Name { get; set; }
    public string Unit { get; set; }
    public string Description { get; set; }
    public Guid Key { get; set; }

    public static ResponseCodeDistributionMetric Build()
    {
        return new ResponseCodeDistributionMetric
        {
            Key = Guid.NewGuid(),
            Value = new(),
            Name = "HTTP response code distribution",
            Unit =
                "Key/value pairs. Keys are HTTP response codes and values are number of returned request",
            Description = "Shows HTTP response code distribution"
        };
    }

    public IList<HttpResponseCodeCount> ToList() =>
        Value.Any()
            ? Value.Keys.Select(k => new HttpResponseCodeCount(k, Value[k])).ToList()
            : Enumerable.Empty<HttpResponseCodeCount>().ToList();
}

public class ResponseCodeDistributionMetricDto
{
    public Guid Key { get; set; }
    public IList<HttpResponseCodeCount> Value { get; set; }
    public string Name { get; set; }
    public string Unit { get; set; }
    public string Description { get; set; }
}

public record HttpResponseCodeCount(string Code, int Count);
