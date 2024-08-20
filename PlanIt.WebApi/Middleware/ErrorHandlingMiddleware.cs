using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace PlanIt.WebApi.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = JsonSerializer.Serialize(new {error = "An error occurred while processing your request"});

            /*

            var problem = new ProblemDetails{
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
                Title = "An error occurred while processing your request",
                Status = (int)HttpStatusCode.InternalServerError
            };

            context.Response = problem;
            return context.Response.WriteAsync(result);
            */

            return context.Response.WriteAsync(result);
        }
    }
}