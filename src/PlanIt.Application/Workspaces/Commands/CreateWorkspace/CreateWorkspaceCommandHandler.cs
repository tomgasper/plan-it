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

    public CreateWorkspaceCommandHandler(IWorkspaceRepository workspaceRepository, IUserContext userContext)
    {
        _workspaceRepository = workspaceRepository;
        _userContext = userContext;
    }

    public async Task<Result<Workspace>> Handle(CreateWorkspaceCommand command, CancellationToken cancellationToken)
    {
        var userId = _userContext.TryGetUserId();

        var workspace = Workspace.Create(command.Name, command.Description, WorkspaceOwnerId.Create(new Guid(userId)));

        await _workspaceRepository.AddAsync(workspace);

        return Result.Ok(workspace);
    }
}
