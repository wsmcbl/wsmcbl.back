using wsmcbl.back.model.config;

namespace wsmcbl.back.model.accounting;

public class CashierEntity 
{
    public string cashierId { get; set; }
    
    public string userId { get; set; }
    
    public UserEntity user { get; set; }
    
    public string fullName()
    {
        return user.name + " " + user.surname;
    }
}