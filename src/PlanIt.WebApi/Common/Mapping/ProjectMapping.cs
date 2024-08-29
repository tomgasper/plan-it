using PlanIt.Contracts.Projects;
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
                TaskCommentIds: new List<string>(), // Temporary
                ProjectWorkerIds: new List<string>() // Temporary
            )).ToList(),
            ProjectOwnerId: project.ProjectOwnerId.Value.ToString(),
            CreatedDateTime: project.CreatedDateTime,
            UpdatedDateTime: project.UpdatedDateTime
        )
    );
}