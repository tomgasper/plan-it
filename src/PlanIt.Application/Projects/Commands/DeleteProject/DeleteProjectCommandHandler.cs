using FluentResults;
using MediatR;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Domain.ProjectAggregate.ValueObjects;

namespace PlanIt.Application.Projects.Commands.DeleteProject;

public record DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, Result>
{
    IProjectRepository _projectRepository;

    public DeleteProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var projectId = ProjectId.Create(new Guid(request.ProjectId));
        
        // Check if the project exists
        var project = await _projectRepository.GetAsync(projectId);

        if (project is null)
        {
            return Result.Fail(new NotFoundError($"The project with id: {projectId.Value} couldn't be found."));
        }

        var userId = new Guid(request.UserId);

        // Check if the user is allowed to delete the project
        bool isAllowedToDeleteProject = project.ProjectOwnerId.Value == userId;

        if (!isAllowedToDeleteProject)
        {
            return Result.Fail(new ForbiddenError($"The user with id: {userId} is not allowed to delete project with id: ${request.ProjectId}"));
        }

        // Delete from persistence
        await _projectRepository.DeleteAsync(project);

        return Result.Ok();
    }
}