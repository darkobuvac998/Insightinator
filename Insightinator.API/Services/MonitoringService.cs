using Insightinator.API.Abstractions;
using Insightinator.API.Handlers.Http.Request;
using MediatR;

namespace Insightinator.API.Services;

public class MonitoringService : IMonitoringService
{
    private readonly ISender _sender;

    public MonitoringService(ISender sender) => (_sender) = (sender);

    public async Task ComputeAvgRequestProcessingTime(double time)
    {
        await _sender.Send(new AvgRequestProcessingTimeRequest(time));
    }

    public async Task ComputeRequestsPerMinute(double upTime)
    {
        await _sender.Send(new RequestsPerMinuteRequest(upTime));
    }

    public async Task ComputeTotalRequestNumber()
    {
        await _sender.Send(new TotalRequestNumberRequest());
    }
}
