using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlanIt.Application.Projects.Commands.CreateProject;
using PlanIt.Application.Projects.Commands.DeleteProject;
using PlanIt.Application.Projects.Queries.GetProject;
using PlanIt.Contracts.Projects.Requests;
using PlanIt.Contracts.Projects.Responses;
using PlanIt.WebApi.Common.Mapping;

using PlanIt.Application.Projects.Queries.GetProjectWithDetails;

namespace PlanIt.WebApi.Controllers;

[Route("api/projects")]
public class ProjectController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public ProjectController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody]CreateProjectRequest createProjectRequest)
    {
        CreateProjectCommand command = createProjectRequest.MapToCommand();

        var createProjectResult = await _mediator.Send(command);

        if (createProjectResult.IsFailed)
        {
            return Problem(createProjectResult.Errors);
        }

        return Ok(createProjectResult.Value.MapToResponse());
    }

    [HttpGet("{projectId}")]
    public async Task<IActionResult> GetProject(string projectId)
    {
        GetProjectQuery query = new (projectId);

        var getProjectResult = await _mediator.Send(query);

        if (getProjectResult.IsFailed)
        {
            return Problem(getProjectResult.Errors);
        }

        return Ok(getProjectResult.Value.MapToResponse());
    }

    [HttpGet("{projectId}/details")]
    public async Task<IActionResult> GetProjectDetails(string projectId)
    {
        GetProjectWitDetailsQuery query = new (projectId);

        var getProjectResult = await _mediator.Send(query);

        if (getProjectResult.IsFailed)
        {
            return Problem(getProjectResult.Errors);
        }

        return Ok(getProjectResult.Value);
    }

    [HttpPut("{projectId}")]
    public async Task<IActionResult> UpdateProject([FromBody]UpdateProjectRequest request, string projectId)
    {
        var userId = GetUserId();
        UpdateProjectCommand command = request.MapToCommand(projectId, userId);

        var updateProjectResult = await _mediator.Send(command);

        if (updateProjectResult.IsFailed)
        {
            return Problem(updateProjectResult.Errors);
        }

        return Ok(updateProjectResult.Value.MapToResponse());
    }

    [HttpDelete("{projectId}")]
    public async Task<IActionResult> DeleteProject(string projectId)
    {
        var userId = GetUserId();
        DeleteProjectCommand command = new (projectId, userId);

        var deleteProjectResult = await _mediator.Send(command);

        if (deleteProjectResult.IsFailed)
        {
            return Problem(deleteProjectResult.Errors);
        }

        return Ok();
    }
}