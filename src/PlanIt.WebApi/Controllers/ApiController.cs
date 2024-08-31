using System.Security.Claims;
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
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, title: applicationError.Message);
    }

    protected string GetUserId()
    {
        if (User.Identity == null || !User.Identity.IsAuthenticated)
        {
            throw new Exception("User must be authenticated in order to get the Id");
        }

        var userIdFromJwt = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdFromJwt)) throw new Exception(
            "Couldn't retrieve User's id from Claims Principal."
            );
        
        return userIdFromJwt;
    }
}