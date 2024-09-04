using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlanIt.Domain.UserAggregate;
using PlanIt.Infrastructure.Authentication;

namespace PlanIt.Infrastructure.Persistence.Configurations;

public class IdentityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        ConfigureAspNetUsersTable(builder);
    }

    private void ConfigureAspNetUsersTable(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("AspNetUsers");
    }
}