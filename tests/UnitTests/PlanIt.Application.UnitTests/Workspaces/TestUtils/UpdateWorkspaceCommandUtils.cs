using PlanIt.Application.UnitTests.TestUtils.Constants;
using PlanIt.Application.Workspaces.Commands.UpdateWorkspace;
using PlanIt.Domain.ProjectAggregate.ValueObjects;
using PlanIt.Application.UnitTests.TestUtils.Common;
using PlanIt.Domain.WorkspaceAggregate;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using PlanIt.Domain.WorkspaceAggregate.ValueObjects;

namespace PlanIt.Application.UnitTests.Workspaces.TestUtils;

public static class UpdateWorkspaceCommandUtils
{
    public static Workspace GetMockedWorkspace() => (
        Workspace.Create(
            Constants.Workspace.Name,
            Constants.Workspace.Description,
            WorkspaceOwnerId.FromString(Constants.User.Id)
        )
    );

    public static UpdateWorkspaceCommand CreateCommand() => (
        new UpdateWorkspaceCommand(
            Constants.Workspace.Id,
            Constants.Workspace.Name,
            Constants.Workspace.Description
        )
    );

    public static List<ProjectId> CreateProjectId( int projectIdsCount = 1) => (
        Enumerable.Range(0, projectIdsCount)
        .Select( index => ProjectId.Create(new Guid(ValueGeneratorsUtils.GenerateSequentialGuid(index)))).ToList()
    );
}