using FluentResults;
using MediatR;
using PlanIt.Domain.WorkspaceAggregate;

namespace PlanIt.Application.Users.Queries;

public record GetUserWorkspacesQuery
(
    string UserId
) : IRequest<Result<List<Workspace>>>;