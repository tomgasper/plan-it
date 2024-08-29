using FluentResults;

public class InvalidCredentialsError : ApplicationError
{
    public InvalidCredentialsError(string message) : base(message)
    {
    }

    public override ErrorType ErrorType => ErrorType.NotFound;
}