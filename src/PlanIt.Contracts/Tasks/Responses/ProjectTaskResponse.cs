using System.Reflection.Metadata.Ecma335;

namespace PlanIt.Contracts.Tasks.Responses;

public record ProjectTaskResponse(
    Guid Id,
    string Name,
    string Description,
    string TaskOwnerId,
    DateTime DueDate,
    IReadOnlyList<TaskCommentResponse> TaskComments,
    IReadOnlyList<string> TaskWorkerIds
);

public record TaskCommentResponse
(
    string Name,
    string Description
);