using Microsoft.EntityFrameworkCore;
using PlanIt.Domain.ProjectAggregate;

namespace PlanIt.Infrastructure.Persistence;

public class PlanItDbContext : DbContext
{
    public PlanItDbContext(DbContextOptions<PlanItDbContext> options) : base(options)
    {
    }

    public DbSet<Project> Projects {get; set;}
}