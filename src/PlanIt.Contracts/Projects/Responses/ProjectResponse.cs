using PlanIt.Contracts.Tasks.Responses;

namespace PlanIt.Contracts.Projects.Responses;

public record ProjectResponse(
    Guid Id,
    string Name,
    string Description,
    List<ProjectTaskResponse> ProjectTasks,
    string ProjectOwnerId,
    DateTime CreatedDateTime,
    DateTime UpdatedDateTime
);