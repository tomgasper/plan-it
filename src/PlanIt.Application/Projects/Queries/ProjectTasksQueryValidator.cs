using FluentValidation;

namespace PlanIt.Application.Projects.Queries;

public class ProjectTasksQueryValidator : AbstractValidator<ProjectTasksQuery>
{
    public ProjectTasksQueryValidator()
    {
        RuleFor(x => x.ProjectId)
            .NotEmpty();
    }
}