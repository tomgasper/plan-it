using FluentResults;
using MediatR;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Domain.ProjectAggregate;

namespace PlanIt.Application.Projects.Commands.UpdateProject;

class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Result<Project>>
{
    private readonly IProjectRepository _projectRepository;

    public UpdateProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result<Project>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        // Check if the project exists
        var project = await _projectRepository.GetAsync(request.ProjectId);

        if (project is null)
        {
            return Result.Fail<Project>(new NotFoundError($"The project with id: {request.ProjectId} couldn't be found."));
        }

        var userId = new Guid(request.UserId);
        // Check if the user can access it (is the project owner or project worker)
        bool isAllowedToUpdateProject = project.ProjectOwnerId.Value == userId
            || project.ProjectTasks.Any(t => t.TaskWorkerIds.Any(wId => wId.Value == userId));

        if (!isAllowedToUpdateProject)
        {
            return Result.Fail<Project>(new ForbiddenError($"The user with id: {userId} is not allowed to edit project with id: ${request.ProjectId}"));
        }

        // Update the entry
        project.ChangeName(request.Name);
        project.ChangeDescription(request.Description);

        // Persist the entry
        await _projectRepository.UpdateAsync();

        // Return the entry
        return project;
    }
}