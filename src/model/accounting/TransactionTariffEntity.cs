namespace wsmcbl.src.model.accounting;

public class TransactionTariffEntity
{
    public string transactionId { get; set; } = null!;
    public int tariffId { get; set; }
    public float amount { get; set; }
    
    private TariffEntity tariff = null!;

    public TransactionTariffEntity()
    {}
    
    public TransactionTariffEntity(int tariffId, float amount, string? transactionId = null)
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

    public float officialAmount()
    {
        return tariff.amount;
    }

    public bool itPaidLate()
    {
        return tariff.isLate;
    }

    public float calculateArrears()
    {
        return (float)(tariff.type == 1 && itPaidLate() ? officialAmount()*0.1 : 0);
    }
}