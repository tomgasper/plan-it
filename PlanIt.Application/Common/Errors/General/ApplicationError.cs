using FluentResults;

public abstract class ApplicationError : Error, IApplicationError
{
    protected ApplicationError(string message) : base(message)
    {
    }

    public abstract ErrorType ErrorType { get; }
}