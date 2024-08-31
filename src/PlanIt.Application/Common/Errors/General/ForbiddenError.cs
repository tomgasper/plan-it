public class ForbiddenError : ApplicationError
{
    public ForbiddenError(string message) : base(message) { }
    public override ErrorType ErrorType => ErrorType.Forbidden;
}