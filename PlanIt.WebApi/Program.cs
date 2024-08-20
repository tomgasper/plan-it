using Microsoft.AspNetCore.Mvc.Infrastructure;
using PlanIt.Application;
using PlanIt.Infrastructure;
using PlanIt.WebApi.Middleware;
using PlanIt.WebApi.Errors;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

    builder.Services.AddControllers();
    
    // Overriding the default implementation
    builder.Services.AddSingleton<ProblemDetailsFactory, PlanItProblemDetailsFactory>();
}

var app = builder.Build();
{
    // app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}