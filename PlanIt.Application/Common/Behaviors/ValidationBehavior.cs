using FluentValidation;
using MediatR;
using PlanIt.Application.Authentication.Commands.Register;
using PlanIt.Application.Services.Authentication.Common;

namespace PlanIt.Application.Common.Behaviors;

public class ValidateRegisterCommandBehavior : IPipelineBehavior<RegisterCommand, AuthenticationResult>
{
    private readonly IValidator<RegisterCommand> _validator;

    public ValidateRegisterCommandBehavior(IValidator<RegisterCommand> validator)
    {
        _validator = validator;
    }

    public async Task<AuthenticationResult> Handle(
        RegisterCommand request,
        RequestHandlerDelegate<AuthenticationResult> next,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        // Before the handler
        var result = await next();
        // After the handler

        return result;
    }
}