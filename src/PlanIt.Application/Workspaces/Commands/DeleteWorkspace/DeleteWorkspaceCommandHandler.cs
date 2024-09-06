using FluentResults;
using MediatR;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Domain.UserAggregate.ValueObjects;
using PlanIt.Domain.WorkspaceAggregate.ValueObjects;

namespace PlanIt.Application.Workspaces.Commands.DeleteWorkspace;

public class DeleteWorkspaceCommandHandler : IRequestHandler<DeleteWorkspaceCommand, Result>
{
    private readonly IUserContext _userContext;
    private readonly IWorkspaceRepository _workspaceRepository;

    public DeleteWorkspaceCommandHandler(IUserContext userContext, IWorkspaceRepository workspaceRepository)
    {
        _userContext = userContext;
        _workspaceRepository = workspaceRepository;
    }

    public async Task<Result> Handle(DeleteWorkspaceCommand request, CancellationToken cancellationToken)
    {
        var userId = UserId.FromString(_userContext.TryGetUserId());
        var workspaceId = WorkspaceId.FromString(request.WorkspaceId);

        var workspace = await _workspaceRepository.GetAsync(workspaceId);
        if (workspace is null)
        {
            return Result.Fail(new NotFoundError($"Couldn't find the workspace with id: {workspaceId.Value}"));
        }

        if (workspace.WorkspaceOwnerId.Value != userId.Value)
        {
            return Result.Fail(new ForbiddenError($"You don't have permission to modify this workspace."));
        }

        await _workspaceRepository.DeleteAsync(workspace);

        return Result.Ok();
    }
}