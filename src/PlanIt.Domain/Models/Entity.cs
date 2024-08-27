namespace PlanIt.Domain.Models;

public abstract class Entity<TId> : IEquatable<Entity<TId>>, IHasDomainEvents
 where TId : notnull
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    public TId Id {get; protected set;}

    protected Entity(TId id)
    {
        Id = id;
    }

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    #pragma warning disable CS8618
    protected Entity()
    {

    }
    #pragma warning restore CS8618

    public override bool Equals(object? obj)
    {
        /*
        return obj is Entity<TId> entity && Id.Equals(entity.Id);
        */

        if (obj is not Entity<TId> entity)
        return false;
    
        if (ReferenceEquals(this, entity))
            return true;

        if (Id is null || entity.Id is null)
            return false;

        return Id.Equals(entity.Id);
    }

    public bool Equals(Entity<TId>? other)
    {
        return Equals((object?)other);
    }

    public static bool operator ==(Entity<TId> left, Entity<TId> right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Entity<TId> left, Entity<TId> right)
    {
        return !Equals(left, right);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}