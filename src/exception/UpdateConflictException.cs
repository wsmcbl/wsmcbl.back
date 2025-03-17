namespace wsmcbl.src.exception;

public class UpdateConflictException : ConflictException
{
    public UpdateConflictException(string element, string detail) : base($"Could not update {element}. {detail}")
    {
    }
}