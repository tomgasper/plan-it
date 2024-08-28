using System.Data;
using FluentValidation;
using FluentValidation.AspNetCore;
using PlanIt.Application.Projects.Commands.AddTaskToProject;

namespace PlanIt.Application.Projects.Commands.CreateTask;

public class CreateTaskValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}