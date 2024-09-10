using FluentResults;
using MediatR;
using PlanIt.Domain.ProjectAggregate;

namespace PlanIt.Application.Projects.Commands.CreateProject;

public record CreateProjectCommand(
    string WorkspaceId,
    string Name,
    string Description,
    List<CreateProjectTaskCommand> ProjectTasks
) : IRequest<Result<Project>>;
public record CreateProjectTaskCommand
(
    string Name,
    string Description
);