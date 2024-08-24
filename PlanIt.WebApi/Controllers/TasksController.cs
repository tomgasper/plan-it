using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanIt.WebApi.Controllers;

[Route("[controller]")]
public class TasksController : ApiController{
    [HttpGet]
    public IActionResult LastTask()
    {
        return Ok(Array.Empty<string>());
    }
}