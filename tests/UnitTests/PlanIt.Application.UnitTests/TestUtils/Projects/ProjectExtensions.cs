using FluentAssertions;
using PlanIt.Application.Projects.Commands.CreateProject;
using PlanIt.Domain.ProjectAggregate;
using PlanIt.Domain.ProjectAggregate.Entities;

namespace PlanIt.Application.UnitTests.TestUtils.Projects.Extensions;
public static partial class ProjectExtensions
{
    public static void ValidateCreatedFrom(this Project project, CreateProjectCommand command)
    {
        project.Name.Should().Be(command.Name);
        project.Description.Should().Be(command.Description);
        project.ProjectTasks.Should().HaveSameCount(command.ProjectTasks);
        project.ProjectTasks.Zip(command.ProjectTasks).ToList().ForEach(pair => ValidateTasks(pair.First, pair.Second));
    }

    static void ValidateTasks(ProjectTask projectTask, CreateProjectTaskCommand command)
    {
        projectTask.Id.Should().NotBeNull();
        projectTask.Name.Should().Be(command.Name);
        projectTask.Description.Should().Be(command.Description);
    }
}