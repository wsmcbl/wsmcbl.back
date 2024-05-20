namespace wsmcbl.back.model.accounting;

public class TransactionEntity
{
    public string? transactionId { get; set; }
    public string cashierId { get; set; }
    public string studentId { get; set; }
    public float total { get; set; }
    public float discount { get; set; }
    public DateTime dateTime { get; set; }
    public int areas { get; set; }
    public ICollection<TariffEntity> tariffs { get; set; } = new List<TariffEntity>();

    public void computeTotal()
    {
        float subtotal = 0;
        
        foreach (var tariff in tariffs)
        {
            subtotal += tariff.amount;
        }
        
        total = subtotal*(1 - discount);
    }
}