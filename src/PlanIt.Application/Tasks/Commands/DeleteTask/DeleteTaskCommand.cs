using FluentResults;
using MediatR;

namespace PlanIt.Application.Tasks.Commands.DeleteTask;

public record DeleteTaskCommand (
    string ProjectId,
    string TaskId
) : IRequest<Result>;