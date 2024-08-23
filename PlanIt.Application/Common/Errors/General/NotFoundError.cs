public class NotFoundError : ApplicationError
{
    public NotFoundError(string message) : base(message) { }
    public override ErrorType ErrorType => ErrorType.NotFound;
}