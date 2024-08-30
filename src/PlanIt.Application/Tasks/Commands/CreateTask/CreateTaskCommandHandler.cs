using FluentResults;
using MediatR;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Domain.ProjectAggregate.Entities;

namespace PlanIt.Application.Tasks.Commands.CreateTask;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Result<ProjectTask>>
{
    IProjectRepository _projectRepository;

    public CreateTaskCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result<ProjectTask>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        // Make sure the Project exists
        var project = await _projectRepository.GetAsync(request.ProjectId);

        if (project is null)
        {
            return Result.Fail<ProjectTask>(new NotFoundError($"The project with provided Id: {request.ProjectId} doesn't exist."));
        }

        // Create task and add to the Project
        var createdTask = project.AddNewTask(request.Name, request.Description);

        // Persist Project
        await _projectRepository.UpdateAsync();

        return Result.Ok(createdTask);
    }
}