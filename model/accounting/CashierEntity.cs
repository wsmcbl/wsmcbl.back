namespace wsmcbl.back.model.accounting;

public class CashierEntity
{
    public string cashierId { get; set; }
    public string name { get; set; }
    public string surname { get; set; }
    public bool sex { get; set; }
    
    public string fullName()
    {
        return name + " " + surname;
    }
}