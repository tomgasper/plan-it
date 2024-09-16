using PlanIt.Domain.Models;

namespace PlanIt.Domain.ProjectAggregate.ValueObjects
{
    public sealed class ProjectId : AggregateRootId<Guid>
    {
        public override Guid Value { get; protected set;}

        private ProjectId(Guid value)
        {
            Value = value;
        }

        public static ProjectId Create(Guid value)
        {
            return new ProjectId(value);
        }

        public static ProjectId CreateUnique()
        {
            return new ProjectId(Guid.NewGuid());
        }

        public static ProjectId FromString(string id)
        {
            return new ProjectId(new Guid(id));
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}