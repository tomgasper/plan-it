public class InternalServerError : ApplicationError
{
    public InternalServerError(string message) : base(message) { }
    public override ErrorType ErrorType => ErrorType.InternalServerError;
}