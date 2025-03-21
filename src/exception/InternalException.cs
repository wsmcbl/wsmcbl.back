namespace wsmcbl.src.exception;

public class InternalException : BadHttpRequestException
{
    public InternalException() : this("Unexpected error")
    {
    }
    
    public InternalException(string message) : base(message, 500)
    {
    }
}