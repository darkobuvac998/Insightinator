namespace Insightinator.API.Hubs;

public interface IMetricsHubClient
{
    Task ReceiveMetrics(string metrics);
}
