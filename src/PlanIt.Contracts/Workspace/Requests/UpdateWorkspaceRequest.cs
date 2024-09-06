namespace PlanIt.Contracts.Workspace.Requests;

public record UpdateWorkspaceRequest(
    string Name,
    string Description
);