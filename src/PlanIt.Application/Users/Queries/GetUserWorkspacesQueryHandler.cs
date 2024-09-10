using FluentResults;
using MediatR;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Domain.UserAggregate.ValueObjects;
using PlanIt.Domain.WorkspaceAggregate;
using PlanIt.Domain.WorkspaceAggregate.ValueObjects;

namespace PlanIt.Application.Users.Queries;
public class GetUserWorkspacesQueryHandler : IRequestHandler<GetUserWorkspacesQuery, Result<List<Workspace>>>
{
    private readonly IWorkspaceRepository _workspaceRepository;
    private readonly IUserRepository _userRepository;

    public GetUserWorkspacesQueryHandler(IWorkspaceRepository workspaceRepository, IUserRepository userRepository)
    {
        _workspaceRepository = workspaceRepository;
        _userRepository = userRepository;
    }

    public async Task<Result<List<Workspace>>> Handle(GetUserWorkspacesQuery query, CancellationToken cancellationToken)
    {
        var userId = WorkspaceOwnerId.FromString(query.UserId);

        // Get workspaces
        var workspaces = await _workspaceRepository.GetUserWorkspacesAsync(userId);

        if (workspaces is null)
        {
            return Result.Fail<List<Workspace>>(new NotFoundError($"Couldn't find workspaces for User with id: ${userId.Value}"));
        }

        // Return it
        return workspaces;
    }
}