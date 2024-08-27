namespace PlanIt.Domain.Models;

public abstract class AggregateRoot<TId, TIdType> : Entity<TId> where TId : AggregateRootId<TIdType>
{
    public new TId Id { get; protected set; }

    protected AggregateRoot(TId id) { Id = id; }

    #pragma warning disable CS8618
    protected AggregateRoot()
    {

    }
    #pragma warning restore CS8618
}