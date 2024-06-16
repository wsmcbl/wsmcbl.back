namespace wsmcbl.back.model.accounting;

public class DebtHistoryEntity
{
    public string studentId { get; set; } = null!;

    public int tariffId { get; set; }

    public bool isPaid { get; set; }

    public string schoolyear { get; set; } = null!;

    public float debtBalance { get; set; }

    public float subAmount { get; set; }

    public float arrear { get; set; }
    
    
    public double amount { get; set; }


    public TariffEntity tariff { get; set; } = null!;

    internal float subtotal()
    {
        return tariff.amount * (1 - subAmount);
    }

    public bool havePayments()
    {
        return subtotal() > debtBalance;
    }
}