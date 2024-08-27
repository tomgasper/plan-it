using Microsoft.EntityFrameworkCore;
using PlanIt.Domain.Models;
using PlanIt.Domain.ProjectAggregate;
using PlanIt.Infrastructure.Persistence.Interceptors;

namespace PlanIt.Infrastructure.Persistence;

public class PlanItDbContext : DbContext
{
    private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;
    public PlanItDbContext(DbContextOptions<PlanItDbContext> options, PublishDomainEventsInterceptor publishDomainEventsInterceptor) : base(options)
    {
        _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
    }

    public DbSet<Project> Projects {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Ignore<List<IDomainEvent>>()
            .ApplyConfigurationsFromAssembly(typeof(PlanItDbContext).Assembly);
 
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // So we can handle domain events
        optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);
        base.OnConfiguring(optionsBuilder);
    }
}