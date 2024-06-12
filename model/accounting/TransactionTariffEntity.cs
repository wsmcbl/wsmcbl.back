namespace wsmcbl.back.model.accounting;

public class TransactionTariffEntity
{
    public string transactionId { get; set; } = null!;

    public int tariffId { get; set; }
    
    public float amount { get; set; }

    public float? discount { get; set; }

    public float arrears { get; set; }
    
    public float subTotal { get; set; }
    

    private TariffEntity tariff = null!;
    public void setTariff(TariffEntity? _tariff)
    {
        tariff = _tariff ?? throw new ArgumentException("Tariff objet is null");
    }

    public void applyArrears()
    {
        tariff.checkDueDate();
        
        if (itPaidLate())
        {
            subTotal *= (1 + arrears);
        }
    }

    public void computeSubTotal()
    {
        amount = tariff.amount;
        subTotal = (float)(amount * (1 - discount))!;
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

    public string schoolYear()
    {
        return tariff.schoolYear;
    }
}