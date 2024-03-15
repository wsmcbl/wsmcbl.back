namespace wsmcbl.back.model.accounting;

public class TransactionEntity
{
    public string transactionId { get; set; }
    public string cashierId { get; set; }
    public string studentId { get; set; }
    public string studentName { get; set; }
    public List<Tariff> tariffs { get; set; }
    public float areas { get; set; }
    public DateTime dateTime { get; set; }
    

    public TransactionEntity(string cashierId, string studentId)
    {
        this.cashierId = cashierId;
        this.studentId = studentId;
    }

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