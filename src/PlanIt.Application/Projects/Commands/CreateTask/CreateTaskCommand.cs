using FluentResults;
using MediatR;
using PlanIt.Domain.ProjectAggregate.Entities;

namespace PlanIt.Application.Projects.Commands.AddTaskToProject;

public record CreateTaskCommand(
    string ProjectId,
    string Name,
    string Description
) : IRequest<Result<ProjectTask>>;