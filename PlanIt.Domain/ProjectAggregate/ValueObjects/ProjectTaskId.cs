using System.Runtime.InteropServices;
using PlanIt.Domain.Models;

namespace PlanIt.Domain.ProjectAggregate.ValueObjects
{
    public sealed class ProjectTaskId : AggregateRootId<Guid>
    {
        public override Guid Value { get; protected set; }

        private ProjectTaskId(Guid value)
        {
            Value = value;
        }

        public static ProjectTaskId Create(Guid value)
        {
            return new ProjectTaskId(value);
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