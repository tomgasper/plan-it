using FluentResults;
using MediatR;

namespace PlanIt.Application.Projects.Commands.DeleteProject;

public record DeleteProjectCommand(
    string ProjectId,
    string UserId
) : IRequest<Result>;