namespace Insightinator.API.Abstractions;

public interface IMetricsPublisherService
{
    Task<IList<object>> PublishMetrics();
}
