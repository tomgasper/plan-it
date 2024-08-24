namespace PlanIt.Contracts.Projects;

public record ProjectResponse(
    Guid Id,
    string Name,
    string Description,
    List<ProjectTaskResponse> ProjectTasks,
    string ProjectOwnerId,
    DateTime CreatedDateTime,
    DateTime UpdatedDateTime
);

public record ProjectTaskResponse(
    Guid Id,
    string Name,
    string Description,
    string TaskOwnerId,
    List<string> TaskCommentIds,
    List<string> ProjectWorkerIds
);