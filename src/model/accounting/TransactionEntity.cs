namespace wsmcbl.src.model.accounting;

public class TransactionEntity
{
    public string? transactionId { get; set; }
    public int number { get; set; }
    public string cashierId { get; set; } = null!;
    public string studentId { get; set; } = null!;
    public float total { get; set; }
    public DateTime date { get; set; }
    public ICollection<TransactionTariffEntity> details { get; set; } = [];
    public bool isValid { get; set; }

    public void computeTotal()
    {
        total = 0;
        
        foreach (var item in details)
        {
            total += item.amount;
        }
    }

    public bool haveValidContent()
    {
        return !string.IsNullOrWhiteSpace(studentId) &&
               !string.IsNullOrWhiteSpace(cashierId) &&
               details.Count > 0;
    }

    public List<int> getTariffIdList()
    {
        return details.Select(item => item.tariffId).ToList();
    }

    public void setAsInvalid()
    {
        isValid = false;
    }
}