namespace PlanIt.Application.Common.Interfaces.Services.ImageStorage;

public record PhotoUploadResult(
    string PublicId,
    string Url
);