using FluentResults;
using MediatR;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Domain.ProjectAggregate.Entities;
using PlanIt.Domain.ProjectAggregate.ValueObjects;

namespace PlanIt.Application.Projects.Queries;

public class ProjectTasksQueryHandler : IRequestHandler<ProjectTasksQuery, Result<IReadOnlyList<ProjectTask>>>
{
    IProjectRepository _projectRepository;

    public ProjectTasksQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result<IReadOnlyList<ProjectTask>>> Handle(ProjectTasksQuery request, CancellationToken cancellationToken)
    {
        var projectId = ProjectId.Create(new Guid(request.ProjectId));
        
        // Retrieve project
        var project = await _projectRepository.GetAsync(projectId);

        if (project is null)
        {
            return Result.Fail<IReadOnlyList<ProjectTask>>(new NotFoundError($"No project found with provided Project Id: {projectId}"));
        }

        // Get tasks from project
        var projectTasks = project.ProjectTasks;

        return Result.Ok(projectTasks);
    }
}