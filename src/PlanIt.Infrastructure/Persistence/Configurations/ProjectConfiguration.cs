

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlanIt.Domain.Project.ValueObjects;
using PlanIt.Domain.ProjectAggregate;
using PlanIt.Domain.ProjectAggregate.Entities;
using PlanIt.Domain.ProjectAggregate.ValueObjects;
using PlanIt.Domain.TaskComment.ValueObjects;
using PlanIt.Domain.WorkspaceAggregate;
using PlanIt.Domain.WorkspaceAggregate.ValueObjects;

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
        builder.OwnsMany(ptb => ptb.ProjectTasks, pt => {
            pt.ToTable("ProjectTasks");
            pt.WithOwner().HasForeignKey("ProjectId");

            // Create primary key that will be a combination of ProjectTaskId and ProjectId
            // Combination is need so that the entities are specified uniquely in the DB
            // For example if we had some ProjectTaskId with Id = 1 and two Projects
            // that both had a ProjectTask inside with Id = 1 and we deleted it
            // in one of the Projects then the ProjectTask is gone in another project as well
            pt.HasKey("Id", "ProjectId");

            pt.Property(s => s.Id)
                .HasColumnName("ProjectTaskId")
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => ProjectTaskId.Create(value)
                );

            pt.Property(s => s.TaskOwnerId)
                .HasConversion(
                    id => id.Value,
                    value => TaskOwnerId.Create(value)
                );

            pt.Property(s => s.Name)
                .HasMaxLength(100);

            pt.Property(s => s.Description)
                .HasMaxLength(200);

            // TaskCommentIds Table
            pt.OwnsMany( tcb => tcb.TaskComments, tc => {
                tc.ToTable("TaskComment");
                
                tc.WithOwner().HasForeignKey("ProjectTaskId", "ProjectId");

                tc.Property( t => t.Id)
                    .HasConversion(
                        id => id.Value,
                        value => TaskCommentId.Create(value)
                    );

                tc.Property( t => t.Id )
                    .HasColumnName("TaskCommentId")
                    .ValueGeneratedNever();

            });

            // TaskOwnerIds Tabel
            pt.OwnsMany( twb => twb.TaskWorkerIds, tw => {
                tw.ToTable("TaskWorker");
                
                tw.WithOwner().HasForeignKey("ProjectTaskId", "ProjectId");
                
                tw.Property<int>("Id").ValueGeneratedOnAdd();
                tw.HasKey("Id");

                tw.Property( t => t.Value )
                    .HasColumnName("TaskWorkerId")
                    .ValueGeneratedNever();

            });

            pt.Navigation( p => p.TaskComments).Metadata.SetField("_taskComments");
            pt.Navigation( p => p.TaskComments).UsePropertyAccessMode(PropertyAccessMode.Field);

            pt.Navigation( p => p.TaskWorkerIds).Metadata.SetField("_taskWorkerIds");
            pt.Navigation( p => p.TaskWorkerIds).UsePropertyAccessMode(PropertyAccessMode.Field);
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

        builder.Property( m => m.WorkspaceId)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => WorkspaceId.Create(value)
            );

        builder.HasOne<Workspace>()
                .WithMany()
                .HasForeignKey( p => p.WorkspaceId );
        
        builder.Property(m => m.Name)
            .HasMaxLength(100);

        builder.Property(m => m.Description)
            .HasMaxLength(200);
        
        builder.Property(m => m.ProjectOwnerId)
            .HasConversion(
                id => id.Value,
                value => ProjectOwnerId.Create(value)
            );

        builder.Metadata.FindNavigation(nameof(Project.ProjectTasks))!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}