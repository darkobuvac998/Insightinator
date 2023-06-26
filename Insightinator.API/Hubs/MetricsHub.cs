using Insightinator.API.Abstractions;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace Insightinator.API.Hubs;

public sealed class MetricsHub : Hub<IMetricsHubClient>
{
    private readonly IMetricsPublisherService _metricsPublisherService;

    public MetricsHub(IMetricsPublisherService metricsPublisherService)
    {
        _metricsPublisherService = metricsPublisherService;
    }

    public async Task PublishMetrics()
    {
        var metrics = await _metricsPublisherService.PublishMetrics();
        await Clients.All.ReceiveMetrics(JsonConvert.SerializeObject(metrics));
    }
}
