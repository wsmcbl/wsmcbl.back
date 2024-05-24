namespace wsmcbl.back.model.accounting;

public class TransactionTariffEntity
{
    public string transactionId { get; set; } = null!;

    public int tariffId { get; set; }

    public float? discount { get; set; }

    public float? arrears { get; set; }
    
    public float subTotal { get; set; }
    

    private TariffEntity tariff = null!;
    public void setTariff(TariffEntity? tariff)
    {
        this.tariff = tariff!;
    }

    public void applyArrears()
    {
        tariff.checkDueDate();
        if (itPaidLate())
            subTotal += (float)arrears!;
    }

    public void computeSubTotal()
    {
        subTotal = (float)(tariff.amount - discount)!;
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
        return (bool) tariff.isLate!;
    }

    public string schooyear()
    {
        return tariff.schoolYear;
    }
}