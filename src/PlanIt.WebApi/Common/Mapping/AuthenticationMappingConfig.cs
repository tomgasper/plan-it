using Mapster;
using PlanIt.Application.Projects.CreateProject.Commands;
using PlanIt.Contracts.Projects;

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