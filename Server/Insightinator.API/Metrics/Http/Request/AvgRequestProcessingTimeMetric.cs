﻿using Insightinator.API.Abstractions;

namespace Insightinator.API.Metrics.Http.Request;

public class AvgRequestProcessingTimeMetric : IMetric<double>
{
    public static readonly string Id = Guid.NewGuid().ToString();
    public double Value { get; set; }
    public string Name { get; set; }
    public string Unit { get; set; }
    public string Description { get; set; }
    public Guid Key { get; set; }

    public static AvgRequestProcessingTimeMetric Build()
    {
        return new AvgRequestProcessingTimeMetric
        {
            Key = Guid.NewGuid(),
            Value = 0,
            Name = "Average http request processing time",
            Unit = "ms",
            Description = "Shows average time for processing http request"
        };
    }
}
