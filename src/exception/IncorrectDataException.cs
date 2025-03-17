namespace wsmcbl.src.exception;

public class IncorrectDataException : BadRequestException
{
    public IncorrectDataException(string message) : base(message)
    {
    }

    public IncorrectDataException(string element, string detail) : base($"Incorrect {element}. {detail}.") 
    {
    }
}