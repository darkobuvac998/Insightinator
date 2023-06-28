using AutoMapper;
using Insightinator.API.Metrics.Http.Response;

namespace Insightinator.API;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ResponseCodeDistributionMetric, ResponseCodeDistributionMetricDto>()
            .ForMember(x => x.Value, m => m.MapFrom(s => s.ToList()));
    }
}
