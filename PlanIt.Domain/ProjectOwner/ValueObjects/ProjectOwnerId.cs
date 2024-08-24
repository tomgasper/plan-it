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
            return new(Guid.NewGuid());
        }

        public static ProjectOwnerId Create(string projectOwnerId)
        {
            return new(Guid.Parse(projectOwnerId));
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}