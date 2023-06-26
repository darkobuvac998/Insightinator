﻿using Insightinator.API.Abstractions;
using Insightinator.API.Hubs;
using Insightinator.API.Options;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Insightinator.API.Services.Background;

public class PubliserBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public PubliserBackgroundService(IServiceProvider serviceProvider) =>
        _serviceProvider = serviceProvider;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();

            var metricsPublisher =
                scope.ServiceProvider.GetRequiredService<IMetricsPublisherService>();

            var hubContext = scope.ServiceProvider.GetRequiredService<
                IHubContext<MetricsHub, IMetricsHubClient>
            >();

            var publishingOptions = scope.ServiceProvider.GetRequiredService<
                IOptions<PublishingOptions>
            >();

            await Task.Delay(publishingOptions.Value.PublishTime, stoppingToken);
            var metrics = await metricsPublisher.PublishMetrics();

            await hubContext.Clients.All.ReceiveMetrics(JsonConvert.SerializeObject(metrics));
            scope!.Dispose();
        }
    }
}