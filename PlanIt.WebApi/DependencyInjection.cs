using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace PlanIt.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
    
    // Overriding the default implementation
        services.AddProblemDetails(
        d => {
            Action<ProblemDetailsContext> extendedOptions = ctx => ctx.ProblemDetails.Extensions["Some new stuff"] = "new stuff";
            d.CustomizeProblemDetails = extendedOptions;
        });
        services.AddMappings();
        return services;
    }
}