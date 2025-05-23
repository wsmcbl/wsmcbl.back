namespace wsmcbl.src.model.accounting;

public class TransactionTariffEntity
{
    public string transactionId { get; set; } = null!;
    public int tariffId { get; set; }
    public decimal amount { get; set; }
    public decimal arrears { get; set; }
    public decimal discount { get; set; }
    public decimal debtBalance { get; set; }

    public TariffEntity tariff { get; set; } = null!;

    public TransactionTariffEntity()
    {}
    
    public TransactionTariffEntity(int tariffId, decimal amount, string? transactionId = null)
    {
        this.transactionId = transactionId ?? "";
        this.tariffId = tariffId;
        this.amount = amount;
    }
    
    public void setTariff(TariffEntity? _tariff)
    {
        tariff = _tariff ?? throw new ArgumentException("Tariff object is null.");
    }

    public string concept()
    {
        return tariff.concept;
    }

    public bool paidLate()
    {
        return tariff.isLate;
    }

    public void setDebtAmounts(DebtHistoryEntity value)
    {
        arrears = value.arrears;
        discount = value.calculateDiscount();
        debtBalance = value.debtBalance;
    }

    public decimal officialAmount()
    {
        return tariff.amount;
    }
}