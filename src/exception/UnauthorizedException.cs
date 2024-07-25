namespace wsmcbl.src.exception;

public class UnauthorizedException : BadHttpRequestException
{
    public UnauthorizedException(string message) : base(message, 401)
    {
    }
}