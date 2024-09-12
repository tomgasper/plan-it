using FluentResults;

public class ImageStorageError : InternalServerError
{
    public ImageStorageError(string message) : base(message)
    {
    }
}