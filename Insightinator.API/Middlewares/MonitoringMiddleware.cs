using Insightinator.API.Handlers.Http.Request;
using MediatR;
using System.Diagnostics;

namespace Insightinator.API.Middlewares;

public class MonitoringMiddleware : IMiddleware
{
    private readonly ILogger<MonitoringMiddleware> _logger;
    private readonly IServiceProvider _serviceProvider;
    private Stopwatch _stopwatch;

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
            using var scope = _serviceProvider.CreateScope();
            var mediatr = scope.ServiceProvider.GetRequiredService<IMediator>();

            await mediatr.Send(new TotalRequestNumberRequest());
        }
    }
}
