namespace PlanIt.Application.Projects.Queries.GetProjectWithDetails.Dto;

// Using specific DTO for faster queries

public record DetailedProjectResponse(
    Guid Id,
    string Name,
    string Description,
    List<DetailedProjectTaskResponse> ProjectTasks,
    string ProjectOwnerId,
    DateTime CreatedDateTime,
    DateTime UpdatedDateTime
);

public record DetailedProjectTaskResponse(
    Guid Id,
    string Name,
    string Description,
    string TaskOwnerId,
    DateTime DueDate,
    IReadOnlyList<DetailedTaskCommentResponse> TaskComments,
    List<TaskWorkerResponse> TaskWorkers
);

public record DetailedTaskCommentResponse
(
    string Name,
    string Description
);

public record TaskWorkerResponse(
    string Id,
    string FirstName,
    string LastName,
    string AvatarUrl
);