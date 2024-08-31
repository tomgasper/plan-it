namespace PlanIt.Domain.Models;

public class IdValueObject : ValueObject
{
    public Guid Value { get; }

    protected IdValueObject(Guid value)
    {
        Value = value;
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}