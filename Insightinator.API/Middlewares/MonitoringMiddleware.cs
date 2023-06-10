using Insightinator.API.Abstractions;
using Insightinator.API.Metrics.Http.Request;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Insightinator.API.Middlewares;

public class MonitoringMiddleware : IMiddleware
{
    private readonly ILogger<MonitoringMiddleware> _logger;
    private readonly IMetricCompute<TotalRequestNumber, long> _metricCompute;
    private readonly IMetricCompute<AvgRequestProcessingTime, double> _avgRequestProcessingTime;

    private Stopwatch _stopwatch;

    public MonitoringMiddleware(
        ILogger<MonitoringMiddleware> logger,
        IMetricCompute<TotalRequestNumber, long> metricCompute,
        IMetricCompute<AvgRequestProcessingTime, double> avgRequestProcessingTime
    )
    {
        _logger = logger;
        _metricCompute = metricCompute;
        _avgRequestProcessingTime = avgRequestProcessingTime;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        _stopwatch = Stopwatch.StartNew();

        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception @{ex}", ex);
        }
        finally
        {
            _stopwatch.Stop();

            _avgRequestProcessingTime.AddValueToBuffer(_stopwatch.ElapsedMilliseconds);
            _avgRequestProcessingTime.ComputeMetric();

            var avgMetric = _avgRequestProcessingTime.GetMetricValue();
            _logger.LogInformation(
                "Avg request processing time: @{metric}",
                JsonConvert.SerializeObject(avgMetric)
            );

            _metricCompute.ComputeMetric();
            var metric = _metricCompute.GetMetricValue();
            _logger.LogInformation(
                "Total number of requests: @{metric}",
                JsonConvert.SerializeObject(metric)
            );
        }
    }
}
