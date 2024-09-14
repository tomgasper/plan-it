using PlanIt.Application.Users.Commands.UpdateUser;
using PlanIt.Application.Users.Commands.UpdateUserAvatarCommand;
using PlanIt.Application.Users.Queries.GetAllUsers;
using PlanIt.Contracts.Users.Requests;
using PlanIt.Contracts.Users.Responses;
using PlanIt.Contracts.Workspace.Responses;
using PlanIt.Domain.UserAggregate;
using PlanIt.Domain.WorkspaceAggregate;

namespace PlanIt.WebApi.Common.Mapping;

public static class UserMapping
{
    public static List<WorkspaceResponse> MapToResponse(this List<Workspace> workspaces)
    {
        return workspaces.Select(w => w.MapToResponse()).ToList();
    }

    public static UserResponse MapToResponse(this User user) =>
    (
        new UserResponse(
            Id: user.Id.ToString(),
            FirstName: user.FirstName,
            LastName: user.LastName,
            AvatarUrl: user.AvatarUrl
        )
    );

    public static UpdateUserAvatarCommand MapToCommand(this UpdateUserAvatarRequest request, string userId) => (
        new UpdateUserAvatarCommand(
            UserId: userId,
            Avatar: request.Avatar
        )
    );

    public static UpdateUserCommand MapToCommand(this UpdateUserRequest request, string userId) => (
        new UpdateUserCommand(
            UserId: userId,
            FirstName: request.FirstName,
            LastName: request.LastName,
            Email: request.Email,
            OldPassword: request.OldPassword,
            NewPassword: request.NewPassword
        )
    );

    public static List<UserResponse> MapToResponse(this List<User> users)
    {
        return users.ConvertAll( user => user.MapToResponse());
    }
}