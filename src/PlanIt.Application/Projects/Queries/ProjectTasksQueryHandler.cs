using FluentResults;
using MediatR;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Domain.ProjectAggregate.Entities;

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
        // Retrieve project
        var project = await _projectRepository.GetAsync(request.ProjectId);

        if (project is null)
        {
            return Result.Fail<IReadOnlyList<ProjectTask>>(new NotFoundError($"No project found with provided Project Id: {request.ProjectId}"));
        }

        // Get tasks from project
        var projectTasks = project.ProjectTasks;

        return Result.Ok(projectTasks);
    }
}