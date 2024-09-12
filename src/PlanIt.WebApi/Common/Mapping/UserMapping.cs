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
}