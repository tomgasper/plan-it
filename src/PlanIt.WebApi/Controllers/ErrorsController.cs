using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PlanIt.WebApi.Controllers;

namespace PlanIt.Api.WebApi;

public class ErrorsController : ApiController
{
    [Route("/error")]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        
        return Problem(); 
    }
}