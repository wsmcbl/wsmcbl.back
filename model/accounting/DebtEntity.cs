namespace wsmcbl.back.model.accounting;

public class DebtEntity
{
    public string studentId { get; set; } = null!;

    public int tariffId { get; set; }
    
    public TariffEntity tariff { get; set; }

    public bool isPaid { get; set; }
}
