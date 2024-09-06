using PlanIt.Application.Workspaces.Commands.AssignProjectToWorkspace;
using PlanIt.Application.Workspaces.Commands.CreateWorkspace;
using PlanIt.Application.Workspaces.Commands.UpdateWorkspace;
using PlanIt.Contracts.Workspace.Requests;
using PlanIt.Contracts.Workspace.Responses;
using PlanIt.Domain.WorkspaceAggregate;

public static class WorkspaceMapping
{
    public static CreateWorkspaceCommand MapToCommand(this CreateWorkspaceRequest request) => (
        new CreateWorkspaceCommand(
            Name: request.Name,
            Description: request.Description
        )
    );

    public static UpdateWorkspaceCommand MapToCommand(this UpdateWorkspaceRequest request, string projectId) => (
        new UpdateWorkspaceCommand(
            WorkspaceId: projectId,
            Name: request.Name,
            Description: request.Description
        )
    );

    public static AssignProjectToWorkspaceCommand MapToCommand(this AssignProjectToWorkspaceRequest request, string workspaceId) => (
        new AssignProjectToWorkspaceCommand(
            ProjectId: request.ProjectId,
            WorkspaceId: workspaceId
        )
    );

    public static WorkspaceResponse MapToResponse(this Workspace workspace) => (
        new WorkspaceResponse(
            Name: workspace.Name,
            Description: workspace.Description
        )
    );
}