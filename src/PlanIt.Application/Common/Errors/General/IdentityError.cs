public class IdentityError : ApplicationError
{
    public IdentityError(string message) : base(message) { }
    public override ErrorType ErrorType => ErrorType.Validation;
}