using NSubstitute.Routing.Handlers;
using PlanIt.Application.Projects.Commands.CreateProject;
using PlanIt.Application.UnitTests.TestUtils.Constants;

namespace PlanIt.Application.UnitTests.Projects.Commands.TestUtils;

public static class CreateProjectCommandUtils
{
    public static CreateProjectCommand CreateCommand(
        List<CreateProjectTaskCommand>? tasks = null
    ) => (
          new CreateProjectCommand(
            Constants.ProjectOwner.Id.Value.ToString()!,
            Constants.Project.Name,
            Constants.Project.Description,
            tasks ?? CreateTasksCommand()
            ));
    
    public static List<CreateProjectTaskCommand> CreateTasksCommand(
        int tasksCount = 1,
        List<DateTime>? DueDates = null
    )
        {
            return Enumerable.Range(0, tasksCount)
                .Select( index => new CreateProjectTaskCommand(
                    Constants.Project.ProjectTaskNameFromIndex(index),
                    Constants.Project.ProjectTaskDescriptionFromIndex(index),
                    ( DueDates is not null && DueDates.Count == tasksCount) ? DueDates[index] : DateTime.UtcNow.AddDays(7 + index)
                )).ToList();
        }
}