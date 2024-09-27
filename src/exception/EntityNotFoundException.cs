namespace wsmcbl.src.exception;

public class EntityNotFoundException : NotFoundException
{
    public EntityNotFoundException(string message) : base(message)
    {
        
    }
    
    public EntityNotFoundException(string type, string? id) : this($"Entity of type ({type}) with id ({id}) not found.")
    {
    }
}