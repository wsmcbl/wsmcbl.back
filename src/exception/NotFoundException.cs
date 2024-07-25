namespace wsmcbl.src.exception;

public class NotFoundException : BadHttpRequestException
{
    public NotFoundException(string message) : base(message, 404)
    {
    }
}