using PlanIt.Application.Projects.Commands.AddTaskToProject;
using PlanIt.Application.UnitTests.TestUtils.Constants;

namespace PlanIt.Application.UnitTests.Projects.Commands.TestUtils;

public static class CreateTaskCommandUtils
{
    public static CreateTaskCommand CreateCommand() => (
        new CreateTaskCommand(
            Constants.Project.Id,
            Constants.Task.Name,
            Constants.Task.Description
        )
    );
}