using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlanIt.Domain.UserAggregate;
using PlanIt.Domain.UserAggregate.ValueObjects;
using PlanIt.Infrastructure.Authentication;

namespace PlanIt.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ConfigureUsersTable(builder);
    }

    private void ConfigureUsersTable(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        /*
        builder.HasKey( x => x.Id);

        builder
            .HasOne( x => x.Id )
            .WithOne()
            .HasForeignKey<ApplicationUser>(e => e.Id);
        */

        builder.HasKey( u => u.Id );

        // builder.Property( u => u.IdentityId ).IsRequired();

        builder.HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey( u => u.Id );

        builder.Property( p => p.FirstName ).HasColumnName("FirstName");
        builder.Property( p => p.LastName ).HasColumnName("LastName");
    }
}