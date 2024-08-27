namespace PlanIt.Application.UnitTests.TestUtils.Constants;

public static partial class Constants
{
    public static class Project{
        public const string Name = "Project Name";
        public const string Description = "Description";
        public const string ProjectTaskName = "Task Name";
        public const string ProjectTaskDescription = "Task Description";

        public static string ProjectTaskNameFromIndex(int index) => $"{ProjectTaskName} {index}";
        public static string ProjectTaskDescriptionFromIndex(int index) => $"{ProjectTaskDescription} {index}";
    }
}