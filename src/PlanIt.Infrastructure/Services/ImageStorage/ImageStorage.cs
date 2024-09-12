using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PlanIt.Application.Common.Interfaces.Services.ImageStorage;

namespace PlanIt.Infrastructure.Services.ImageStorage;

public class ImageStorage : IImageStorage
{
    private readonly Cloudinary _cloudinary;

    public ImageStorage(IOptions<CloudinarySettings> configuration, Cloudinary cloudinary)
    {
        var account = new Account(
                configuration.Value.CloudName,
                configuration.Value.ApiKey,
                configuration.Value.ApiSecret
            );

        _cloudinary = cloudinary;
    }

    public async Task<Result<PhotoUploadResult>> AddPhoto(IFormFile photo)
    {
        if (photo.Length > 0)
        {
            await using var stream = photo.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                    File = new FileDescription(photo.FileName, stream),
                    Transformation = new Transformation().Height(450).Width(450).Crop("fill")
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
            {
                return Result.Fail<PhotoUploadResult>(
                    new ImageStorageError("Couldn't upload the image to the storage provider. Please try again later."));
            }

            return new PhotoUploadResult
            (
                PublicId: uploadResult.PublicId,
                Url: uploadResult.SecureUrl.ToString()
            );
        }

        return Result.Fail<PhotoUploadResult>(new ImageStorageError("Provided incorrect file for photo upload."));
    }

    public async Task<Result<string>> DeletePhoto(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);
        var result = await _cloudinary.DestroyAsync(deleteParams);
        return result.Result == "ok" ? result.Result : Result.Fail<string>(new ImageStorageError("Couldn't delete the image from the storage provide. Please try again later."));
    }
}