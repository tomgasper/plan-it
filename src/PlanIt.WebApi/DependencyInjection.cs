using PlanIt.WebApi.Common.Mapping;

namespace PlanIt.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddCors( options => {
            options.AddDefaultPolicy(
                builder => {
                    // Allow any part on localhost for dev purposes
                    builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                }
            );
        });
        /*
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
        */
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