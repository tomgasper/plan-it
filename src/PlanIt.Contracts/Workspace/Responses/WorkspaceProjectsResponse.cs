using PlanIt.Contracts.Projects.Responses;

namespace PlanIt.Contracts.Workspace.Responses;

public record WorkspaceProjectsResponse
(
    string WorkspaceId,
    List<ProjectResponse> Projects
);