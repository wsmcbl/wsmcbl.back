using wsmcbl.src.model.config;

namespace wsmcbl.src.model.accounting;

public class CashierEntity 
{
    public string? cashierId { get; set; }
    public Guid userId { get; set; }
    public UserEntity user { get; set; } = null!;

    public string getAlias()
    {
        return $"{user.name[0]}. {user.surname}";
    }
}