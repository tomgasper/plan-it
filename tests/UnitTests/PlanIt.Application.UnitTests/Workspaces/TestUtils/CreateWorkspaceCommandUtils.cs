using PlanIt.Application.UnitTests.TestUtils.Constants;
using PlanIt.Application.Workspaces.Commands.CreateWorkspace;

namespace PlanIt.Application.UnitTests.Workspaces.TestUtils;

public static class CreateWorkspaceCommandUtils
{
    public static CreateWorkspaceCommand CreateCommand() => (
        new CreateWorkspaceCommand(
            Constants.Workspace.Name,
            Constants.Workspace.Description
        )
    );
}