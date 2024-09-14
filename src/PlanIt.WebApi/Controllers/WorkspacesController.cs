using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanIt.Application.Workspaces.Commands.AssignProjectToWorkspace;
using PlanIt.Application.Workspaces.Queries.GetWorkspace;
using PlanIt.Application.Workspaces.Commands.CreateWorkspace;
using PlanIt.Application.Workspaces.Commands.DeleteWorkspace;
using PlanIt.Application.Workspaces.Commands.UpdateWorkspace;
using PlanIt.Contracts.Workspace.Requests;
using PlanIt.Domain.WorkspaceAggregate;
using PlanIt.Contracts.Projects.Responses;
using PlanIt.Application.Workspaces.Queries.GetWorkspaceProjects;
using PlanIt.Contracts.Workspace.Responses;
using PlanIt.Domain.ProjectAggregate;
using PlanIt.Application.Projects.Queries.GetProjectWithDetails.Dto;

namespace PlanIt.WebApi.Controllers;


[Route("/api/workspaces")]
public class WorkspaceController : ApiController
{
    private readonly ISender _mediator;

    public WorkspaceController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("{workspaceId}")]
    public async Task<IActionResult> GetWorkspace(string workspaceId)
    {
        GetWorkspaceQuery query = new(workspaceId);

        Result<Workspace> workspaceQueryResult = await _mediator.Send(query);

        if (workspaceQueryResult.IsFailed)
        {
            return Problem(workspaceQueryResult.Errors);
        }

        return Ok(workspaceQueryResult.Value.MapToResponse());
    }

    
    [HttpGet]
    [Route("{workspaceId}/projects")]
    public async Task<IActionResult> GetWorkspaceProjects(string workspaceId)
    {
        GetWorkspaceProjectsQuery query = new(workspaceId);

        Result<List<DetailedProjectResponse>> workspaceProjectsQueryResult = await _mediator.Send(query);

        if (workspaceProjectsQueryResult.IsFailed)
        {
            return Problem(workspaceProjectsQueryResult.Errors);
        }

        return Ok(workspaceProjectsQueryResult.Value.MapToResponse(workspaceId));
    }

    [HttpPost]
    public async Task<IActionResult> CreateWorkspace([FromBody] CreateWorkspaceRequest createWorkspaceRequest)
    {
        CreateWorkspaceCommand command = createWorkspaceRequest.MapToCommand();

        Result<Workspace> createWorkspaceResult = await _mediator.Send(command);

        if (createWorkspaceResult.IsFailed)
        {
            return Problem(createWorkspaceResult.Errors);
        }

        return Ok(createWorkspaceResult.Value.MapToResponse());
    }

    [HttpPut]
    [Route("{workspaceId}")]
    public async Task<IActionResult> UpdateWorkspace([FromBody] UpdateWorkspaceRequest updateWorkspaceRequest, string workspaceId)
    {
        UpdateWorkspaceCommand command = updateWorkspaceRequest.MapToCommand(workspaceId);

        Result<Workspace> updateWorkspaceResult = await _mediator.Send(command);

        if (updateWorkspaceResult.IsFailed)
        {
            return Problem(updateWorkspaceResult.Errors);
        }

        return Ok(updateWorkspaceResult.Value.MapToResponse());
    }

    [HttpDelete]
    [Route("{workspaceId}")]
    public async Task<IActionResult> DeleteWorkspace( string workspaceId)
    {
        DeleteWorkspaceCommand command = new(workspaceId);

        Result<Workspace> deleteWorkspaceResult = await _mediator.Send(command);

        if (deleteWorkspaceResult.IsFailed)
        {
            return Problem(deleteWorkspaceResult.Errors);
        }

        return Ok();
    }

    [HttpPost]
    [Route("{workspaceId}/assignProject")]
    public async Task<IActionResult> AssignProjectToWorkspace(
        [FromBody] AssignProjectToWorkspaceRequest assignProjectToWorkspaceRequest,
        string workspaceId
        )
    {
        AssignProjectToWorkspaceCommand command = assignProjectToWorkspaceRequest.MapToCommand(workspaceId);

        Result<Workspace> assignProjectResult = await _mediator.Send(command);

        if (assignProjectResult.IsFailed)
        {
            return Problem(assignProjectResult.Errors);
        }

        return Ok(assignProjectResult.Value.MapToResponse());
    }
}