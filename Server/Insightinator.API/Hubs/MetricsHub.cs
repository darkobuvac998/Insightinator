using Insightinator.API.Abstractions;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace Insightinator.API.Hubs;

public sealed class MetricsHub : Hub<IMetricsHubClient>
{
    private readonly IMetricsPublisherService _metricsPublisherService;
    private readonly IStoreService _storeService;

    public MetricsHub(IMetricsPublisherService metricsPublisherService, IStoreService storeService)
    {
        _metricsPublisherService = metricsPublisherService;
        _storeService = storeService;
    }

    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
        var key = $"{Constants.HubConnections}-Metrics";

        var connections = await _storeService.GetAsync<IList<string>>(key);

        if (connections is not null && connections.Any())
        {
            connections.Add(Context.ConnectionId);
        }
        else
        {
            connections = new List<string> { Context.ConnectionId };
        }

        await _storeService.SaveAsync(key, connections);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
        var key = $"{Constants.HubConnections}-Metrics";

        var connections = await _storeService.GetAsync<IList<string>>(key);

        if (connections is not null && connections.Any())
        {
            connections.Remove(Context.ConnectionId);
        }

        await _storeService.SaveAsync(key, connections);
    }

    public async Task PublishMetrics()
    {
        var metrics = await _metricsPublisherService.PublishMetrics();
        await Clients.All.ReceiveMetrics(JsonConvert.SerializeObject(metrics));
    }
}
