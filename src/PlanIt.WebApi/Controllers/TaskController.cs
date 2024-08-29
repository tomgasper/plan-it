
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlanIt.Application.Projects.Commands.AddTaskToProject;
using PlanIt.Application.Projects.Queries;
using PlanIt.Contracts.Projects.Requests;
using PlanIt.Contracts.Projects.Responses;

namespace PlanIt.WebApi.Controllers;

[Route("api/projects/{projectId}")]
public class TaskController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public TaskController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet("tasks")]
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

    [HttpPost("addTask")]
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