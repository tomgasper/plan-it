using Mapster;
using Microsoft.Data.SqlClient;
using PlanIt.Application.Projects.Queries;
using PlanIt.Application.Services.Authentication.Common;
using PlanIt.Application.Tasks.Commands.CreateTask;
using PlanIt.Contracts.Authenthication;
using PlanIt.Contracts.Projects.Responses;
using PlanIt.Contracts.Tasks.Requests;
using PlanIt.Contracts.Tasks.Responses;
using PlanIt.Domain.ProjectAggregate;

using ProjectTask = PlanIt.Domain.ProjectAggregate.Entities.ProjectTask;

namespace PlanIt.WebApi.Common.Mapping;

public class ProjectMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
        .Map(dest => dest.Token, src => src.Token)
        .Map(dest => dest, src => src.User);

        config.NewConfig<Project, ProjectResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.ProjectOwnerId, src => src.ProjectOwnerId.Value);

        config.NewConfig<ProjectTask, ProjectTaskResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.TaskOwnerId, src => src.TaskOwnerId.Value )
            .Map(dest => dest.TaskComments, src => src.TaskComments.Select( tc => new TaskCommentResponse(tc.Name, tc.Description)))
            .Map(dest => dest.TaskWorkerIds, src => src.TaskWorkerIds.Select( id => id.Value).ToList());

        config.NewConfig<(CreateTaskRequest Request, string ProjectId), CreateTaskCommand>()
            .Map(dest => dest.ProjectId, src => src.ProjectId)
            .Map(dest => dest, src => src.Request);
    }
}