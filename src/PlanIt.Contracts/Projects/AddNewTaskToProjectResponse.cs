namespace PlanIt.Contracts.Projects;

public record NewTaskResponse(
    Guid Id,
    string Name,
    string Description,
    DateTime CreatedDateTime,
    DateTime UpdatedDateTime
);