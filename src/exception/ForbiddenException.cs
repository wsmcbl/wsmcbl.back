namespace wsmcbl.src.exception;

public class ForbiddenException : BadHttpRequestException
{
    public ForbiddenException(string message) : base(message, 403)
    {
    }
}