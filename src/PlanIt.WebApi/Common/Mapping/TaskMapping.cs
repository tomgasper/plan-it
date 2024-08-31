using PlanIt.Application.Tasks.Commands.CreateTask;
using PlanIt.Application.Tasks.Commands.UpdateTask;
using PlanIt.Contracts.Tasks.Requests;
using PlanIt.Contracts.Tasks.Responses;
using PlanIt.Domain.ProjectAggregate.Entities;

namespace PlanIt.WebApi.Common.Mapping;

public static class TaskMapping
{
    public static CreateTaskCommand MapToCommand(this CreateTaskRequest request, string userId, string projectId) => (
        new CreateTaskCommand(
            UserId: userId,
            ProjectId: projectId,
            Name: request.Name,
            Description: request.Description)
    );

    public static UpdateTaskCommand MapToCommand(this UpdateTaskRequest request, string userId, string projectId, string taskId) => (
        new UpdateTaskCommand(
            UserId: userId,
            ProjectId: projectId,
            ProjectTaskId: taskId,
            Name: request.Name,
            Description: request.Description
        )
    );
    public static ProjectTaskResponse MapToResponse(this ProjectTask projectTask) => (
        new ProjectTaskResponse(
            Id: projectTask.Id.Value,
            Name: projectTask.Name,
            Description: projectTask.Description,
            TaskOwnerId: projectTask.TaskOwnerId.MapToResponse(),
            TaskCommentIds: IdMapping.MapIdsToStrings(projectTask.TaskCommentIds),
            TaskWorkerIds: IdMapping.MapIdsToStrings(projectTask.TaskWorkerIds)
        )
    );

    public static List<ProjectTaskResponse> MapToResponse(this IReadOnlyList<ProjectTask> projectTasks) => (
        projectTasks.Select( task => task.MapToResponse()).ToList()
    );
}