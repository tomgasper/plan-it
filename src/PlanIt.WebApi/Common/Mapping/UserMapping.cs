using PlanIt.Contracts.Workspace.Responses;
using PlanIt.Domain.WorkspaceAggregate;

namespace PlanIt.WebApi.Common.Mapping;

public static class UserMapping
{
    public static List<WorkspaceResponse> MapToResponse(this List<Workspace> workspaces)
    {
        return workspaces.Select(w => w.MapToResponse()).ToList();
    }
}