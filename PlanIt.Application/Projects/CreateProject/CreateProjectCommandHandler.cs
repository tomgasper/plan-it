using FluentResults;
using MediatR;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Domain.Project.ValueObjects;
using PlanIt.Domain.ProjectAggregate;
using PlanIt.Domain.ProjectAggregate.Entities;

namespace PlanIt.Application.Projects.CreateProject;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Result<Project>>
{
    public readonly IProjectRepository _projectRepository;

    public CreateProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result<Project>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        // Create Project
        var project = Project.Create(
            name: request.Name,
            description: request.Description,
            projectOwnerId: ProjectOwnerId.Create(request.ProjectOwnerId),
            projectTasks: request.ProjectTasks.ConvertAll( projectTask => ProjectTask.Create(
                name: projectTask.Name,
                description: projectTask.Description
            ))
        );

        // Persist Project
        _projectRepository.Add(project);
        
        return project;
    }
}