using PlanIt.Domain.Models;

namespace PlanIt.Domain.Project.ValueObjects
{
    public sealed class ProjectOwnerId : ValueObject
    {
        public Guid Value { get; }

        private ProjectOwnerId(Guid value)
        {
            Value = value;
        }

        public static ProjectOwnerId CreateUnique()
        {
            return new ProjectOwnerId(Guid.NewGuid());
        }

        public static ProjectOwnerId Create(Guid projectOwnerId)
        {
            return new ProjectOwnerId(projectOwnerId);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}