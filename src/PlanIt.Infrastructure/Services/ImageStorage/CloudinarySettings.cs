using Microsoft.Net.Http.Headers;

namespace PlanIt.Infrastructure.Services.ImageStorage;

public class CloudinarySettings
{
    public const string SectionName = "CloudinarySettings";
    public string CloudName { get; set; } = null!;
    public string ApiKey { get; set; } = null!;
    public string ApiSecret { get; set; } = null!;
}