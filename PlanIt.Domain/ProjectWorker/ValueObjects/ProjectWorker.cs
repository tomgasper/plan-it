using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using PlanIt.Domain.Models;

namespace PlanIt.Domain.ProjectWorker.ValueObjects;

public sealed class ProjectWorkerId : ValueObject
{
    public Guid Value {get;}

    private ProjectWorkerId(Guid value ) { Value = value; }

    public static ProjectWorkerId Create()
    {
        return new( Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
