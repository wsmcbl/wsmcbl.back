namespace wsmcbl.src.exception;

public class ConflictException : BadHttpRequestException
{
    public ConflictException(string message) : base(message, 409)
    {
    }
}