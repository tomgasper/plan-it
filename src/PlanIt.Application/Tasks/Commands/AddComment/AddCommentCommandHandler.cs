using FluentResults;
using MediatR;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Domain.ProjectAggregate.Entities;
using PlanIt.Domain.ProjectAggregate.ValueObjects;

namespace PlanIt.Application.Tasks.Commands.AddComment;

public sealed class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, Result<TaskComment>>
{
    private readonly IProjectRepository _projectRepository;

    public AddCommentCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result<TaskComment>> Handle(AddCommentCommand request, CancellationToken cancellationToken)
    {
        // Add comment
        var projectId = ProjectId.Create(new Guid(request.ProjectId));
        var taskId = ProjectTaskId.Create(new Guid(request.TaskId));

        var project = await _projectRepository.GetAsync(projectId);

        if (project is null)
        {
            return Result.Fail<TaskComment>(new NotFoundError($"Could not find project with id:{projectId.Value}"));
        }

        var taskComment = TaskComment.Create(
            projectTaskId:taskId,
            name: request.Name,
            description: request.Description);

        project.AddCommentToTask(taskComment);

        await _projectRepository.UpdateAsync();

        return taskComment;
    }
}