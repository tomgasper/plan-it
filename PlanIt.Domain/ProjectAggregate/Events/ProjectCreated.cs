using PlanIt.Domain.Models;

namespace PlanIt.Domain.ProjectAggregate.Events;

public record ProjectCreated(Project Project) : IDomainEvent;