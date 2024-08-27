using FluentResults;

public interface IApplicationError : IError
{
    ErrorType ErrorType { get; }
}