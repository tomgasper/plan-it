using MediatR;
using PlanIt.Domain.ProjectAggregate.Entities;

public record GetTasksCommand(
    string userId,
    string ProjectId
) : IRequest<IReadOnlyList<ProjectTask>>;