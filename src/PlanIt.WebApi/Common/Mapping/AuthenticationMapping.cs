using PlanIt.Application.Services.Authentication.Common;
using PlanIt.Contracts.Authenthication;

namespace PlanIt.WebApi.Common.Mapping;

public static class AuthenthicationMapping
{
    public static AuthenticationResponse ToResponse(this AuthenticationResult authResult) => (
        new AuthenticationResponse(
            Id: authResult.User.Id,
            FirstName: authResult.User.FirstName,
            LastName: authResult.User.LastName,
            Token: authResult.Token
        )
    );
}