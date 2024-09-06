
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlanIt.Application.Projects.Queries;
using PlanIt.Application.Tasks.Commands.CreateTask;
using PlanIt.Contracts.Tasks.Requests;
using PlanIt.WebApi.Common.Mapping;
using FluentResults;
using PlanIt.Domain.ProjectAggregate.Entities;
using PlanIt.Application.Tasks.Commands.DeleteTask;

namespace PlanIt.WebApi.Controllers;

[Route("api/projects/{projectId}/tasks")]
public class TaskController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public TaskController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProjectTask(
        CreateTaskRequest request,
        string projectId
        )
    {
        var userId = GetUserId();
        CreateTaskCommand command = request.MapToCommand(userId, projectId);

        var createdProjectTaskResult = await _mediator.Send(command);

        if (createdProjectTaskResult.IsFailed)
        {
            return Problem(createdProjectTaskResult.Errors);
        }

        return Ok(createdProjectTaskResult.Value.MapToResponse());
    }

    [HttpGet]
    public async Task<IActionResult> GetProjectTasks(
        string projectId
    )
    {
        var command = new ProjectTasksQuery(projectId);

        Result<IReadOnlyList<ProjectTask>> projectTasksResult = await _mediator.Send(command);

        if (projectTasksResult.IsFailed)
        {
            return Problem(projectTasksResult.Errors);
        }

        return Ok(projectTasksResult.Value.MapToResponse());
    }

    [HttpPut("{taskId}")]
    public async Task<IActionResult> UpdateProjectTask(
        [FromBody] UpdateTaskRequest request,
        string projectId,
        string taskId
    )
    {
        var userId = GetUserId();
        var command = request.MapToCommand(userId, projectId, taskId);

        Result<ProjectTask> projectTasksResult = await _mediator.Send(command);

        if (projectTasksResult.IsFailed)
        {
            return Problem(projectTasksResult.Errors);
        }

        return Ok(projectTasksResult.Value.MapToResponse());
    }

    [HttpDelete("{taskId}")]
    public async Task<IActionResult> DeleteProjectTask(
        string projectId,
        string taskId
    )
    {
        var command = new DeleteTaskCommand(projectId, taskId);

        Result deleteTaskResult = await _mediator.Send(command);

        if (deleteTaskResult.IsFailed)
        {
            return Problem(deleteTaskResult.Errors);
        }

        return Ok();
    }

    [HttpPost]
    [Route("{taskId}/comments")]
    public async Task<IActionResult> AddCommentToTask([FromBody] AddCommentRequest addCommentRequest, string projectId, string taskId)
    {
        var addCommentCommand = addCommentRequest.MapToCommand(projectId, taskId);

        var addCommentResult = await _mediator.Send(addCommentCommand);

        if (addCommentResult.IsFailed){
            return Problem(addCommentResult.Errors);
        }

        return Ok(addCommentResult.Value);
    }
}