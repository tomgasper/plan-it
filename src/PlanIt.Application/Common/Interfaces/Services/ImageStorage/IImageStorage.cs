using FluentResults;
using Microsoft.AspNetCore.Http;

namespace PlanIt.Application.Common.Interfaces.Services.ImageStorage;

public interface IImageStorage
{
    public Task<Result<PhotoUploadResult>> AddPhoto(IFormFile file);
    public Task<Result<string>> DeletePhoto(string publicId);
}