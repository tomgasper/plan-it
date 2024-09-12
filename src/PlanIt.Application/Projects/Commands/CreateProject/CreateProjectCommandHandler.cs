using FluentResults;
using MediatR;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Domain.Project.ValueObjects;
using PlanIt.Domain.ProjectAggregate;
using PlanIt.Domain.ProjectAggregate.Entities;
using PlanIt.Domain.ProjectAggregate.ValueObjects;
using PlanIt.Domain.UserAggregate.ValueObjects;
using PlanIt.Domain.WorkspaceAggregate.ValueObjects;

namespace PlanIt.Application.Projects.Commands.CreateProject;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Result<Project>>
{
    public readonly IProjectRepository _projectRepository;
    public readonly IUserContext _userContext;

    public CreateProjectCommandHandler(IProjectRepository projectRepository, IUserContext userContext)
    {
        _projectRepository = projectRepository;
        _userContext = userContext;
    }

    public async Task<Result<Project>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var loggedInUserId = _userContext.TryGetUserId();

        var projectOwnerId = ProjectOwnerId.FromString(loggedInUserId);
        var taskOwnerId = TaskOwnerId.FromString(loggedInUserId);

        // Create Project
        var project = Project.Create(
            name: request.Name,
            description: request.Description,
            workspaceId: WorkspaceId.FromString(request.WorkspaceId),
            projectOwnerId: projectOwnerId,
            projectTasks: request.ProjectTasks.ConvertAll(projectTask => ProjectTask.Create(
                taskOwnerId: taskOwnerId,
                name: projectTask.Name,
                description: projectTask.Description
            ))
        );

        // Persist Project
        await _projectRepository.AddAsync(project);

        return project;
    }
}