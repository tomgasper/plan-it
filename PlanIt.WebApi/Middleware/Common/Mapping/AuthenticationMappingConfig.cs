using Mapster;
using PlanIt.Application.Services.Authentication.Common;
using PlanIt.Contracts.Authenthication;

namespace PlanIt.WebApi.Middleware.Common.Mapping;

public class AuthhenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
        .Map(dest => dest.Token, src => src.Token)
        .Map(dest => dest, src => src.User);
    }
}