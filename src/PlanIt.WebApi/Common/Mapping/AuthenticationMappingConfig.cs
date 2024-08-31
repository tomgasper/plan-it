using Mapster;
using PlanIt.Application.Projects.Commands.CreateProject;
using PlanIt.Contracts.Projects.Requests;

namespace PlanIt.WebApi.Common.Mapping;

public class AuthenthicationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(CreateProjectRequest Request, string ProjectOwnerId), CreateProjectCommand>()
        .Map( dest => dest.ProjectOwnerId, src => src.ProjectOwnerId)
        .Map( dest => dest, src => src.Request );
    }
}