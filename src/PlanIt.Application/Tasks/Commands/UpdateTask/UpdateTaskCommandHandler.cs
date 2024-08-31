using FluentResults;
using MediatR;
using Microsoft.Extensions.Configuration.UserSecrets;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Domain.ProjectAggregate;
using PlanIt.Domain.ProjectAggregate.Entities;
using PlanIt.Domain.ProjectAggregate.ValueObjects;

namespace PlanIt.Application.Tasks.Commands.UpdateTask;

public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, Result<ProjectTask>>
{
    IProjectRepository _projectRepository;

    public UpdateTaskCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result<ProjectTask>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        // Check if the project exists
        var userId = new Guid(request.UserId);
        Project? project = await _projectRepository.GetAsync(request.ProjectId);

        if (project is null)
        {
            return Result.Fail<ProjectTask>(new NotFoundError($"The project with id: {request.ProjectId} couldn't be found."));
        }
        
        // Check if the task exists
        var taskToEdit = project.ProjectTasks.FirstOrDefault(task => task.Id.Value.ToString() == request.ProjectTaskId);
        if (taskToEdit is null)
        {
            return Result.Fail<ProjectTask>(new NotFoundError($"The task with id: {request.ProjectTaskId} couldn't be found."));
        }

        // Update the entry
        var editedTask = project.ChangeTaskNameDescription(
            ProjectTaskId.Create(new Guid(request.ProjectTaskId)),
            request.UserId,
            request.Name,
            request.Description);

        if (editedTask is null)
        {
            return Result.Fail<ProjectTask>(new InternalServerError("Couldn't update the task. Try again later."));
        }

        // Persist the entry
        await _projectRepository.UpdateAsync();

        return editedTask;
    }
}