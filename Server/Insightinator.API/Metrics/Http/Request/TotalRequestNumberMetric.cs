﻿using Insightinator.API.Abstractions;

namespace Insightinator.API.Metrics.Http.Request;

public sealed class TotalRequestNumberMetric : IMetric<long>
{
    public static readonly string Id = Guid.NewGuid().ToString();
    public long Value { get; set; }
    public string Name { get; set; }
    public string Unit { get; set; }
    public string Description { get; set; }
    public Guid Key { get; set; }

    public static TotalRequestNumberMetric Build()
    {
        return new TotalRequestNumberMetric
        {
            Key = Guid.NewGuid(),
            Value = 0,
            Name = "Total number of requests",
            Unit = "Request Count",
            Description = "Request volume: This metric indicates the total number of HTTP requests",
        };
    }
}
