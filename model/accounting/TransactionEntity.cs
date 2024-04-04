namespace wsmcbl.back.model.accounting;

public class TransactionEntity
{
    public string transactionId { get; set; }
    public string cashierId { get; set; }
    public string studentId { get; set; }
    public ICollection<TariffEntity> tariffs { get; set; } = new List<TariffEntity>();
    private float areas { get; set; }
    public DateTime dateTime { get; set; }
    public float discount { get; set; }
    public float total { get; set; }

    public float getAmount()
    {
        float amount = 0;
        
        foreach (var tariff in tariffs)
        {
            amount += tariff.amount;
        }
        return amount*(1 - areas);
    }
}