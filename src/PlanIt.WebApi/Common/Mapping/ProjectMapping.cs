using Microsoft.AspNetCore.Authorization.Infrastructure;
using PlanIt.Application.Projects.Commands.CreateProject;
using PlanIt.Contracts.Projects.Requests;
using PlanIt.Contracts.Projects.Responses;
using PlanIt.Contracts.Tasks.Responses;
using PlanIt.Domain.ProjectAggregate;

namespace PlanIt.WebApi.Common.Mapping;

public static class ProjectMapping
{
    public static ProjectResponse MapToResponse(this Project project) => (
        new ProjectResponse(
            Id: project.Id.Value,
            Name: project.Name,
            Description: project.Description,
            ProjectTasks: project.ProjectTasks.Select( task => new ProjectTaskResponse(
                Id: task.Id.Value,
                Name: task.Name,
                Description: task.Description,
                TaskOwnerId: task.TaskOwnerId.Value.ToString(),
                TaskComments: task.TaskComments.Select( tc => new TaskCommentResponse(tc.Name, tc.Description)).ToList(), // Temporary
                TaskWorkerIds: new List<string>() // Temporary
            )).ToList(),
            ProjectOwnerId: project.ProjectOwnerId.Value.ToString(),
            CreatedDateTime: project.CreatedDateTime,
            UpdatedDateTime: project.UpdatedDateTime
        )
    );

    public static CreateProjectCommand MapToCommand(this CreateProjectRequest request) =>
    (
        new CreateProjectCommand(
            WorkspaceId: request.WorkspaceId,
            Name: request.Name,
            Description: request.Description,
            ProjectTasks: request.ProjectTasks.Select( pt => new CreateProjectTaskCommand(
                Name: pt.Name,
                Description: pt.Description,
                DueDate: pt.DueDate,
                AssignedUsers: pt.AssignedUsers
            )).ToList()
        )
    );

    public static UpdateProjectCommand MapToCommand(this UpdateProjectRequest request, string projectId, string userId) => 
        new UpdateProjectCommand(
            ProjectId: projectId,
            UserId: userId,
            Name: request.Name,
            Description: request.Description
        );
}