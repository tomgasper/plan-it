using PlanIt.Application.Tasks.Commands.CreateTask;
using PlanIt.Application.UnitTests.TestUtils.Constants;

namespace PlanIt.Application.UnitTests.Projects.Commands.TestUtils;

public static class CreateTaskCommandUtils
{
    public static DateTime CreateDueDateForTask(int daysFromNow)
    {
        return DateTime.UtcNow.AddDays(daysFromNow);
    }

    public static CreateTaskCommand CreateCommand(DateTime taskDueDate, List<string>? assignedUsers = null) => (
        new CreateTaskCommand(
            Constants.User.Id,
            Constants.Project.Id,
            Constants.Task.Name,
            Constants.Task.Description,
            taskDueDate,
            assignedUsers ?? []
        )
    );
}