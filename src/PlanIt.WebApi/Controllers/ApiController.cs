using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PlanIt.WebApi.Common.Http;

namespace PlanIt.WebApi.Controllers;

[ApiController]
[Authorize]
public class ApiController : ControllerBase
{
    protected IActionResult Problem(List<IError> errors)
        {
        if (errors.Count == 0)
        {
            return Problem();
        }

        if (errors.All(error => error is ValidationError))
        {
            return ValidationProblem(errors.Cast<ValidationError>().ToList());
        }

        HttpContext.Items[HttpContextItemKeys.Errors] = errors;
        
        var error = errors[0];
        if (error is IApplicationError applicationError)
        {
            return ApplicationProblem(applicationError);
        }

            return Problem();
    }

    private IActionResult ValidationProblem(List<ValidationError> errors)
        {   
        var modelStateDictionary = new ModelStateDictionary();
        foreach (var error in errors)
        {
            modelStateDictionary.AddModelError(
                error.PropertyName,
                error.Message);
        }
        return ValidationProblem(modelStateDictionary);
    }

    private IActionResult ApplicationProblem(IApplicationError applicationError)
    {
        var statusCode = applicationError.ErrorType switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, title: applicationError.Message);
    }
}

