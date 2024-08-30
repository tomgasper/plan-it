using PlanIt.Application.Tasks.Commands.CreateTask;
using PlanIt.Application.UnitTests.TestUtils.Constants;

namespace PlanIt.Application.UnitTests.Projects.Commands.TestUtils;

public static class CreateTaskCommandUtils
{
    public static CreateTaskCommand CreateCommand() => (
        new CreateTaskCommand(
            Constants.User.Id,
            Constants.Project.Id,
            Constants.Task.Name,
            Constants.Task.Description
        )
    );
}