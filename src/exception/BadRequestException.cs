namespace wsmcbl.src.exception;

public class BadRequestException : BadHttpRequestException
{
    public BadRequestException(string message) : base(message)
    {
    }
}