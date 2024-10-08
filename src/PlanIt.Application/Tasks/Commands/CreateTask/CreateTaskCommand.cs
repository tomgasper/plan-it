using FluentResults;
using MediatR;
using PlanIt.Domain.ProjectAggregate.Entities;

namespace PlanIt.Application.Tasks.Commands.CreateTask;

public record CreateTaskCommand(
    string UserId,
    string ProjectId,
    string Name,
    string Description,
    DateTime DueDate,
    List<string> AssignedUsers
) : IRequest<Result<ProjectTask>>;