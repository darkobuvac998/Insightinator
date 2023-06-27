using Insightinator.API.Options;
using Microsoft.Extensions.Options;

namespace Insightinator.API.OptionsSetup;

public class PublishingOptionsSetup : IConfigureOptions<PublishingOptions>
{
    private static readonly string SectionName = "PublishingOptions";
    private readonly IConfiguration _configuration;

    public PublishingOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(PublishingOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}
