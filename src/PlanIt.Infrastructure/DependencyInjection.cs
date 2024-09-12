using System.Text;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PlanIt.Application.Common.Interfaces.Authentication;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Application.Common.Interfaces.Services;
using PlanIt.Application.Common.Interfaces.Services.ImageStorage;
using PlanIt.Infrastructure.Authentication;
using PlanIt.Infrastructure.Persistence;
using PlanIt.Infrastructure.Persistence.Interceptors;
using PlanIt.Infrastructure.Persistence.Repositories;
using PlanIt.Infrastructure.Services;
using PlanIt.Infrastructure.Services.ImageStorage;

namespace PlanIt.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddAuth(configuration);
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddImageStorage(configuration);
        services.AddPersistence();

        return services;
    }

    public static IServiceCollection AddAuth(
        this IServiceCollection services,
        ConfigurationManager configuration
    )
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtGenerator, JwtGenerator>();

        services.AddIdentityCore<ApplicationUser>()
            .AddSignInManager<SignInManager<ApplicationUser>>()
            .AddEntityFrameworkStores<PlanItDbContext>();

        services.AddScoped<IIdentity, Identity>();
        
        services.AddScoped<IUserContext, UserContext>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret)
                )
            });

        return services;
    }

    public static IServiceCollection AddPersistence(
        this IServiceCollection services
    )
    {
        services.AddDbContext<PlanItDbContext>(options => options.UseSqlServer("Server=localhost;Database=PlanIt;User Id=sa;Password=Pass1234$;Encrypt=false;"));

        services.AddScoped<PublishDomainEventsInterceptor>();
        services.AddScoped<IUserRepository,UserRepository>();
        services.AddScoped<IWorkspaceRepository, WorkspaceRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        return services;
    }

    public static IServiceCollection AddImageStorage(this IServiceCollection services, ConfigurationManager configuration)
    {
        // var cloudinarySettings = new CloudinarySettings();
        // configuration.Bind(CloudinarySettings.SectionName, cloudinarySettings);
        services.Configure<CloudinarySettings>(configuration.GetSection(CloudinarySettings.SectionName));

        services.AddSingleton( sp => {
            var cloudinarySettings = sp.GetRequiredService<IOptions<CloudinarySettings>>().Value;

            var account = new Account(
                cloudinarySettings.CloudName,
                cloudinarySettings.ApiKey,
                cloudinarySettings.ApiSecret
            );
            return new Cloudinary(account);
        });

        services.AddScoped<IImageStorage, ImageStorage>();

        return services;
    }
}