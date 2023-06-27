using System.ComponentModel.DataAnnotations;

namespace Insightinator.API.Options;

public class PublishingOptions
{
    [Required]
    public int PublishTime { get; set; }
}
