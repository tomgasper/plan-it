using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlanIt.Domain.WorkspaceAggregate;
using PlanIt.Domain.WorkspaceAggregate.ValueObjects;

namespace PlanIt.Infrastructure.Persistence.Configurations;
public class WorkspaceConfiguration : IEntityTypeConfiguration<Workspace>
{
    public void Configure(EntityTypeBuilder<Workspace> builder)
    {
        ConfigureWorkspacesTable(builder);
        ConfigureProjectsTable(builder);
        ConfigureUsersTable(builder);
    }

    private void ConfigureWorkspacesTable(EntityTypeBuilder<Workspace> builder)
    {
        builder.ToTable("Workspaces");

        builder.HasKey(w => w.Id);
        builder.Property(w => w.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => WorkspaceId.Create(value)
            );

        builder.Property(w => w.Name)
            .HasMaxLength(100);

        builder.Property(w => w.Description)
            .HasMaxLength(200);

        builder.Property(w => w.WorkspaceOwnerId)
            .HasConversion(
                id => id.Value,
                value => WorkspaceOwnerId.Create(value)
            );

        builder.Metadata.FindNavigation(nameof(Workspace.ProjectIds))!.SetPropertyAccessMode(PropertyAccessMode.Field);
        builder.Metadata.FindNavigation(nameof(Workspace.ProjectIds))!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureProjectsTable(EntityTypeBuilder<Workspace> builder)
    {
        builder.OwnsMany(w => w.ProjectIds, pId =>
        {
            pId.ToTable("WorkspaceProject");

            pId.WithOwner().HasForeignKey("WorkspaceId");

            pId.Property<int>("Id").ValueGeneratedOnAdd();
            pId.HasKey("Id");

            pId.Property(pId => pId.Value)
                .HasColumnName("ProjectId")
                .ValueGeneratedNever();
        });

        builder.Navigation(p => p.ProjectIds).Metadata.SetField("_projectIds");
        builder.Navigation(p => p.ProjectIds).UsePropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureUsersTable(EntityTypeBuilder<Workspace> builder)
    {
        builder.OwnsMany(w => w.UserIds, uId =>
        {
            uId.ToTable("WorkspaceUser");

            uId.WithOwner().HasForeignKey("WorkspaceId");

            uId.Property<int>("Id").ValueGeneratedOnAdd();
            uId.HasKey("Id");

            uId.Property(pId => pId.Value)
                .HasColumnName("UserId")
                .ValueGeneratedNever();
        });

        builder.Navigation(p => p.UserIds).Metadata.SetField("_userIds");
        builder.Navigation(p => p.UserIds).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}