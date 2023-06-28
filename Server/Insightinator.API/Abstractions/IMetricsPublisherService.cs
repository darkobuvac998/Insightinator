using Insightinator.API.Metrics;
using Insightinator.API.Metrics.Http.Response;

namespace Insightinator.API.Abstractions;

public interface IMetricsPublisherService
{
    Task<MetricsGroup> PublishErrorMetrics();
    Task<MetricsGroup> PublishHttpRequestMetrics();
    Task<IList<MetricsGroup>> PublishMetricGroups();
    Task<MetricsGroup> PublishHttpResponseMetrics();
}
