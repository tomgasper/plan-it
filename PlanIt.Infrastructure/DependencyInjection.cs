using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlanIt.Application.Common.Interfaces.Authentication;
using PlanIt.Application.Common.Interfaces.Services;
using PlanIt.Infrastructure.Authentication;
using PlanIt.Infrastructure.Services;

namespace PlanIt.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        
        services.AddSingleton<IJwtGenerator, JwtGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        return services;
    }
}