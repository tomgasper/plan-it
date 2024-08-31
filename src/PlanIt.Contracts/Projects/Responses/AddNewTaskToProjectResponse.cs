namespace PlanIt.Contracts.Projects.Responses;

public record NewTaskResponse(
    Guid Id,
    string Name,
    string Description,
    DateTime CreatedDateTime,
    DateTime UpdatedDateTime
);