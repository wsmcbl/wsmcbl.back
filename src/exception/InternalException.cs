namespace wsmcbl.src.exception;

public class InternalException : BadHttpRequestException
{
    public InternalException() : base("Unexpected error")
    {
    }
    
    public InternalException(string message) : base(message, 500)
    {
    }
}