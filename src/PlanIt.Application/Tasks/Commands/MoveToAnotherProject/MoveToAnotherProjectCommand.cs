using FluentResults;
using MediatR;
using PlanIt.Domain.ProjectAggregate;

namespace PlanIt.Application.Tasks.Commands.MoveToAnotherProject
{
    public record MoveToAnotherProjectCommand(
        string ProjectId,
        string TaskId,
        string AnotherProjectId
    ) : IRequest<Result<Project>>;
}