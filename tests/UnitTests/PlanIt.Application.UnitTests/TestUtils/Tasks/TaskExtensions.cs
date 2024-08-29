using FluentAssertions;
using PlanIt.Application.Projects.Commands.AddTaskToProject;
using PlanIt.Domain.ProjectAggregate.Entities;

namespace PlanIt.Application.UnitTests.TestUtils.Tasks;

public static partial class TaskExtensions
{
    public static void ValidteCreatedFrom(this ProjectTask projectTask, CreateTaskCommand command)
    {
        projectTask.Name.Should().Be(command.Name);
        projectTask.Description.Should().Be(command.Description);
    }
}