using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanIt.Application.Workspaces.Commands.AssignProjectToWorkspace;
using PlanIt.Application.Workspaces.Commands.CreateWorkspace;
using PlanIt.Domain.WorkspaceAggregate;

namespace PlanIt.WebApi.Controllers;

[Authorize]
[Route("/api/workspaces")]
public class WorkspaceController : ApiController
{
    private readonly ISender _mediator;

    public WorkspaceController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateWorkspace([FromBody] CreateWorkspaceRequest createWorkspaceRequest)
    {
        CreateWorkspaceCommand command = createWorkspaceRequest.ToCommand();

        Result<Workspace> createWorkspaceResult = await _mediator.Send(command);

        if (createWorkspaceResult.IsFailed)
        {
            return Problem(createWorkspaceResult.Errors);
        }

        return Ok(createWorkspaceResult.Value.ToResponse());
    }

    [HttpPost]
    [Route("{workspaceId}/assignProject")]
    public async Task<IActionResult> AssignProjectToWorkspace(
        [FromBody] AssignProjectToWorkspaceRequest assignProjectToWorkspaceRequest,
        string workspaceId
        )
    {
        AssignProjectToWorkspaceCommand command = assignProjectToWorkspaceRequest.ToCommand(workspaceId);

        Result<Workspace> assignProjectResult = await _mediator.Send(command);

        if (assignProjectResult.IsFailed)
        {
            return Problem(assignProjectResult.Errors);
        }

        return Ok(assignProjectResult.Value.ToResponse());
    }
}