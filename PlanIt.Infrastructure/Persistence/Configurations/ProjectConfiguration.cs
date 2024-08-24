using System.ComponentModel.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlanIt.Domain.Project.ValueObjects;
using PlanIt.Domain.ProjectAggregate;
using PlanIt.Domain.ProjectAggregate.ValueObjects;

namespace PlanIt.Infrastructure.Persistence.Configurations;

public class ProjectConfigurations : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
       ConfigureProjectsTable(builder);
       ConfigureProjectTasksTable(builder);
    }

    private void ConfigureProjectTasksTable(EntityTypeBuilder<Project> builder)
    {
        builder.OwnsMany(p => p.ProjectTasks, pb => {
            pb.ToTable("ProjectTasks");
            pb.WithOwner().HasForeignKey("ProjectId");

            // Create primary key that will be a combination of ProjectTaskId and ProjectId
            // Combination is need so that the entities are specified uniquely in the DB
            // For example if we had some ProjectTaskId with Id = 1 and two Projects
            // that both had a ProjectTask inside with Id = 1 and we deleted it
            // in one of the Projects then the ProjectTask is gone in another project as well
            pb.HasKey("Id", "ProjectId");

            pb.Property(s => s.Id)
                .HasColumnName("ProjectTaskId")
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => ProjectTaskId.Create(value)
                );

            pb.Property(s => s.Name)
                .HasMaxLength(100);

            pb.Property(s => s.Description)
                .HasMaxLength(200);

        });

        builder.Metadata.FindNavigation(nameof(Project.ProjectTasks))!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureProjectsTable(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Projects");

        // Aggregate Id is always unique
        builder.HasKey(m => m.Id);
        builder.Property( m => m.Id )
            .ValueGeneratedNever() // The object will always have id so not needed
            .HasConversion( 
                id => id.Value, // In conversion
                value => ProjectId.Create(value)); // Out conversion
        
        builder.Property(m => m.Name)
            .HasMaxLength(100);

        builder.Property(m => m.Description)
            .HasMaxLength(200);
        
        builder.Property(m => m.ProjectOwnerId)
            .HasConversion(
                id => id.Value,
                value => ProjectOwnerId.Create(value)
            );
    }
}