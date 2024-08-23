public class ConflictError : ApplicationError
{
    public ConflictError(string message) : base(message) { }
    public override ErrorType ErrorType => ErrorType.Conflict;
}
