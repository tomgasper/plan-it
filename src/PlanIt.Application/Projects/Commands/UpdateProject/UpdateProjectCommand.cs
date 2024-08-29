using FluentResults;
using MediatR;
using PlanIt.Domain.ProjectAggregate;

public record UpdateProjectCommand (
    string ProjectId,
    string UserId,
    string Name,
    string Description
) : IRequest<Result<Project>>;