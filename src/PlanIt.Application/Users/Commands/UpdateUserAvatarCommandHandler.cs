using FluentResults;
using MediatR;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Application.Common.Interfaces.Services.ImageStorage;
using PlanIt.Domain.UserAggregate;
using PlanIt.Domain.UserAggregate.ValueObjects;

namespace PlanIt.Application.Users.Commands;

public class UpdateUserAvatarCommandHandler : IRequestHandler<UpdateUserAvatarCommand, Result<User>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserContext _userContext;
    private readonly IImageStorage _imageStorage;

    public UpdateUserAvatarCommandHandler(IUserRepository userRepository, IUserContext userContext, IImageStorage imageStorage)
    {
        _userRepository = userRepository;
        _userContext = userContext;
        _imageStorage = imageStorage;
    }

    public async Task<Result<User>> Handle(UpdateUserAvatarCommand command, CancellationToken cancellationToken)
    {
        var loggedInUserId = UserId.FromString(_userContext.TryGetUserId());
        var userIdFromRequest = UserId.FromString(command.UserId);

        // Check if user is allowed to change the avatar
        if (loggedInUserId != userIdFromRequest)
        {
            Result.Fail<User>(new ForbiddenError("You don't have permission to change this user's avatar."));
        }

        // Make sure the user exists
        User? user = await _userRepository.GetAsync(userIdFromRequest);
        if (user is null)
        {
            return Result.Fail<User>(new NotFoundError($"Couldn't find any user with id: {userIdFromRequest.Value}"));
        }

        // Save the picture to the storage
        Result<PhotoUploadResult> imageUrl = await _imageStorage.AddPhoto(command.Avatar);

        if (imageUrl.IsFailed)
        {
            return Result.Fail<User>(imageUrl.Errors);
        }

        user.ChangeAvatar(imageUrl.Value.Url);

        // Persist the change
        await _userRepository.SaveChangesAsync();

        return user;
    }
}