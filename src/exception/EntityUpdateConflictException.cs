namespace wsmcbl.src.exception;

public class EntityUpdateConflictException : ConflictException
{
    public EntityUpdateConflictException(string message) : base($"This entity is already updated. {message}")
    {
    }
}