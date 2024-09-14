using FluentValidation;
using PlanIt.Application.Common.Validators;

namespace PlanIt.Application.Projects.Commands.CreateProject;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleForEach(x => x.ProjectTasks).ChildRules( task => task.RuleForEach( t => t.AssignedUsers).MustBeGuid());
    }
}