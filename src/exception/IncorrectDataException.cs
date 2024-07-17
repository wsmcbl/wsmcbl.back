namespace wsmcbl.src.exception;

public class IncorrectDataException : Exception
{
    public IncorrectDataException(string entityName) :
        base($"Incorrect {entityName} data. Check the data.")
    {
    }
}