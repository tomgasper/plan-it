using Mapster;
using Microsoft.Data.SqlClient;
using PlanIt.Application.Projects.Commands.AddTaskToProject;
using PlanIt.Application.Projects.Queries;
using PlanIt.Application.Services.Authentication.Common;
using PlanIt.Contracts.Authenthication;
using PlanIt.Contracts.Projects.Requests;
using PlanIt.Contracts.Projects.Responses;
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
            .Map(dest => dest.TaskCommentIds, src => src.TaskCommentIds.Select( id => id.Value).ToList())
            .Map(dest => dest.ProjectWorkerIds, src => src.TaskWorkerIds.Select( id => id.Value).ToList());

        config.NewConfig<(CreateTaskRequest Request, string ProjectId), CreateTaskCommand>()
            .Map(dest => dest.ProjectId, src => src.ProjectId)
            .Map(dest => dest, src => src.Request);
    }
}