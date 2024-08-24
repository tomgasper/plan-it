using PlanIt.Domain.Models;

namespace PlanIt.Domain.ProjectAggregate.ValueObjects
{
    public sealed class ProjectTaskId : ValueObject
    {
        public Guid Value { get; }

        private ProjectTaskId(Guid value)
        {
            Value = value;
        }

        public static ProjectTaskId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}