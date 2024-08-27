using MediatR;
using PlanIt.Domain.ProjectAggregate.Events;

namespace PlanIt.Application.Projects.CreateProject.Events;

public class ProjectCreatedEventHandler : INotificationHandler<ProjectCreated>
{
    public Task Handle(ProjectCreated notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}