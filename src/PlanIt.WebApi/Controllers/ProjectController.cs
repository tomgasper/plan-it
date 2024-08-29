using System.Security.Claims;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlanIt.Application.Projects.Commands.AddTaskToProject;
using PlanIt.Application.Projects.Commands.CreateProject;
using PlanIt.Application.Projects.Queries;
using PlanIt.Application.Projects.Queries.GetProject;
using PlanIt.Contracts.Projects;
using PlanIt.WebApi.Common.Mapping;

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
    public async Task<IActionResult> CreateProject(
        CreateProjectRequest request
    )
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var command = _mapper.Map<CreateProjectCommand>((request, userId));

        var createProjectResult = await _mediator.Send(command);

        if (createProjectResult.IsFailed)
        {
            return Problem(createProjectResult.Errors);
        }

        return Ok(_mapper.Map<ProjectResponse>(createProjectResult.Value));
    }

    [HttpGet("{projectId}")]
    public async Task<IActionResult> GetProject(string projectId)
    {
        var query = new GetProjectQuery(projectId);

        var getProjectResult = await _mediator.Send(query);

        if (getProjectResult.IsFailed)
        {
            return Problem(getProjectResult.Errors);
        }

        return Ok(getProjectResult.Value.MapToResponse());
    }

    public async Task<IActionResult> UpdateProject(string projectId)
    {
        var command = new UpdateProjectQuery(projectId);

        var updateProjectResult = await _mediator.Send(command);

        if (updateProjectResulot.IsFailed)
        {
            return Problem(updateProjectResult.Errors);
        }

        return Ok(updateProjectResult.MapToResponse());
    }

    [HttpGet("{projectId}/tasks")]
    public async Task<IActionResult> GetProjectTasks(
        string projectId
    )
    {
        var command = new ProjectTasksQuery(projectId);

        var projectTasksResult = await _mediator.Send(command);

        if (projectTasksResult.IsFailed)
        {
            return Problem(projectTasksResult.Errors);
        }

        return Ok(_mapper.Map<List<ProjectTaskResponse>>(projectTasksResult.Value));
    }

    [HttpPost("{projectId}/addTask")]
    public async Task<IActionResult> CreateProjectTask(
        CreateTaskRequest request,
        string projectId
        )
    {
        var command = _mapper.Map<CreateTaskCommand>((request, projectId));

        var createdProjectTaskResult = await _mediator.Send(command);

        if (createdProjectTaskResult.IsFailed)
        {
            return Problem(createdProjectTaskResult.Errors);
        }

        return Ok(_mapper.Map<ProjectTaskResponse>(createdProjectTaskResult.Value));
    }
}