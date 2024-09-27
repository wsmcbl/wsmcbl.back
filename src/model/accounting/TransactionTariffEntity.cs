namespace wsmcbl.src.model.accounting;

public class TransactionTariffEntity
{
    public string transactionId { get; set; } = null!;

    public int tariffId { get; set; }
    
    public float amount { get; set; }
    
    
    private TariffEntity tariff = null!;
    
    public void setTariff(TariffEntity? _tariff)
    {
        tariff = _tariff ?? throw new ArgumentException("Tariff object is null");
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

    public float calculateArrear()
    {
        return (float)(itPaidLate() ? officialAmount()*0.1 : 0);
    }

    public string schoolYear()
    {
        return tariff.schoolYear!;
    }
}