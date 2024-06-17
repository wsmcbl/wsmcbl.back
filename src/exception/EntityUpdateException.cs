namespace wsmcbl.src.exception;

public class EntityUpdateException : Exception
{
    public EntityUpdateException(string message) : base($"This entity is already updated. {message}")
    {
    }
}