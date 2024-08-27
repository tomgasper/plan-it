using PlanIt.Domain.ProjectAggregate.ValueObjects;

namespace PlanIt.Application.UnitTests.TestUtils.Constants;

public static partial class Constants
{
    public static class ProjectOwner{
        public static readonly TaskOwnerId Id = TaskOwnerId.Create(new Guid("94cacc8a-a08e-4b79-a728-58dd2884364a"));
    }
}