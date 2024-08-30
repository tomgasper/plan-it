using FluentResults;
using MediatR;
using PlanIt.Domain.ProjectAggregate.Entities;

namespace PlanIt.Application.Tasks.Commands.UpdateTask;

public record UpdateTaskCommand(
    string UserId,
    string ProjectId,
    string ProjectTaskId,
    string Name,
    string Description
    
) : IRequest<Result<ProjectTask>>;