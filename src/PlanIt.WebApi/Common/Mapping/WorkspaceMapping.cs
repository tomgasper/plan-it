using PlanIt.Application.Workspaces.Commands.AssignProjectToWorkspace;
using PlanIt.Application.Workspaces.Commands.CreateWorkspace;
using PlanIt.Contracts.Workspace.Responses;
using PlanIt.Domain.WorkspaceAggregate;

public static class WorkspaceMapping
{
    public static CreateWorkspaceCommand ToCommand(this CreateWorkspaceRequest request) => (
        new CreateWorkspaceCommand(
            Name: request.Name,
            Description: request.Description
        )
    );

    public static WorkspaceResponse ToResponse(this Workspace workspace) => (
        new WorkspaceResponse(
            Name: workspace.Name,
            Description: workspace.Description
        )
    );

    public static AssignProjectToWorkspaceCommand ToCommand(this AssignProjectToWorkspaceRequest request, string workspaceId) => (
        new AssignProjectToWorkspaceCommand(
            ProjectId: request.ProjectId,
            WorkspaceId: workspaceId
        )
    );
}