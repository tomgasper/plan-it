namespace PlanIt.Contracts.Tasks.Responses;

public record ProjectTaskResponse(
    Guid Id,
    string Name,
    string Description,
    string TaskOwnerId,
    IReadOnlyList<string> TaskCommentIds,
    IReadOnlyList<string> TaskWorkerIds
);