using wsmcbl.src.model.config;

namespace wsmcbl.src.model.accounting;

public class CashierEntity 
{
    public string? cashierId { get; set; }
    public string userId { get; set; } = null!;
    public UserEntity user { get; set; } = null!;

    public string getAlias()
    {
        return $"{user.name[0]}. {user.surname}";
    }
}