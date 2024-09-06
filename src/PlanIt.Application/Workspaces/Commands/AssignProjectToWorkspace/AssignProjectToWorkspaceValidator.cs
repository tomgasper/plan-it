using System.Data;
using FluentValidation;
using PlanIt.Application.Common.Validators;

namespace PlanIt.Application.Workspaces.Commands.AssignProjectToWorkspace;

public sealed class AssignProjectToWorkspaceValidator : AbstractValidator<AssignProjectToWorkspaceCommand>
{
    public AssignProjectToWorkspaceValidator()
    {
        RuleFor( x => x.ProjectId ).MustBeGuid();
        RuleFor( x => x.WorkspaceId ).MustBeGuid();
    }
}