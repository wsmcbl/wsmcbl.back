namespace wsmcbl.src.exception;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string type, string id) : base($"Entity {type} with ID = {id} not found.")
    {
    }
}