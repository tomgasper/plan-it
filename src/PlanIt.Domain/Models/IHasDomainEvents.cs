namespace PlanIt.Domain.Models;

public interface IHasDomainEvents
{
    public IReadOnlyList<IDomainEvent> DomainEvents {get; }

    public void ClearDomainEvents();
}