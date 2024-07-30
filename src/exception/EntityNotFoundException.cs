namespace wsmcbl.src.exception;

public class EntityNotFoundException : NotFoundException
{
    public EntityNotFoundException(string message) : base(message)
    {
        
    }
    
    public EntityNotFoundException(string type, string id) : this($"Entity {type} with ID = {id} not found.")
    {
    }
}