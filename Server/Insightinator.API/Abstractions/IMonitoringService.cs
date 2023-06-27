namespace Insightinator.API.Abstractions;

public interface IMonitoringService
{
    Task ComputeTotalRequestNumber();
    Task ComputeAvgRequestProcessingTime(double time);
    Task ComputeRequestsPerMinute(double upTime);
    Task ComputeResponseCodeDistribution(string httpCode);
    Task ComputeErrroRate(double upTime);
    Task ComputeErrorTypes(object error);
}
