using Insightinator.API.Abstractions;
using System.Diagnostics;

namespace Insightinator.API.Middlewares;

public class MonitoringMiddleware : IMiddleware
{
    private readonly ILogger<MonitoringMiddleware> _logger;
    private readonly IServiceProvider _serviceProvider;
    private Stopwatch _stopwatch;

    private static double UpTimeSeconds =>
        (DateTime.UtcNow - Process.GetCurrentProcess().StartTime.ToUniversalTime()).TotalSeconds;

    public MonitoringMiddleware(
        ILogger<MonitoringMiddleware> logger,
        IServiceProvider serviceProvider
    )
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        _stopwatch = Stopwatch.StartNew();
        using var scope = _serviceProvider.CreateScope();
        var monitoringService = scope.ServiceProvider.GetRequiredService<IMonitoringService>();

        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception {@Ex}", ex);
            await Task.WhenAll(
                monitoringService.ComputeErrorCount(),
                monitoringService.ComputeErrroRate(UpTimeSeconds),
                monitoringService.ComputeErrorTypes(ex)
            );
            //await monitoringService.ComputeErrorCount();
            //await monitoringService.ComputeErrroRate(UpTimeSeconds);
            //await monitoringService.ComputeErrorTypes(ex);
            throw;
        }
        finally
        {
            _stopwatch.Stop();

            // HTTP request
            //await monitoringService.ComputeTotalRequestNumber();
            //await monitoringService.ComputeAvgRequestProcessingTime(_stopwatch.ElapsedMilliseconds);
            //await monitoringService.ComputeRequestsPerMinute(UpTimeSeconds);

            await Task.WhenAll(
                monitoringService.ComputeTotalRequestNumber(),
                monitoringService.ComputeAvgRequestProcessingTime(_stopwatch.ElapsedMilliseconds),
                monitoringService.ComputeRequestsPerMinute(UpTimeSeconds)
            );

            //HTTP response
            //await monitoringService.ComputeResponseCodeDistribution(
            //    context.Response.StatusCode.ToString()
            //);

            await Task.WhenAll(
                monitoringService.ComputeResponseCodeDistribution(
                    context.Response.StatusCode.ToString()
                )
            );
        }

        scope!.Dispose();
    }
}
