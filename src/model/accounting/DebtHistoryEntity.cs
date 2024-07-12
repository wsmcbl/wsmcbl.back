namespace wsmcbl.src.model.accounting;

public class DebtHistoryEntity
{
    public string studentId { get; set; } = null!;
    public int tariffId { get; set; }
    public string schoolyear { get; set; } = null!;
    public float arrear { get; set; }
    public float subAmount { get; set; }
    public float amount { get; set; }
    public float debtBalance { get; set; }
    public bool isPaid { get; set; }
    
    public TariffEntity tariff { get; set; } = null!;
    
    public bool havePayments()
    {
        return debtBalance > 0;
    }

    public float getDebtBalance()
    {
        var debt = amount - debtBalance;

        if (debt < 0)
        {
            return 0;
        }

        return debt;
    }
}