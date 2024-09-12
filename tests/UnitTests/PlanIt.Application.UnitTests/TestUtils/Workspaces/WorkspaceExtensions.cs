using FluentAssertions;
using PlanIt.Application.Workspaces.Commands.CreateWorkspace;
using PlanIt.Application.Workspaces.Commands.UpdateWorkspace;
using PlanIt.Domain.WorkspaceAggregate;

namespace PlanIt.Application.UnitTests.TestUtils.Workspaces;

public static partial class WorkspaceExtensions
{
    public static void ValidateCreatedFrom(this Workspace workspace, CreateWorkspaceCommand command)
    {
        workspace.Name.Should().Be(command.Name);
        workspace.Description.Should().Be(command.Description);
    }

    public static void ValidateUpdatedFrom(this Workspace workspace, UpdateWorkspaceCommand command)
    {
        workspace.Name.Should().Be(command.Name);
        workspace.Description.Should().Be(command.Description);
    }
}