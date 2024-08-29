namespace PlanIt.Application.UnitTests.TestUtils.Constants;

public static partial class Constants
{
    public static class Project{
        public const string Id = "00000000-0000-0000-0000-000000000000";
        public const string Name = "Project Name";
        public const string Description = "Project Description";
        public const string ProjectTaskName = "Task Name";
        public const string ProjectTaskDescription = "Task Description";

        public static string ProjectTaskNameFromIndex(int index) => $"{ProjectTaskName} {index}";
        public static string ProjectTaskDescriptionFromIndex(int index) => $"{ProjectTaskDescription} {index}";
    }
}