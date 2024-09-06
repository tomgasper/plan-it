using FluentValidation;

namespace PlanIt.Application.Common.Validators;

public static class GuidValidator
{
    public static IRuleBuilderOptions<T, string> MustBeGuid<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Must(ValidateIsGuid).WithMessage("The property must be a valid GUID");
    }

    public static bool ValidateIsGuid(string str)
    {
        return Guid.TryParse(str, out _);
    }
}