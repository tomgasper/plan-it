using FluentResults;
using MediatR;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Application.Users.Commands.UpdateUser;
using PlanIt.Domain.UserAggregate;
using PlanIt.Domain.UserAggregate.ValueObjects;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<User>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserContext _userContext;
    public UpdateUserCommandHandler(IUserRepository userRepository, IUserContext userContext)
    {
        _userRepository = userRepository;
        _userContext = userContext;
    }
    public async Task<Result<User>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
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

        // Password and email are stored in the "base" Identity user that the Domain User has a reference to.
        // If the user wants to change those fields we must do it via Identity User and not Domain User.
        if (command.Email is not null || command.NewPassword is not null )
        {
            var result = await _userRepository.UpdateIdentityUserAsync(
                    userId: userIdFromRequest.Value,
                    email: command.Email,
                    oldPassword: command.OldPassword,
                    newPassword: command.NewPassword);

            if (result.IsFailed)
            {
                return Result.Fail(result.Errors);
            }
        }

        // Here we are back working on the Domain Entity
        var newFirstName = command.FirstName ?? user.FirstName;
        var newLastName = command.LastName ?? user.LastName;

        user.ChangeName(newFirstName, newLastName);

        // Persist the change
        await _userRepository.SaveChangesAsync();

        return user;
    }
}