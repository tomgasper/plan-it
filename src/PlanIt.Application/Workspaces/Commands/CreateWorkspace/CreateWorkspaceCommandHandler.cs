using FluentResults;
using MediatR;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Domain.WorkspaceAggregate;
using PlanIt.Domain.WorkspaceAggregate.ValueObjects;

namespace PlanIt.Application.Workspaces.Commands.CreateWorkspace;

public class CreateWorkspaceCommandHandler : IRequestHandler<CreateWorkspaceCommand, Result<Workspace>>
{
    private readonly IWorkspaceRepository _workspaceRepository;
    private readonly IUserContext _userContext;

    public CreateWorkspaceCommandHandler(IUserContext userContext, IWorkspaceRepository workspaceRepository)
    {
        _workspaceRepository = workspaceRepository;
        _userContext = userContext;
    }

    public async Task<Result<Workspace>> Handle(CreateWorkspaceCommand command, CancellationToken cancellationToken)
    {
        var userId = _userContext.TryGetUserId();

        var workspace = Workspace.Create(
            name: command.Name,
            description: command.Description,
            workspaceOwnerId: WorkspaceOwnerId.Create(new Guid(userId)));

        await _workspaceRepository.AddAsync(workspace);

        return Result.Ok(workspace);
    }
}
