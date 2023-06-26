namespace Insightinator.API.Abstractions;

public interface IMonitoringService
{
    Task ComputeTotalRequestNumber();
    Task ComputeAvgRequestProcessingTime(double time);
}
