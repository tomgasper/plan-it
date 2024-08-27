using FluentResults;
using MediatR;
using PlanIt.Domain.ProjectAggregate;

namespace PlanIt.Application.Projects.CreateProject.Commands;

public record CreateProjectCommand(
    string ProjectOwnerId,
    string Name,
    string Description,
    List<ProjectTaskCommand> ProjectTasks
) : IRequest<Result<Project>>;
public record ProjectTaskCommand
(
    string Name,
    string Description
);