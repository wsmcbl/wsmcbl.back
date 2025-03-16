namespace wsmcbl.src.exception;

public class IncorrectDataException : BadRequestException
{
    public IncorrectDataException(string message) : base(message)
    {
    }

    public IncorrectDataException(string parameter, string value) : base($"Incorrect {parameter} data. Check the {value}.") 
    {
    }
}