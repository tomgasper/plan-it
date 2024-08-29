using FluentResults;
using MediatR;
using PlanIt.Domain.ProjectAggregate.Entities;

namespace PlanIt.Application.Projects.Queries;

public record ProjectTasksQuery(
    string ProjectId
) : IRequest<Result<IReadOnlyList<ProjectTask>>>;