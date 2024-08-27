public class ValidationError : ApplicationError
{
    public ValidationError(string message, string propertName) : base(message) { PropertyName = propertName; }
    public override ErrorType ErrorType => ErrorType.Validation;
    public string PropertyName;
}