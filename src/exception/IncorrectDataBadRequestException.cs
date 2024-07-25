namespace wsmcbl.src.exception;

public class IncorrectDataBadRequestException : BadRequestException
{
    public IncorrectDataBadRequestException(string entityName) : base($"Incorrect {entityName} data. Check the data.")
    {
    }
}