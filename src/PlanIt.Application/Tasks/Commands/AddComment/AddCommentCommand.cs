using FluentResults;
using MediatR;
using PlanIt.Domain.ProjectAggregate.Entities;

namespace PlanIt.Application.Tasks.Commands.AddComment;

public record AddCommentCommand(
    string ProjectId,
    string TaskId,
    string Name,
    string Description
) : IRequest<Result<TaskComment>>;