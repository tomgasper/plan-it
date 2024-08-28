using Azure.Core;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanIt.Application.Projects.Commands.AddTaskToProject;
using PlanIt.Application.Projects.Commands.CreateProject;
using PlanIt.Application.Projects.Queries;
using PlanIt.Contracts.Projects;

namespace PlanIt.WebApi.Controllers;

[Route("projectOwners/{projectOwnerId}/project")]
public class ProjectController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public ProjectController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateProject(
        CreateProjectRequest request,
        string projectOwnerId
    )
    {
        var command = _mapper.Map<CreateProjectCommand>((request, projectOwnerId));

        var createProjectResult = await _mediator.Send(command);

        if (createProjectResult.IsFailed)
        {
            return Problem(createProjectResult.Errors);
        }

        return Ok(_mapper.Map<ProjectResponse>(createProjectResult.Value));
    }

    [AllowAnonymous]
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