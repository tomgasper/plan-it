using FluentValidation;
using PlanIt.Application.Common.Validators;

namespace PlanIt.Application.Tasks.Commands.CreateTask;

public class CreateTaskValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.DueDate).NotEmpty();
        RuleForEach(x => x.AssignedUsers).MustBeGuid();
    }
}