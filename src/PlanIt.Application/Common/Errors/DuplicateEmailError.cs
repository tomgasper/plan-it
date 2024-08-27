using FluentResults;

public class DuplicateEmailError : ConflictError
{
    public DuplicateEmailError() : base("This email has already been used!")
    {
    }
}