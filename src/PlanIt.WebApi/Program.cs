using PlanIt.WebApi;
using PlanIt.Application;
using PlanIt.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.UseCors();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}